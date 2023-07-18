using Scripts.Constants;
using Scripts.StaticData.Service;

namespace Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;

        public BootstrapState(IGameStateMachine stateMachine, SceneLoader sceneLoader, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;
        }

        public async void Enter()
        {
            await _staticData.Load();
            _sceneLoader.Load(Scenes.Initial, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }
    }
}