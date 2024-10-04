using System.Collections.Generic;
using System;


namespace StateMachine_Feature
{
    public class StateMachine
    {
        private StateNode _currentNode;
        private Dictionary<Type, StateNode> _nodes = new();
        private HashSet<ITransition> _anyTransitions = new();

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.To);
            }

            _currentNode.State?.Update();
        }

        public void FixUpdate()
        {
            _currentNode.State?.FixUpdate();
        }

        public void SetState(IState state)
        {
            _currentNode = _nodes[state.GetType()];
            _currentNode.State?.OnEnter();
        }

        public void AddTrasition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        private StateNode GetOrAddNode(IState state)
        {
           var node = _nodes.GetValueOrDefault(state.GetType());

            if(node == null)
            {
                node = new StateNode(state);
                _nodes.Add(state.GetType(), node);
            }

            return node;
        }

        private void ChangeState(IState state)
        {
            if (state == _currentNode.State) return;

            var previousState = _currentNode.State;
            var nextState = _nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();

            _currentNode = _nodes[state.GetType()];
        }

        private ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition.Evaluate())
                    return transition;

            foreach (var transition in _currentNode.Transitions)
                if (transition.Condition.Evaluate())
                    return transition;

            return null;
        }


        class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}