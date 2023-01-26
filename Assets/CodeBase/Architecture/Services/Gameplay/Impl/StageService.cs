using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Services.AssetProviding;
using Architecture.Services.Factories;
using Architecture.Services.General;
using Gameplay.HealthLogic;
using Gameplay.Setup.SpawnPoints;
using Metric.Levels.Stages;

namespace Architecture.Services.Gameplay.Impl {
    public class StageService {
        private readonly IGameplayFactory _gameplayFactory;
        private readonly IEnemySpawnPoint[] _enemySpawnPoints;
        private readonly IRandomService _randomService;
        private readonly IMetricProvider _metricProvider;
        
        private List<IEnemySpawnPoint> _availablePoints = new();
        private int _remindedEnemies;
        private int _allEnemies;
        private StageData _trackedStage;

        public event Action Cleared;

        public StageService(
            IGameplayFactory gameplayFactory,
            IEnemySpawnPoint[] enemySpawnPoints,
            IRandomService randomService,
            IMetricProvider metricProvider
        ) {
            _gameplayFactory = gameplayFactory;
            _enemySpawnPoints = enemySpawnPoints;
            _randomService = randomService;
            _metricProvider = metricProvider;
        }

        public void SpawnStage(StageData activeStage) {
            _trackedStage = activeStage;
            foreach (var enemyId in activeStage.EnemyIds) {
                var spawnPoint = TakeSpawnPoint();
                var enemy = _gameplayFactory.CreateEnemy(enemyId, spawnPoint.Position, spawnPoint.Rotation);
                Track(enemy.GetComponent<Health>());
            }

            _remindedEnemies = _allEnemies = activeStage.EnemyIds.Length;
            UpdateStageProgress();
        }

        private IEnemySpawnPoint TakeSpawnPoint() {
            if (_availablePoints.Count == 0) _availablePoints = _enemySpawnPoints.ToList();
            
            int chosenIndex = _randomService.Range(0, _availablePoints.Count);
            var randomEnemySpawn = _availablePoints[chosenIndex];
            _availablePoints.RemoveAt(chosenIndex);
            return randomEnemySpawn;
        }

        private void Track(Health health) => health.Died += Forget;

        private void Forget(Health health) {
            health.Died -= Forget;
            
            _remindedEnemies--;
            UpdateStageProgress();
            if (_remindedEnemies == 0) Cleared?.Invoke();
        }

        private void UpdateStageProgress() => _trackedStage.Progress = 1 - (float)_remindedEnemies / _allEnemies;
    }
}