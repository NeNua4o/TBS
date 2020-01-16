using System.Collections.Generic;

namespace ClientV2.FSM
{
    public class EmptyState : IState
    {
        public void Update(float dt) { }
        public void Render() { }
        public void HandleInput() { }
        public void Enter(StateMachine sm) { }
        public void Exit() { }
    }

    public class StateMachine
    {
        Stack<IState> _statesQueue = new Stack<IState>();
        IState _current = new EmptyState();

        public IState Current { get { return _current; } }

        public void Push(IState state)
        {
            _current.Exit();
            _statesQueue.Push(_current);
            _current = state;
            _current.Enter(this);
        }

        public void Set(IState state)
        {
            _current.Exit();
            _current = state;
            _current.Enter(this);
        }
        
        public void Pop()
        {
            _current.Exit();
            _current = _statesQueue.Pop();
            _current.Enter(this);
        }

        public void Clear()
        {
            _statesQueue.Clear();
        }

        public void Update(float dt)
        {
            _current.Update(dt);
        }

        public void Render()
        {
            _current.Render();
        }

        public void HandleInput()
        {
            _current.HandleInput();
        }
    }
}
