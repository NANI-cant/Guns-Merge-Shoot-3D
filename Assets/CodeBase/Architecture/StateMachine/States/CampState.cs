using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Architecture.Services.Gameplay.Impl;
using Architecture.Services.General;
using Architecture.Services.PersistentProgress;
using Architecture.Services.PersistentProgress.Impl;
using Gameplay.PlayerLogic.StateMachine;
using General.StateMachine;
using UI;
using UI.Hud;
using UnityEngine;

namespace Architecture.StateMachine.States {
    public class CampState: State {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IDestroyProvider _destroyProvider;
        private readonly PlayerPointer _playerPointer;
        private readonly ILevelProgressService _levelProgressService;
        private readonly IPersistentProgressService _persistentProgressService;

        private CampUI _campUI;
        private GameObject _hud;

        public CampState(
            GameStateMachine gameStateMachine,
            IUIFactory uiFactory,
            IDestroyProvider destroyProvider,
            PlayerPointer playerPointer,
            ILevelProgressService levelProgressService,
            IPersistentProgressService persistentProgressService
        ) {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _destroyProvider = destroyProvider;
            _playerPointer = playerPointer;
            _levelProgressService = levelProgressService;
            _persistentProgressService = persistentProgressService;
        }
        
        public override void Enter() {
            _levelProgressService.NextLevel();
            _persistentProgressService.Save();
            _campUI = _uiFactory.CreateCampUI().GetComponent<CampUI>();
            _campUI.FightButtonClicked += TranslateToFight;
            _playerPointer.Player.StateMachine.TranslateTo<IdleState>();

            if (_hud != null) _destroyProvider.Destroy(_hud);
            
            _persistentProgressService.Load();
        }

        public override void Exit() {
            _campUI.FightButtonClicked -= TranslateToFight;
            _hud = _uiFactory.CreateHUD();
            _destroyProvider.Destroy(_campUI.gameObject);   
        }

        private void TranslateToFight() {
            _gameStateMachine.TranslateTo<FightState>();
        }
    }
}