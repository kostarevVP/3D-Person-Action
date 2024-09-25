using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WKosArch.MVVM.Binders;
using UnityEditor;

namespace WKosArch.MVVM.Editor
{
    [CustomEditor(typeof(GenericPropertyBinder), true)]
    public class ReactivePropertyBinderEditor : PropertyBinderEditor
    {
        private GenericPropertyBinder _genericPropertyBinder;

        protected override void OnEnable()
        {
            base.OnEnable();

            _genericPropertyBinder = (GenericPropertyBinder)target;
        }

        protected override IEnumerable<PropertyInfo> GetPropertiesInfo()
        {
            var viewModelType = GetViewModelType(ViewModelTypeFullName.stringValue);
            var requiredType = _genericPropertyBinder.ParameterType;

            var allProperties = viewModelType.GetProperties()
                 .Where(propertyInfo =>
                 {
                     var parameterType = propertyInfo.PropertyType;

                     return propertyInfo.PropertyType == requiredType;
                 });

            return allProperties;
        }
    }
}
