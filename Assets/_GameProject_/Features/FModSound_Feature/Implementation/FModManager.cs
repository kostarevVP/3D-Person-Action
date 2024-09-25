using UnityEngine;

namespace FMod_Feature
{
    public class FModManager : MonoBehaviour
    {
        public IFModFeature FModFeature { get; set; }

        private const string PrefabPath = "[FModManager]";

        public static FModManager CreateInstnace()
        {
            var prefab = Resources.Load<FModManager>(PrefabPath);
            var fModManager = Instantiate(prefab);

            DontDestroyOnLoad(fModManager.gameObject);

            return fModManager;
        }
    }
}