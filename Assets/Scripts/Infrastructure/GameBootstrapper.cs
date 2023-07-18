using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Instantiating;
using Scripts.Infrastructure.Services.ContainerService;
using Scripts.Infrastructure.Services.CoroutineRunner;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Infrastructure.Services.SaveLoad;
using Scripts.Infrastructure.States;
using Scripts.Services.Randomizer;
using Scripts.StaticData.Service;
using Scripts.UI.Services.Factory;
using Scripts.UI.Services.Screens;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure
{
    public class GameBootstrapper : MonoInstaller, ICoroutineRunner
    {
        private Game _game;

        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;

            BindServices();

            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

            Container.Bind<Game>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<ContainerService>().AsSingle();
            Container.Bind<ICoroutineRunner>().FromMethod(GetCoroutineRunner).AsSingle();
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<IInstantiateService>().To<ZenjectInstantiateService>().AsSingle();
            Container.Bind<IAssets>().To<AddressableAssets>().AsSingle();

            PersistentProgressService progressService = new();
            Container.Bind<IPersistentProgressService>().FromInstance(progressService).AsSingle();
            Container.Bind<IReadonlyProgressService>().FromInstance(progressService).AsSingle();
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IUIFactory>().To<ZenjectUIFactory>().AsSingle();
            Container.Bind<IScreenService>().To<ScreenService>().AsSingle();
            Container.Bind<IGameFactory>().To<ZenjectGameFactory>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
        }

        public override void Start()
        {
            _game = Container.Resolve<Game>();
            _game.StateMachine.Enter<BootstrapState>();
        }

        private static ICoroutineRunner GetCoroutineRunner()
        {
            CoroutineRunner coroutineRunner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(coroutineRunner);
            return coroutineRunner;
        }
    }
}