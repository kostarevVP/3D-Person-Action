using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace WKosArch.Dots
{
    public partial struct SampledCurve
    {
        public static class SampledCurveFactory
        {
            public static BlobAssetReference<SampledCurve> CreateSampledCurve(AnimationCurve curve, int numberOfSamples)
            {
                using var blobBuilder = new BlobBuilder(Allocator.Temp);
                ref var sampledCurve = ref blobBuilder.ConstructRoot<SampledCurve>();
                var keyframeTimes = blobBuilder.Allocate(ref sampledCurve.KeyframeTimes, numberOfSamples);
                sampledCurve.TimePoint = numberOfSamples;

                for (var i = 0; i < numberOfSamples; i++)
                {
                    var samplePoint = (float)i / (numberOfSamples - 1);
                    var sampleValue = curve.Evaluate(samplePoint);
                    keyframeTimes[i] = sampleValue;
                }

                return blobBuilder.CreateBlobAssetReference<SampledCurve>(Allocator.Temp);
            }
        }
    }
}