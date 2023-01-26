using Architecture.Services.Gameplay;
using Architecture.Services.Gameplay.Impl;
using Gameplay.PlayerLogic.StateMachine;
using General.StateMachine;

namespace Architecture.StateMachine.States {
    public class FightState : State {
        private readonly GameStateMachine _gameStateMachine;
        private readonly EnemySpawnService _enemySpawnService;
        private readonly PlayerPointer _playerPointer;
        private readonly ILevelProgressService _levelProgressService;

        public FightState(
            GameStateMachine gameStateMachine, 
            EnemySpawnService enemySpawnService,
            PlayerPointer playerPointer,
            ILevelProgressService levelProgressService
        ) {
            _gameStateMachine = gameStateMachine;
            _enemySpawnService = enemySpawnService;
            _playerPointer = playerPointer;
            _levelProgressService = levelProgressService;
        }

        public override void Enter() {
            _enemySpawnService.SpawnWave(_levelProgressService.WayPointData);
            _playerPointer.Player.StateMachine.TranslateTo<AttackState>();

            _enemySpawnService.Cleared += TranslateToRun;
        }

        public override void Exit() {
            _levelProgressService.NextPoint();
            _enemySpawnService.Cleared -= TranslateToRun;
        }

        private void TranslateToRun() {
            _gameStateMachine.TranslateTo<TransitionState>();
        }
    }
}