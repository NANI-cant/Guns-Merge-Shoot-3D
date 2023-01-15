using Architecture.Services.AssetProviding.Impl;
using Architecture.Services.Factories.Impl;
using Architecture.Services.Gameplay;
using Architecture.StateMachine;
using Gameplay.Setup.SpawnPoints;
using Gameplay.Setup.SpawnPoints.Impl;
using Zenject;

namespace Architecture.Bootstrappers {
    public class GameplayBootstrapper: MonoInstaller {
        public override void InstallBindings() {
            BindPlayerSpawnPoint();
            BindEnemySpawnPoints();
            
            BindService<GameStateMachine>();
            
            BindService<GameplayFactory>();
            BindService<ResourcesPrefabProvider>();
            
            BindService<UIFactory>();
            BindService<ResourcesUIProvider>();
            
            BindService<ResourcesMetricProvider>();

            BindService<EnemySpawnService>();
            BindService<PlayerPointer>();
        }

        private void BindPlayerSpawnPoint() {
            var playerSpawnPoint = FindObjectOfType<PlayerSpawnPoint>();
            Container.Bind<IPlayerSpawnPoint>().FromInstance(playerSpawnPoint).AsSingle().NonLazy();
        }
        
        private void BindEnemySpawnPoints() {
            var enemySpawnPoint = FindObjectsOfType<EnemySpawnPoint>();
            foreach (var spawnPoint in enemySpawnPoint) {
                Container.Bind<IEnemySpawnPoint>().FromInstance(spawnPoint).NonLazy();    
            }
        } 

        private void BindService<TService>() 
            => Container.BindInterfacesAndSelfTo<TService>().AsSingle().NonLazy();
    }
}