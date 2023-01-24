using Architecture.Services.AssetProviding;
using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Architecture.Services.PersistentProgress;
using Gameplay.PlayerLogic;
using Gameplay.Setup.SpawnPoints;
using General.StateMachine;

namespace Architecture.StateMachine.States {
    public class InitializeState : State {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameplayFactory _gameplayFactory;
        private readonly IPlayerSpawnPoint _playerSpawnPoint;
        private readonly PlayerPointer _playerPointer;
        private readonly IMetricProvider _metricProvider;
        private readonly IPersistentProgressService _persistentProgressService;

        public InitializeState(
            GameStateMachine gameStateMachine,
            IGameplayFactory gameplayFactory,
            IPlayerSpawnPoint playerSpawnPoint,
            PlayerPointer playerPointer,
            IMetricProvider metricProvider,
            IPersistentProgressService persistentProgressService
        ) {
            _gameStateMachine = gameStateMachine;
            _gameplayFactory = gameplayFactory;
            _playerSpawnPoint = playerSpawnPoint;
            _playerPointer = playerPointer;
            _metricProvider = metricProvider;
            _persistentProgressService = persistentProgressService;
        }
        
        public override void Enter() {
            _playerPointer.Player = _gameplayFactory
                .CreatePlayer(_playerSpawnPoint.Position, _playerSpawnPoint.Rotation)
                .GetComponent<Player>();
            _persistentProgressService.Load();
            
            _gameStateMachine.TranslateTo<CampState>();
        }
    }
}