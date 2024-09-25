using FMODUnity;
using UnityEngine;

namespace FMod_Feature
{
    public class FModListener : MonoBehaviour
    {
        private const string PrefabPath = "[FModListener]";

        private StudioListener _studioListener;

        public static FModListener CreateInstance()
        {
            var prefab = Resources.Load<FModListener>(PrefabPath);
            var fModListener = Instantiate(prefab);

            DontDestroyOnLoad(fModListener.gameObject);

            return fModListener;
        }

        private void Awake() =>
            _studioListener = GetComponent<StudioListener>();

        private void Update() =>
            UpdatePosition();

        private void UpdatePosition()
        {
            if (Camera.main != null)
                transform.position = Camera.main.transform.position;
        }
    } 
}
