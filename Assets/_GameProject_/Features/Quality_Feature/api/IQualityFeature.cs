using WKosArch.Domain.Features;

public interface IQualityFeature : IFeature
{
    void SetFPSLimit(int targetFPS);
    void SetRenderPipeline(RenderingQuality renderingQuality);
}