using System;
using System.Collections.Generic;
using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Architecture.Services.General;
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
            LevelProgress levelProgress
        ) {
            _states = new Dictionary<Type, State>() {
                [typeof(InitializeState)] = new InitializeState(this, gameplayFactory, playerSpawnPoint, playerPointer),
                [typeof(CampState)] = new CampState(this, uiFactory, destroyProvider, playerPointer, levelProgress),       
                [typeof(FightState)] = new FightState(this, enemySpawnService, playerPointer, levelProgress),
                [typeof(TransitionState)] = new TransitionState(this, levelProgress),
                [typeof(LoseState)] = new LoseState(),
            };
        }

        public void Initialize() => TranslateTo<InitializeState>();
    }
}