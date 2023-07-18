using Scripts.Infrastructure.States;

namespace Scripts.Infrastructure
{
    public class Game
    {
        public IGameStateMachine StateMachine { get; }

        public Game(IGameStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}