using Xunit;
using GrmTask.Application.Parsers;

namespace GrmTask.Tests.Parsers
{
    public class DateParserTests
    {
        [Theory]
        [InlineData("1st March 2012", 2012, 3, 1)]
        [InlineData("2nd Feb 2012", 2012, 2, 2)]
        [InlineData("25th Dec 2012", 2012, 12, 25)]
        [InlineData("3rd Jan 2013", 2013, 1, 3)]
        public void Parse_ValidDates_ReturnsCorrectDate(string input, int year, int month, int day)
        {
            var result = DateParser.Parse(input);

            Assert.NotNull(result);
            Assert.Equal(new DateTime(year, month, day), result.Value);
        }

        [Fact]
        public void Parse_InvalidDate_ReturnsNull()
        {
            var result = DateParser.Parse("some invalid date");
            Assert.Null(result);
        }
    }
}