using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Architecture.Services.General;
using Gameplay.PlayerLogic.StateMachine;
using General.StateMachine;
using UI;

namespace Architecture.StateMachine.States {
    public class CampState: State {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IDestroyProvider _destroyProvider;
        private readonly PlayerPointer _playerPointer;
        private readonly LevelProgress _levelProgress;

        private CampUI _campUI;

        public CampState(
            GameStateMachine gameStateMachine,
            IUIFactory uiFactory,
            IDestroyProvider destroyProvider,
            PlayerPointer playerPointer,
            LevelProgress levelProgress
        ) {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _destroyProvider = destroyProvider;
            _playerPointer = playerPointer;
            _levelProgress = levelProgress;
        }
        
        public override void Enter() {
            _levelProgress.NextLevel();
            _campUI = _uiFactory.CreateCampUI().GetComponent<CampUI>();
            _campUI.FightButtonClicked += TranslateToFight;
            _playerPointer.Player.StateMachine.TranslateTo<IdleState>();
        }

        public override void Exit() {
            _campUI.FightButtonClicked -= TranslateToFight;
            _destroyProvider.Destroy(_campUI.gameObject);   
        }

        private void TranslateToFight() {
            _gameStateMachine.TranslateTo<FightState>();
        }
    }
}