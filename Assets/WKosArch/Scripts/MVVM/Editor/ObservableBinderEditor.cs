using System;
using WKosArch.MVVM.Binders;
using UnityEditor;
using R3;

namespace WKosArch.MVVM.Editor
{
    [CustomEditor(typeof(ObservableBinder), true)]
    public class ObservableBinderEditor : ObservableBinderBase
    {
        private ObservableBinder _observableBinder;
        protected override SerializedProperty _propertyName { get; set; }

        protected override void OnEnable()
        {
            base.OnEnable();

            _observableBinder = (ObservableBinder)target;
            _propertyName = serializedObject.FindProperty(nameof(_propertyName));
        }

        protected override bool IsValidProperty(Type propertyType)
        {
            var requiredArgumentType = _observableBinder.ArgumentType;
            var requiredType0 = typeof(IObservable<>);
            var requiredType1 = typeof(Observable<>);
            
            return IsValidProperty(propertyType, requiredType0, requiredArgumentType) || IsValidProperty(propertyType, requiredType1, requiredArgumentType);
        }
    }
}