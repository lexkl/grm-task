using Xunit;
using GrmTask.Application.Parsers;

namespace GrmTask.Tests.Parsers;

public class CliParserTests
{
    [Fact]
    public void Parse_EmptyArgs_ReturnsNull()
    {
        var result = CliParser.Parse([]);
        Assert.Null(result);
    }

    [Fact]
    public void Parse_InvalidDate_ReturnsNull()
    {
        var args = new[] { "ITunes", "invalid date" };
        var result = CliParser.Parse(args);
        Assert.Null(result);
    }

    [Theory]
    [InlineData("ITunes", "1st March 2012", "ITunes", 2012, 3, 1)]
    [InlineData("YouTube", "25th Dec 2012", "YouTube", 2012, 12, 25)]
    [InlineData("Spotify", "2nd Feb 2012", "Spotify", 2012, 2, 2)]
    public void Parse_ValidArgs_ReturnsCorrectPartnerAndDate(
            string argPartner, string argDate,
            string expectedPartner, int year, int month, int day)
    {
        var args = new[] { argPartner, argDate };
        var result = CliParser.Parse(args);

        Assert.NotNull(result);
        Assert.Equal(expectedPartner, result?.Partner);
        Assert.Equal(new DateTime(year, month, day), result?.EffectiveDate);
    }
}
