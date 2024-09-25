using WKosArch.MVVM.Binders;
using UnityEditor;

namespace WKosArch.MVVM.Editor
{
    [CustomEditor(typeof(EmptyBinder), true)]
    public class EmptyBinderEditor : BinderEditor
    {
        protected override void DrawProperties()
        {
           
        }
    }
}
