using System;

namespace WKosArch.MVVM.Binders
{
    public abstract class GenericPropertyBinder : PropertyBinder
    {
        public abstract Type ParameterType { get; }
    }
}