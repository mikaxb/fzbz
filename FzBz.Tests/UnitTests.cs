using FzBz.Domain;

namespace FzBz.Tests;

public class UnitTests
{
    [Fact]
    public void SimpleNonHitTest()
    {
        var logger = new TestLogger<FzBzService>();
        var service = new FzBzService(logger);

        var number = 4;

        var result = service.GetResult([number]);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(number.ToString(), result[number]);
    }

    [Theory]
    [MemberData(nameof(KnownNumbersAndResults))]
    public void TestKnownNumbers(NumbersAndResult knownNumbersAndResults)
    {
        var logger = new TestLogger<FzBzService>();
        var service = new FzBzService(logger);
        var uniqueNumberCount = knownNumbersAndResults.Numbers.Distinct().Count();
        var duplicateCount = knownNumbersAndResults.Numbers.Length - uniqueNumberCount;

        var result = service.GetResult(knownNumbersAndResults.Numbers);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(uniqueNumberCount, result.Count);
        Assert.All(knownNumbersAndResults.Result, r => Assert.Equal(result[r.Key], r.Value));
        Assert.Equal(duplicateCount, logger.Entries.Count);
    }

    public static IEnumerable<object[]> KnownNumbersAndResults()
    {
        yield return new object[] { new NumbersAndResult([1, 2, 3, 4, 5], new Dictionary<int, string> { { 1, 1.ToString() }, { 2, 2.ToString() }, { 3, StringDefinitions.FIZZ }, { 4, 4.ToString() }, { 5, StringDefinitions.BUZZ } }) };
        yield return new object[] { new NumbersAndResult([15], new Dictionary<int, string> { { 15, StringDefinitions.FIZZ + StringDefinitions.BUZZ } }) };
        yield return new object[] { new NumbersAndResult([1, 1, 3, 3, 5], new Dictionary<int, string> { { 1, 1.ToString() }, { 3, StringDefinitions.FIZZ }, { 5, StringDefinitions.BUZZ } }) };
    }

    public record NumbersAndResult(int[] Numbers, Dictionary<int, string> Result);
}