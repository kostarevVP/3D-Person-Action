namespace StateMachine_Feature
{
    public interface IState
    {
        void OnEnter();
        void Update();
        void FixUpdate();
        void OnExit();
    }
}