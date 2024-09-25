using StateMachine_Feature;

public interface ITransition
{
    IState To { get; }
    IPredicate Condition { get; }
}
