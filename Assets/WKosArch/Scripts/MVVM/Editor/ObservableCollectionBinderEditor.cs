using System;
using WKosArch.MVVM.Binders;
using WKosArch.Reactive;
using UnityEditor;

namespace WKosArch.MVVM.Editor
{
    [CustomEditor(typeof(ObservableCollectionBinder), true)]
    public class ObservableCollectionBinderEditor : ObservableBinderBase
    {
        private ObservableCollectionBinder _observableBinder;
        protected override SerializedProperty _propertyName { get; set; }

        protected override void OnEnable()
        {
            base.OnEnable();

            _observableBinder = (ObservableCollectionBinder)target;
            _propertyName = serializedObject.FindProperty(nameof(_propertyName));
        }

        protected override bool IsValidProperty(Type propertyType)
        {
            var requiredArgumentType = _observableBinder.ArgumentType;
            var requiredType = typeof(IReadOnlyReactiveCollection<>);

            return IsValidProperty(propertyType, requiredType, requiredArgumentType);
        }
    }
}