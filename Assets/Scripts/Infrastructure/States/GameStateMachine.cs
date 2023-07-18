using System;
using System.Collections.Generic;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Infrastructure.Services.SaveLoad;
using Scripts.StaticData.Service;
using Scripts.UI.Services.Factory;

namespace Scripts.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, IStaticDataService staticData,
            IPersistentProgressService progress, ISaveLoadService saveLoad, IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = 
                    new BootstrapState(this, sceneLoader, staticData),
                
                [typeof(LoadProgressState)] = 
                    new LoadProgressState(this, progress, saveLoad),
                
                [typeof(LoadMainMenuState)] = 
                    new LoadMainMenuState(this, sceneLoader),
                
                [typeof(MainMenuState)] = 
                    new MainMenuState(),
                
                [typeof(LoadLevelState)] = 
                    new LoadLevelState(this, sceneLoader, gameFactory, uiFactory),
                
                [typeof(GameLoopState)] = 
                    new GameLoopState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState newState = ChangeState<TState>();
            newState.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState newState = GetState<TState>();
            _activeState = newState;
            return newState;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}