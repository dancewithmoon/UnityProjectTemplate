namespace Scripts.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameStateMachine _stateMachine;

        public GameLoopState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
        }
    }
}