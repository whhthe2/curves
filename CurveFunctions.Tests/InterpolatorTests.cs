using CurveFunctions;
using System.Text;

namespace CurveFunctions.Tests;

public class InterpolatorTests
{
    private readonly ITestOutputHelper output;

    public InterpolatorTests(ITestOutputHelper testOutputHelper)
    {
        output = testOutputHelper;
    }

    [Theory]
    [InlineData(0.0f)]
    [InlineData(0.25f)]
    [InlineData(0.33f)]
    [InlineData(0.5f)]
    [InlineData(0.66f)]
    [InlineData(0.75f)]
    [InlineData(1.0f)]
    public void Interpolator_Lerp_Scale0to1_ResultEqualsT(float t)
    {
        var input = new Interpolator.LerpInput
        {
            Start = 0f,
            End = 1f,
            Time = t
        };
        var result = Interpolator.Lerp(input);
        Assert.True(result == t);
    }

    [Theory]
    [InlineData(0.5f, 50f)]
    [InlineData(0.42, 42f)]
    [InlineData(0.1259, 12.59f)]
    [InlineData(0.0f, 0f)]
    public void Interpolator_Lerp_Scale0to100_ResultEqualsExpected(float t, float expected) 
    {
        var input = new Interpolator.LerpInput
        {
            Start = 0f,
            End = 100f,
            Time = t
        };
        var result = Interpolator.Lerp(input);
        Assert.True(result == expected);
    }

    [Fact]
    public void Interpolator_Lerp_Graph()
    {
        var printer = new GraphPrinter(output, 20, Interpolator.Lerp);
        printer.Function = Interpolator.Lerp;
        printer.Input = new Interpolator.LerpInput
        {
            Start = 2,
            End = 4
        };
        printer.Input = new Interpolator.LerpInput
        {
            Start = 4,
            End = 16
        };
        printer.Print();
    }

    [Fact]
    public void Interpolator_ExponentialImpulse_Peak()
    {
        //the exponential impulse function produces its peak when time = 1/velocity
        var velocity = 5f;
        var peak = 25f;
        var result = Interpolator.ExponentialImpulse(velocity, peak, 1 / velocity);
        Assert.Equal(peak, result);
    }

    [Theory]
    [InlineData(1f)]
    [InlineData(2f)]
    [InlineData(5f)]
    [InlineData(10f)]
    [InlineData(30f)]
    public void Interpolator_Impulse_Graph(float velocity)
    {
        var size = 25;
        var printer = new GraphPrinter(output, size, Interpolator.ExponentialImpulse);
        printer.Input = new Interpolator.ExponentialImpulseInput { Velocity = velocity, Magnitude = size };
        printer.Print();
    }

    [Fact]
    public void Interpolator_AlmostIdentity_Graph()
    {
        var size = 25;
        var printer = new GraphPrinter(output, size, Interpolator.AlmostIdentity);
        printer.Input = new Interpolator.AlmostIdentityInput { Floor = 5f, Threshold = 15f, Magnitude = size };
        printer.Print();
    }

    [Fact]
    public void Interpolator_Smoothstep_Graph()
    {
        var size = 25;
        var printer = new GraphPrinter(output, size, Interpolator.Smoothstep);
        printer.Input = new Interpolator.SmoothStepInput { Edge0 = 0.10f, Edge1 = 0.9f, Magnitude = size };
        printer.Input = new Interpolator.SmoothStepInput { Edge0 = 0.33f, Edge1 = 0.66f, Magnitude = size };
        printer.Print();
    }

}