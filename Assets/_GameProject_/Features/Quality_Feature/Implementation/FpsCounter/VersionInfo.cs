using UnityEngine;
using UnityEngine.UI;

namespace WKosArch.Quality_Feature
{
    public class VersionInfo : MonoBehaviour
    {
        private Text _text;

        void Start()
        {
            if (GetComponent<Text>() == null)
            {
                Debug.LogWarning("VersionInfo requires a GUIText component.");
                return;
            }
            _text = GetComponent<Text>();

            string appVersion = Application.version;

            _text.text = "V. " + appVersion;
        }
    }
}