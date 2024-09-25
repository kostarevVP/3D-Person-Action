namespace WKosArch.MVVM.Binders
{
    public class FloatToStringUnityEventBinder : ConvertibleUnityEventerBinder<float, string>
    {
        protected override string ConvertValue(float newValue)
        {
            return $"{newValue:F0}";
        }
    }
}