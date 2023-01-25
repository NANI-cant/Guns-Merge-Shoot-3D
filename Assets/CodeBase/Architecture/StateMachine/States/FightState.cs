using Architecture.Services.Gameplay;
using Gameplay.PlayerLogic.StateMachine;
using General.StateMachine;

namespace Architecture.StateMachine.States {
    public class FightState : State {
        private readonly GameStateMachine _gameStateMachine;
        private readonly EnemySpawnService _enemySpawnService;
        private readonly PlayerPointer _playerPointer;
        private readonly LevelProgress _levelProgress;

        public FightState(
            GameStateMachine gameStateMachine, 
            EnemySpawnService enemySpawnService,
            PlayerPointer playerPointer,
            LevelProgress levelProgress
        ) {
            _gameStateMachine = gameStateMachine;
            _enemySpawnService = enemySpawnService;
            _playerPointer = playerPointer;
            _levelProgress = levelProgress;
        }

        public override void Enter() {
            _enemySpawnService.SpawnWave(_levelProgress.WaveData);
            _playerPointer.Player.StateMachine.TranslateTo<AttackState>();

            _enemySpawnService.Cleared += TranslateToRun;
        }

        public override void Exit() {
            _levelProgress.NextWave();
            _enemySpawnService.Cleared -= TranslateToRun;
        }

        private void TranslateToRun() {
            _gameStateMachine.TranslateTo<TransitionState>();
        }
    }
}