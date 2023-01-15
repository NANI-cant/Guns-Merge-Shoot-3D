using Architecture.Services.AssetProviding;
using Architecture.Services.General;
using Gameplay.EnemyLogic;
using Gameplay.HealthLogic;
using Gameplay.PlayerLogic;
using Gameplay.Utils;
using Metric;
using UnityEngine;

namespace Architecture.Services.Factories.Impl {
    public class GameplayFactory : IGameplayFactory {
        private readonly IPrefabProvider _prefabProvider;
        private readonly IInstantiateProvider _instantiateProvider;
        private readonly IMetricProvider _metricProvider;
        private readonly ITimeProvider _timeProvider;

        private Player _player;

        public GameplayFactory(
            IPrefabProvider prefabProvider,
            IInstantiateProvider instantiateProvider,
            IMetricProvider metricProvider,
            ITimeProvider timeProvider
        ) {
            _prefabProvider = prefabProvider;
            _instantiateProvider = instantiateProvider;
            _metricProvider = metricProvider;
            _timeProvider = timeProvider;
        }
        
        public GameObject CreatePlayer(Vector3 position, Quaternion rotation) {
            var metric = _metricProvider.PlayerMetric;
            var player = _instantiateProvider.Instantiate(_prefabProvider.Player, position, rotation);

            player.GetComponent<AutoAttacker>().Construct(1, metric.AttackRadius, 10, _timeProvider);
            player.GetComponent<Health>().Construct(metric.MaxHealth);
            player.GetComponent<CharacterAnimator>();

            _player = player.GetComponent<Player>();
            return player;
        }

        public GameObject CreateEnemy(EnemyId enemyId, Vector3 position, Quaternion rotation) {
            var metric = _metricProvider.EnemyMetric(enemyId);
            var enemy = _instantiateProvider.Instantiate(_prefabProvider.Enemy(enemyId), position, rotation);

            enemy.GetComponent<Mover>().Construct(metric.MovementSpeed, metric.AttackRadius, _player.transform, _timeProvider);
            enemy.GetComponent<Health>().Construct(metric.MaxHealth);
            enemy.GetComponent<AutoAttacker>().Construct(1, metric.AttackRadius, metric.Damage, _timeProvider);
            
            return enemy;
        }
    }
}