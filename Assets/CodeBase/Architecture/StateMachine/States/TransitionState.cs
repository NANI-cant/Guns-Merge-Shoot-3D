using Architecture.Services.Gameplay;
using Architecture.Services.Gameplay.Impl;
using Gameplay.PlayerLogic.StateMachine;
using General.StateMachine;

namespace Architecture.StateMachine.States {
    public class TransitionState : State {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ILevelProgressService _levelProgressService;
        private readonly PlayerPointer _playerPointer;

        public TransitionState(
            GameStateMachine gameStateMachine,
            ILevelProgressService levelProgressService,
            PlayerPointer playerPointer
        ) {
            _gameStateMachine = gameStateMachine;
            _levelProgressService = levelProgressService;
            _playerPointer = playerPointer;
        }
        
        public override void Enter() {
            _playerPointer.Player.StateMachine.TranslateTo<RunState>();
            _levelProgressService.TranslateToNextStage(() => {
                if (_levelProgressService.IsLevelOver) {
                    _levelProgressService.NextLevel();
                    _gameStateMachine.TranslateTo<CampState>();
                }
                else {
                    _gameStateMachine.TranslateTo<FightState>();
                }
            });
        }
    }
}