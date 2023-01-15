using Architecture.Services.Gameplay;
using Gameplay.PlayerLogic.StateMachine;
using Gameplay.Setup.SpawnPoints;
using General.StateMachine;

namespace Architecture.StateMachine.States {
    public class FightState : State {
        private readonly GameStateMachine _gameStateMachine;
        private readonly EnemySpawnService _enemySpawnService;
        private readonly PlayerPointer _playerPointer;

        public FightState(
            GameStateMachine gameStateMachine, 
            EnemySpawnService enemySpawnService,
            PlayerPointer playerPointer
        ) {
            _gameStateMachine = gameStateMachine;
            _enemySpawnService = enemySpawnService;
            _playerPointer = playerPointer;
        }

        public override void Enter() {
            _enemySpawnService.SpawnPack(4);
            _playerPointer.Player.StateMachine.TranslateTo<AttackState>();
        }
    }
}