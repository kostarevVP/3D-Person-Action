using UnityEngine;

namespace WKosArch.UI_Feature
{
    public class UILayerContainer : MonoBehaviour
    {
        [SerializeField] private UILayer _layer;

        public UILayer Layer => _layer;
    }
}