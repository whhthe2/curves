namespace CurveFunctions;

//interpolation functions
//For timed transitions. Are you dealing with a fixed duration?
public interface IInterpolatorInput
{
    public float Time { get; set; }
}

public static class Interpolator
{
    public class EmptyInput : IInterpolatorInput
    {
        public float Time { get; set; }
    }

    public class LerpInput : IInterpolatorInput
    {
        public float Start { get; set; }
        public float End { get; set; }
        public float Time { get; set; }
    }

    public static float Lerp(IInterpolatorInput parameters)
    {
        var input = (LerpInput) parameters;
        //t is the proportion of the displacement we wish to apply
        var t = input.Time;

        //so we 'blend' t with the end value,
        var endWeight = t * input.End;

        //and t's inverse with the start value.
        var startWeight = (1f - t) * input.Start;

        //then combine their weights to finish the interpolation
        return startWeight + endWeight;
    }

    public static float ExponentialImpulse(float k, float t)
    {
        float h = t * k;
        return h * MathF.Exp(1.0f - h);
    }
}
