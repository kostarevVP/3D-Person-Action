namespace WKosArch
{
    public abstract class WindowViewModel : UiViewModel
    {
        public void BackToPreviousWindow() => 
            UI.Back();

        public void CloseAllWindow()
        {
            UI.CloseAllWindowInStack();
        }
    }
}
