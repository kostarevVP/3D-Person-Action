using UnityEngine;

namespace WKosArch.Sound_Feature
{
    public class SoundConfig : ScriptableObject
    {
        public int Id { get; private set; }

        private void OnValidate()
        {
            Id = this.GetInstanceID();
        }
    }
}