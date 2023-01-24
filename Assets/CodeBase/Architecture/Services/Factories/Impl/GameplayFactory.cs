using Architecture.Services.AssetProviding;
using Architecture.Services.General;
using Architecture.Services.PersistentProgress;
using Gameplay.EnemyLogic;
using Gameplay.HealthLogic;
using Gameplay.PlayerLogic;
using Gameplay.PlayerLogic.Weapons;
using Metric;
using PersistentProgress;
using UnityEngine;

namespace Architecture.Services.Factories.Impl {
    public class GameplayFactory : IGameplayFactory {
        private readonly IPrefabProvider _prefabProvider;
        private readonly IInstantiateProvider _instantiateProvider;
        private readonly IMetricProvider _metricProvider;
        private readonly ITimeProvider _timeProvider;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _persistentProgressService;

        private Player _player;

        public GameplayFactory(
            IPrefabProvider prefabProvider,
            IInstantiateProvider instantiateProvider,
            IMetricProvider metricProvider,
            ITimeProvider timeProvider,
            IRandomService randomService,
            IPersistentProgressService persistentProgressService
        ) {
            _prefabProvider = prefabProvider;
            _instantiateProvider = instantiateProvider;
            _metricProvider = metricProvider;
            _timeProvider = timeProvider;
            _randomService = randomService;
            _persistentProgressService = persistentProgressService;
        }
        
        public GameObject CreatePlayer(Vector3 position, Quaternion rotation) {
            var metric = _metricProvider.PlayerMetric;
            var player = _instantiateProvider.Instantiate(_prefabProvider.Player, position, rotation);

            player.GetComponent<AutoAttacker>().Construct(metric.AttackRadius, _timeProvider, _randomService);
            player.GetComponent<WeaponHolder>().Construct(_instantiateProvider, _metricProvider);
            player.GetComponent<Health>().Construct(metric.MaxHealth);
            
            SubscribeToProgress(player);
            
            _player = player.GetComponent<Player>();
            return player;
        }

        public GameObject CreateEnemy(EnemyId enemyId, Vector3 position, Quaternion rotation) {
            var metric = _metricProvider.EnemyMetric(enemyId);
            var enemy = _instantiateProvider.Instantiate(_prefabProvider.Enemy(enemyId), position, rotation);

            enemy.GetComponent<Mover>().Construct(metric.MovementSpeed, metric.AttackRadius, _player.transform, _timeProvider);
            enemy.GetComponent<Health>().Construct(metric.MaxHealth);
            enemy.GetComponent<AutoAttacker>().Construct(1/metric.AttackSpeed, metric.AttackRadius, metric.Damage, _timeProvider, _randomService);
            enemy.GetComponent<CharacterAnimator>().AttackSpeed = metric.AttackSpeed;
            
            return enemy;
        }
        
        private void SubscribeToProgress(GameObject gameObject) {
            foreach (var reader in gameObject.GetComponentsInChildren<IProgressReader>()) {
                _persistentProgressService.AddReader(reader);
            }
            
            foreach (var writer in gameObject.GetComponentsInChildren<IProgressWriter>()) {
                _persistentProgressService.AddWriter(writer);
            }
        }
    }
}