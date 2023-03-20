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

    //basic lerp implementation, just for demonstrating the structure
    public class LerpInput : IInterpolatorInput
    {
        public float Start { get; set; }
        public float End { get; set; }
        public float Time { get; set; }
    }

    public static float Lerp(IInterpolatorInput parameters)
    {
        var input = (LerpInput)parameters;
        return Lerp(input.Start, input.End, input.Time);
    }

    public static float Lerp(float start, float end, float time)
    {
        //t is the proportion of the displacement we wish to apply
        var t = time;

        //so we 'blend' t with the end value,
        var endWeight = t * end;

        //and t's inverse with the start value.
        var startWeight = (1f - t) * start;

        //then combine their weights to finish the interpolation
        return startWeight + endWeight;
    }
}
