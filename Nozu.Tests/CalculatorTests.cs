using Nozu.Domain;

namespace Nozu.Tests;

public class CalculatorTests
{
    private readonly Calculator _calculator;

    public CalculatorTests()
    {
        _calculator = new Calculator();
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(3, 4, 7)]
    [InlineData(-1, -1, -2)]
    public void Add_ShouldReturnSum_WhenCalled(int a, int b, int expectedSum)
    {
        var result = _calculator.Add(a, b);

        Assert.Equal(expectedSum, result);
    }

    [Theory]
    [InlineData(1, 2, -1)]
    [InlineData(2, 1, 1)]
    [InlineData(-1, -1, 0)]
    public void Subtract_ShouldReturnDifference_WhenCalled(int a, int b, int expectedDifference)
    {
        var result = _calculator.Subtract(a, b);

        Assert.Equal(expectedDifference, result);
    }

    [Theory]
    [InlineData(1, 2, 2)]
    [InlineData(2, 2, 4)]
    [InlineData(-2, 3, -6)]
    public void Multiply_ShouldReturnProduct_WhenCalled(int a, int b, int expectedProduct)
    {
        var result = _calculator.Multiply(a, b);

        Assert.Equal(expectedProduct, result);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 0)]
    public void Divide_ShouldThrowException_WhenArgumentIsDivisibleByZero(int a, int b)
    {
        Assert.Throws<DivideByZeroException>(() => _calculator.Divide(a, b));
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(2, 2, 1)]
    [InlineData(7, 2, 3)]
    public void Divide_ShouldReturnQuotient_WhenArgumentIsNotDivisibleByZero(int a, int b, int expectedQuotient)
    {
        var result = _calculator.Divide(a, b);

        Assert.Equal(expectedQuotient, result);
    }
}
