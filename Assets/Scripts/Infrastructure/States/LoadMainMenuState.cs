using Scripts.Constants;

namespace Scripts.Infrastructure.States
{
    public class LoadMainMenuState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadMainMenuState(IGameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter() => 
            _sceneLoader.Load(Scenes.MainMenu, OnLoaded);

        public void Exit(){}

        private void OnLoaded() => 
            _stateMachine.Enter<MainMenuState>();
    }
}