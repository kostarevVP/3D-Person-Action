using UnityEngine;

namespace WKosArch.MVVM.Binders
{
    public class CloseWindowBinder : ObservableBinder<bool>
    {
        [SerializeField] private GameObject _destroyingGameObject;
        
        protected override void OnPropertyChanged(bool forced)
        {
            Destroy(_destroyingGameObject);
        }
        
#if UNITY_EDITOR
        private void Reset()
        {
            if (!_destroyingGameObject)
            {
                _destroyingGameObject = gameObject;
            }
        }
#endif
    }
}
