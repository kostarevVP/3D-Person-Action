using WKosArch.Domain.Features;

namespace WKosArch.Analytics_Feature
{
    public interface IAnalyticsFeature : IFeature
    {
        void LogEvent(string name, string parameterName, string parameterValue);
        void LogEvent(string name, string parameterName, double parameterValue);
        void LogEvent(string name, string parameterName, long parameterValue);
        void LogEvent(string name, string parameterName, int parameterValue);
        void LogEvent(string name);
    } 
}
