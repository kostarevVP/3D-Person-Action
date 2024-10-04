using WKosArch.Domain.Features;

namespace WKosArch.Quality_Feature
{
	public interface IQualityFeature : IFeature
	{
		void SetFPSLimit(int targetFPS);
		void SetRenderPipeline(RenderingQuality renderingQuality);
	} 
}