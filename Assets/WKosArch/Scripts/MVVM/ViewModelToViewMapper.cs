using System.Collections.Generic;
using UnityEngine;

namespace WKosArch.MVVM
{
    public class ViewModelToViewMapper : MonoBehaviour
    {
        [SerializeField] private List<ViewModelToViewMapping> _prefabMappings;
        [SerializeField] private View _prefabByDefault;

        private readonly Dictionary<string, View> _mappings = new();

        private void Awake()
        {
            foreach (var prefabMapping in _prefabMappings)
            {
                _mappings.TryAdd(prefabMapping.Key, prefabMapping.Value);
            }
        }

        public View GetPrefab(string viewModelTypeFullName)
        {
            if (_mappings.TryGetValue(viewModelTypeFullName, out var value))
            {
                return value;
            }

            return _prefabByDefault;
        }
    }
}