using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveFunctions
{
    public static partial class Interpolator
    {
        public class ExponentialImpulseInput : IInterpolatorInput
        {
            public float Velocity { get; set; }
            public float Magnitude { get; set; }
            public float Time { get; set; }
        }


        public static float ExponentialImpulse(float velocity, float magnitude, float time)
        {
            float h = velocity * time;
            float result = h * MathF.Exp(1.0f - h);
            return result * magnitude;
        }
        public static float ExponentialImpulse(IInterpolatorInput parameters)
        {
            var input = (ExponentialImpulseInput)parameters;
            return ExponentialImpulse(input.Velocity, input.Magnitude, input.Time);
        }
    }
}
