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
        public class AlmostIdentityInput : IInterpolatorInput
        {
            //the value when the input (time) is zero.
            public float Floor { get; set; }

            //any input larger than Threshold remains unchanged.
            public float Threshold { get; set; }

            public float Magnitude { get; set; }

            //the input value, assumed to be time.
            public float Time { get; set; }
        }

        //floor is the value when the signal is zero.
        //anything larger than "threshold" is unchanged
        public static float AlmostIdentity(float floor, float threshold, float magnitude, float time)
        {
            time = time * magnitude;
            if (time > threshold) return time;

            float a = 2.0f * floor - threshold;
            float b = 2.0f * threshold - 3.0f * floor;
            float t = time / threshold;

            return (a * t + b) * t * t + floor;
        }
        public static float AlmostIdentity(IInterpolatorInput parameters)
        {
            var input = (AlmostIdentityInput)parameters;
            return AlmostIdentity(input.Floor, input.Threshold, input.Magnitude, input.Time);
        }

    }
}
