using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Gameplay.PlayerLogic;
using Gameplay.Setup.SpawnPoints;
using General.StateMachine;

namespace Architecture.StateMachine.States {
    public class InitializeState : State {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameplayFactory _gameplayFactory;
        private readonly IPlayerSpawnPoint _playerSpawnPoint;
        private readonly PlayerPointer _playerPointer;

        public InitializeState(
            GameStateMachine gameStateMachine,
            IGameplayFactory gameplayFactory,
            IPlayerSpawnPoint playerSpawnPoint,
            PlayerPointer playerPointer
        ) {
            _gameStateMachine = gameStateMachine;
            _gameplayFactory = gameplayFactory;
            _playerSpawnPoint = playerSpawnPoint;
            _playerPointer = playerPointer;
        }
        
        public override void Enter() {
            _playerPointer.Player = _gameplayFactory
                .CreatePlayer(_playerSpawnPoint.Position, _playerSpawnPoint.Rotation)
                .GetComponent<Player>();
            _gameStateMachine.TranslateTo<CampState>();
        }
    }
}