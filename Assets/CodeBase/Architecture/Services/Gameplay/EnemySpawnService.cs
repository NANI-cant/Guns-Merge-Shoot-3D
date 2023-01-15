using System;
using Architecture.Services.AssetProviding;
using Architecture.Services.Factories;
using Architecture.Services.General;
using Gameplay.HealthLogic;
using Gameplay.Setup.SpawnPoints;
using Gameplay.Utils;
using Metric.Levels;

namespace Architecture.Services.Gameplay {
    public class EnemySpawnService {
        private readonly IGameplayFactory _gameplayFactory;
        private readonly IEnemySpawnPoint[] _enemySpawnPoints;
        private readonly IRandomService _randomService;
        private readonly IMetricProvider _metricProvider;

        private int _remindedEnemies = 0;

        public event Action Cleared;

        public EnemySpawnService(
            IGameplayFactory gameplayFactory,
            IEnemySpawnPoint[] enemySpawnPoints,
            IRandomService randomService
        ) {
            _gameplayFactory = gameplayFactory;
            _enemySpawnPoints = enemySpawnPoints;
            _randomService = randomService;
        }
        
        public void SpawnWave(WaveEnemy[] waveEnemies) {
            foreach (var waveEnemy in waveEnemies) {
                for (int i = 0; i < waveEnemy.Count; i++) {
                    var randomEnemySpawn = _enemySpawnPoints[_randomService.Range(0,_enemySpawnPoints.Length)];
                    var enemy = _gameplayFactory.CreateEnemy(waveEnemy.Enemy, randomEnemySpawn.Position, randomEnemySpawn.Rotation);
                    Track(enemy.GetComponent<Health>());
                }
            }
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