namespace CurveFunctions;

//Interface for various interpolator input schemes
public interface IInterpolatorInput
{
    public float Time { get; set; }
}

//Interpolation functions for timed transitions.
//Are you dealing with a fixed duration? Use one of these.
public static partial class Interpolator
{
    public class EmptyInput : IInterpolatorInput
    {
        public float Time { get; set; }
    }
}
