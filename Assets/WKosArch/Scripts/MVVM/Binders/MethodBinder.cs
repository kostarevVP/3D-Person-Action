namespace WKosArch.MVVM.Binders
{
    public abstract class MethodBinder : Binder
    {
        protected string MethodName => PropertyName;
    }

    public abstract class PropertyBinder : Binder
    {
      
    }
}