using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Services.AssetProviding;
using Architecture.Services.Factories;
using Architecture.Services.General;
using Gameplay.HealthLogic;
using Gameplay.Setup.SpawnPoints;
using Metric.Levels;

namespace Architecture.Services.Gameplay {
    public class EnemySpawnService {
        private readonly IGameplayFactory _gameplayFactory;
        private readonly IEnemySpawnPoint[] _enemySpawnPoints;
        private readonly IRandomService _randomService;
        private readonly IMetricProvider _metricProvider;

        private int _remindedEnemies = 0;
        private List<IEnemySpawnPoint> _availablePoints = new();

        public event Action Cleared;

        public EnemySpawnService(
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
        
        public void SpawnWave(StandartWaveData waveData) {
            foreach (var enemyId in waveData.GetEnemies(_randomService, _metricProvider)) {
                var randomEnemySpawn = TakeSpawnPoint();
                var enemy = _gameplayFactory.CreateEnemy(enemyId, randomEnemySpawn.Position, randomEnemySpawn.Rotation);
                Track(enemy.GetComponent<Health>());
            }
        }

        private IEnemySpawnPoint TakeSpawnPoint() {
            if (_availablePoints.Count == 0) _availablePoints = _enemySpawnPoints.ToList();
            
            int chosenIndex = _randomService.Range(0, _availablePoints.Count);
            var randomEnemySpawn = _availablePoints[chosenIndex];
            _availablePoints.RemoveAt(chosenIndex);
            return randomEnemySpawn;
        }

        private void Track(Health health) {
            _remindedEnemies++;
            health.Died += Forget;
        }

        private void Forget(Health health) {
            health.Died -= Forget;
            _remindedEnemies--;
            if (_remindedEnemies == 0) Cleared?.Invoke();
        }
    }
}