namespace StateMachine_Feature
{
    public abstract class State : IState
    {
        public virtual void OnEnter() { }
        public virtual void Update() { }
        public virtual void FixUpdate() { }
        public virtual void OnExit() { }
    }
}