using WKosArch;
using UnityEngine;

namespace WKosArch.Services.UIService.Common
{
    public class UILayerContainer : MonoBehaviour
    {
        [SerializeField] private UILayer _layer;

        public UILayer Layer => _layer;
    }
}