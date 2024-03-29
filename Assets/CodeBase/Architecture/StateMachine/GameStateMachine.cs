﻿using System;
using System.Collections.Generic;
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
            StageService stageService,
            PlayerPointer playerPointer,
            ILevelProgressService levelProgressService, 
            IPersistentProgressService persistentProgressService
        ) {
            _states = new Dictionary<Type, State>() {
                [typeof(InitializeState)] = new InitializeState(this, gameplayFactory, playerSpawnPoint, playerPointer, persistentProgressService),
                [typeof(CampState)] = new CampState(this, uiFactory, destroyProvider, playerPointer, levelProgressService, persistentProgressService),       
                [typeof(FightState)] = new FightState(this, stageService, playerPointer, levelProgressService),
                [typeof(TransitionState)] = new TransitionState(this, levelProgressService, playerPointer),
                [typeof(LoseState)] = new LoseState(),
            };
        }

        public void Initialize() => TranslateTo<InitializeState>();
    }
}