using Architecture.Services.AssetProviding;
using Architecture.Services.Factories;
using Architecture.Services.General;
using Gameplay.Setup.SpawnPoints;
using Metric.Levels;

namespace Architecture.Services.Gameplay {
    public class EnemySpawnService {
        private readonly IGameplayFactory _gameplayFactory;
        private readonly IEnemySpawnPoint[] _enemySpawnPoints;
        private readonly IRandomService _randomService;
        private readonly IMetricProvider _metricProvider;

        public EnemySpawnService(
            IGameplayFactory gameplayFactory,
            IEnemySpawnPoint[] enemySpawnPoints,
            IRandomService randomService
        ) {
            _gameplayFactory = gameplayFactory;
            _enemySpawnPoints = enemySpawnPoints;
            _randomService = randomService;
        }
        

        public void SpawnPack(Pack enemyPack) {
            var ids = enemyPack.EnemyIds;
            for (int i = 0; i < ids.Length; i++) {
                int spawnPoint = _randomService.Range(0, _enemySpawnPoints.Length);
                var enemyId = ids[i];

                _gameplayFactory.CreateEnemy(
                    enemyId, 
                    _enemySpawnPoints[spawnPoint].Position,
                    _enemySpawnPoints[spawnPoint].Rotation
                );
            }
        }
    }
}