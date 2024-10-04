using WKosArch.Domain.Features;
using UnityEngine;

namespace WKosArch.AssetProvider_Feature
{
    public interface IAssetProviderFeature : IFeature
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path, Vector3 at, Quaternion rotaion);
        GameObject Load(string path);
        T Load<T>(string path) where T : Object;
        T[] LoadAll<T>(string path) where T : Object;
    } 
}
