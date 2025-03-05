namespace TrustedWinner.Core.Tests;

public class SelectionAlgorithmTests
{
    [Fact]
    public void SelectWinnerWithSubstitutes_SameSeed_ProducesSameResults()
    {
        // Arrange
        var entries = new[] { "entry1", "entry2", "entry3", "entry4" };

        var algorithm1 = new SelectionAlgorithm(
            seed: "test-seed",
            entries: entries);
        var algorithm2 = new SelectionAlgorithm(
            seed: "test-seed",
            entries: entries);

        // Act
        var result1 = algorithm1.SelectWinnerWithSubstitutes(substitutesCount: 2);
        var result2 = algorithm2.SelectWinnerWithSubstitutes(substitutesCount: 2);

        // Assert
        Assert.Equal(result1, result2);
    }

    [Fact]
    public void SelectWinnerWithSubstitutes_DifferentSeeds_ProduceDifferentResults()
    {
        // Arrange
        var entries = new[] { "entry1", "entry2", "entry3", "entry4" };

        var algorithm1 = new SelectionAlgorithm(
            seed: "test-seed-1",
            entries: entries);
        var algorithm2 = new SelectionAlgorithm(
            seed: "test-seed-2",
            entries: entries);

        // Act
        var result1 = algorithm1.SelectWinnerWithSubstitutes(substitutesCount: 2);
        var result2 = algorithm2.SelectWinnerWithSubstitutes(substitutesCount: 2);

        // Assert
        Assert.NotEqual(result1, result2);
    }

    [Fact]
    public void SelectWinnerWithSubstitutes_NeverSelectsSameEntryTwice()
    {
        // Arrange
        var entries = new[] { "entry1", "entry2", "entry3" };

        var algorithm = new SelectionAlgorithm(
            seed: "test-seed",
            entries: entries);

        // Act
        var result = algorithm.SelectWinnerWithSubstitutes(substitutesCount: 2);

        // Assert
        var selectedEntries = new HashSet<string>();
        foreach (var entry in result)
        {
            Assert.True(selectedEntries.Add(entry), "Same entry was selected multiple times");
        }
    }

    [Fact]
    public void SelectWinnerWithSubstitutes_SelectsEvenly()
    {
        // Arrange
        var entries = new[] { "A", "B" };
        const int iterations = 1000;
        int aWins = 0;

        // Act
        for (int i = 0; i < iterations; i++)
        {
            var algorithm = new SelectionAlgorithm(
                seed: $"test-seed-{i}",
                entries: entries);
            var result = algorithm.SelectWinnerWithSubstitutes(substitutesCount: 0);
            if (result[0] == "A")
            {
                aWins++;
            }
        }

        // Assert
        // With no weights, we expect roughly equal distribution
        double ratio = (double)aWins / iterations;
        Assert.True(ratio > 0.45 && ratio < 0.55);
    }
} 