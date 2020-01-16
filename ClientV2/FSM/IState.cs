namespace ClientV2.FSM
{
    public interface IState
    {
        void Update(float dt);
        void HandleInput();
        void Render();

        void Enter(StateMachine sm);
        void Exit();
    }
}
