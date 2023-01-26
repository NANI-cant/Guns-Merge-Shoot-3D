using Architecture.Services.Gameplay;
using Architecture.Services.Gameplay.Impl;
using Gameplay.PlayerLogic.StateMachine;
using General.StateMachine;

namespace Architecture.StateMachine.States {
    public class FightState : State {
        private readonly GameStateMachine _gameStateMachine;
        private readonly StageService _stageService;
        private readonly PlayerPointer _playerPointer;
        private readonly ILevelProgressService _levelProgressService;

        public FightState(
            GameStateMachine gameStateMachine, 
            StageService stageService,
            PlayerPointer playerPointer,
            ILevelProgressService levelProgressService
        ) {
            _gameStateMachine = gameStateMachine;
            _stageService = stageService;
            _playerPointer = playerPointer;
            _levelProgressService = levelProgressService;
        }

        public override void Enter() {
            _stageService.SpawnStage(_levelProgressService.ActiveStage);
            _playerPointer.Player.StateMachine.TranslateTo<AttackState>();

            _stageService.Cleared += TranslateToRun;
        }

        public override void Exit() {
            _levelProgressService.NextStage();
            _stageService.Cleared -= TranslateToRun;
        }

        private void TranslateToRun() {
            _gameStateMachine.TranslateTo<TransitionState>();
        }
    }
}