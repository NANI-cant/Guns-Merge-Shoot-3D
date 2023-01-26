using System;
using System.Collections.Generic;
using Architecture.Services.AssetProviding;
using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Architecture.Services.Gameplay.Impl;
using Architecture.Services.General;
using Architecture.Services.PersistentProgress;
using Architecture.StateMachine.States;
using Gameplay.Setup.SpawnPoints;
using General.StateMachine;
using Zenject;

namespace Architecture.StateMachine {
    public class GameStateMachine: General.StateMachine.StateMachine, IInitializable {
        private readonly Dictionary<Type, State> _states;
        
        protected override Dictionary<Type, State> States => _states;

        public GameStateMachine(IGameplayFactory gameplayFactory,
            IPlayerSpawnPoint playerSpawnPoint,
            IUIFactory uiFactory,
            IDestroyProvider destroyProvider,
            EnemySpawnService enemySpawnService,
            PlayerPointer playerPointer,
            ILevelProgressService levelProgressService,
            IMetricProvider metricProvider, 
            IPersistentProgressService persistentProgressService
        ) {
            _states = new Dictionary<Type, State>() {
                [typeof(InitializeState)] = new InitializeState(this, gameplayFactory, playerSpawnPoint, playerPointer, metricProvider, persistentProgressService),
                [typeof(CampState)] = new CampState(this, uiFactory, destroyProvider, playerPointer, levelProgressService, persistentProgressService),       
                [typeof(FightState)] = new FightState(this, enemySpawnService, playerPointer, levelProgressService),
                [typeof(TransitionState)] = new TransitionState(this, levelProgressService),
                [typeof(LoseState)] = new LoseState(),
            };
        }

        public void Initialize() => TranslateTo<InitializeState>();
    }
}