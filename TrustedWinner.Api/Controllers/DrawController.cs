using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TrustedWinner.Api.Configuration;
using TrustedWinner.Api.Data;
using TrustedWinner.Api.Models;
using TrustedWinner.Core;
using System.Text.Json;

namespace TrustedWinner.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrawController : ControllerBase
    {
        private readonly CertificateSettings _certificateSettings;
        private readonly DrawDbContext _dbContext;

        public DrawController(IOptions<CertificateSettings> certificateSettings, DrawDbContext dbContext)
        {
            _certificateSettings = certificateSettings.Value;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Executes an instant draw with the provided entries and configuration.
        /// The result is not persisted and is returned immediately.
        /// </summary>
        /// <param name="request">The draw request containing entries and configuration.</param>
        /// <returns>A draw result containing all the draw information including winners, substitutes, and optional signature.</returns>
        /// <response code="200">Returns the draw result in the official TrustedWinner format.</response>
        /// <response code="400">If the request is invalid or the draw execution fails.</response>
        [HttpPost("instantDraw")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DrawResult), 200)]
        [ProducesResponseType(400)]
        public ActionResult<DrawResult> ExecuteInstantDraw(DrawRequest request)
        {
            if (request.Entries == null || request.Entries.Count == 0)
            {
                return BadRequest("No entries provided for the draw.");
            }

            if (request.Configuration.Winners == 0 || request.Configuration.Winners > request.Entries.Count)
            {
                return BadRequest("Invalid number of winners specified.");
            }

            try 
            {
                DrawExecutor executor;
                if (!string.IsNullOrEmpty(_certificateSettings.CertificatePath) && !string.IsNullOrEmpty(_certificateSettings.CertificatePassword))
                {
                    var certificate = new X509Certificate2(_certificateSettings.CertificatePath, _certificateSettings.CertificatePassword);
                    executor = new DrawExecutor(request.Configuration, [.. request.Entries], certificate);
                }
                else 
                {
                    executor = new DrawExecutor(request.Configuration, [.. request.Entries]);
                }

                // Execute the draw
                executor.ExecuteDraw(request.AdditionalEntropy);

                // Return the JSON string directly while maintaining content type
                return new ContentResult
                {
                    Content = executor.GetAuditableJson(),
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to execute draw: {ex.Message}");
            }
        }

        /// <summary>
        /// Executes a persistent draw with the provided entries and configuration.
        /// The result is stored in the database and can be retrieved later.
        /// </summary>
        /// <param name="request">The persistent draw request containing entries, configuration, contest ID, and title.</param>
        /// <returns>The unique identifier of the created draw.</returns>
        /// <response code="200">Returns the draw ID.</response>
        /// <response code="400">If the request is invalid or the draw execution fails.</response>
        /// <response code="409">If a draw with the same contest ID and title already exists.</response>
        [HttpPost("persistentDraw")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<Guid>> ExecutePersistentDraw(PersistentDrawRequest request)
        {
            if (request.Entries == null || request.Entries.Count == 0)
            {
                return BadRequest("No entries provided for the draw.");
            }

            if (request.Configuration.Winners == 0 || request.Configuration.Winners > request.Entries.Count)
            {
                return BadRequest("Invalid number of winners specified.");
            }

            if (string.IsNullOrEmpty(request.ContestId) || string.IsNullOrEmpty(request.Title))
            {
                return BadRequest("Contest ID and Title are required for persistent draws.");
            }

            try 
            {
                // Check if draw already exists
                var existingDraw = await _dbContext.Draws
                    .FirstOrDefaultAsync(d => d.ContestId == request.ContestId && d.Title == request.Title);

                if (existingDraw != null)
                {
                    return Conflict($"A draw with Contest ID '{request.ContestId}' and Title '{request.Title}' already exists.");
                }

                DrawExecutor executor;
                if (!string.IsNullOrEmpty(_certificateSettings.CertificatePath) && !string.IsNullOrEmpty(_certificateSettings.CertificatePassword))
                {
                    var certificate = new X509Certificate2(_certificateSettings.CertificatePath, _certificateSettings.CertificatePassword);
                    executor = new DrawExecutor(request.Configuration, [.. request.Entries], certificate);
                }
                else 
                {
                    executor = new DrawExecutor(request.Configuration, [.. request.Entries]);
                }

                // Execute the draw
                executor.ExecuteDraw(request.AdditionalEntropy);
                var drawResult = JsonSerializer.Deserialize<DrawResult>(executor.GetAuditableJson(), DrawResult.SerializerOptions)
                    ?? throw new InvalidOperationException("Failed to deserialize draw result");

                // Create and save the draw record
                var draw = Draw.FromDrawResult(request.ContestId, request.Title, drawResult);
                _dbContext.Draws.Add(draw);
                await _dbContext.SaveChangesAsync();

                // Create and save the auditable result
                var auditableResult = AuditableResult.FromDrawResult(draw.Id, drawResult);
                _dbContext.AuditableResults.Add(auditableResult);
                await _dbContext.SaveChangesAsync();

                return Ok(draw.Id);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to execute draw: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves the main properties of a persisted draw by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the draw.</param>
        /// <returns>The draw information without the full auditable JSON.</returns>
        /// <response code="200">Returns the draw information.</response>
        /// <response code="404">If the draw is not found.</response>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Draw), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Draw>> GetDrawInfo(Guid id)
        {
            var draw = await _dbContext.Draws.FindAsync(id);
            if (draw == null)
            {
                return NotFound();
            }

            return Ok(draw);
        }

        /// <summary>
        /// Retrieves the full auditable JSON of a persisted draw by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the draw.</param>
        /// <returns>The full auditable JSON of the draw.</returns>
        /// <response code="200">Returns the full auditable JSON.</response>
        /// <response code="404">If the draw is not found.</response>
        [HttpGet("{id}/auditableResult")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<string>> GetDrawAuditableJson(Guid id)
        {
            var auditableResult = await _dbContext.AuditableResults.FindAsync(id);
            if (auditableResult == null)
            {
                return NotFound();
            }

            // Since it's already stored as JSON, we can return it directly
            return new ContentResult
            {
                Content = auditableResult.Json,
                ContentType = "application/json",
                StatusCode = 200
            };
        }
    }
} 