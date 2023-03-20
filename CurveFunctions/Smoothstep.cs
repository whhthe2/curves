using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CurveFunctions
{
    public static partial class Interpolator
    {
        public class SmoothStepInput : IInterpolatorInput
        {
            public float Edge0 { get; set; }
            public float Edge1 { get; set; }

            public float Magnitude { get; set; }
            public float Time { get; set; }
        }

        public static float Smoothstep(float edge0, float edge1, float magnitude, float time)
        {         
            // scale to a number between 0 and 1
            var t = Math.Clamp((time - edge0) / (edge1 - edge0), 0f, 1f);

            return t * t * (3f - 2f * t) * magnitude;
        }
        public static float Smoothstep(IInterpolatorInput parameters)
        {
            var input = (SmoothStepInput)parameters;
            return Smoothstep(input.Edge0, input.Edge1, input.Magnitude, input.Time);
        }
    }
}
