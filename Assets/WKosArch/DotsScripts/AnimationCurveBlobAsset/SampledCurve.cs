using Unity.Entities;
using Unity.Mathematics;

namespace WKosArch.Dots
{
    public partial struct SampledCurve
    {
        public BlobArray<float> KeyframeTimes;
        public int TimePoint;

        public float GetKeyframeValue(float time)
        {
            var approxTimeIndex = (TimePoint - 1) * time;
            var timeIndexBelow = (int)math.floor(approxTimeIndex);
            if (timeIndexBelow >= TimePoint - 1)
            {
                return KeyframeTimes[TimePoint - 1];
            }
            var indexRemainder = approxTimeIndex - timeIndexBelow;
            return math.lerp(KeyframeTimes[timeIndexBelow], KeyframeTimes[timeIndexBelow + 1], indexRemainder);
        }
    }
}