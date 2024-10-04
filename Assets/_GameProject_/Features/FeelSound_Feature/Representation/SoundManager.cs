using Lofelt.NiceVibrations;
using MoreMountains.Tools;
using UnityEngine;

namespace WKosArch.Sound_Feature
{
    public class SoundManager : MonoBehaviour
    {
        private const string PrefabPath = "[SOUND_MANGER]";

        public MMSoundManager MMSoundManager;
        [Space]
        public SFXFeedbackHolder SFXHolder;
        [Space]
        public MusicPlayer MusicPlayer;
        [Space]
        public UIFeedbackHolder UIHolder;
        [Space]
        public HapticReceiver HapticReceiver;


        private Vector3 _cashPosition = Vector3.zero;

        public static SoundManager CreateInstance()
        {
            var prefab = Resources.Load<SoundManager>(PrefabPath);
            var soundManager = Instantiate(prefab);


            DontDestroyOnLoad(soundManager.gameObject);

            return soundManager;
        }

        private void Awake() =>
            DontDestroyOnLoad(gameObject);

        private void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (Camera.main != null)
                transform.position = Camera.main.transform.position;
        }
    }
}