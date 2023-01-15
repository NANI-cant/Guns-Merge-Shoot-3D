using Gameplay.Utils;
using Metric;
using UnityEngine;

namespace Architecture.Services.AssetProviding.Impl {
    public class ResourcesPrefabProvider : IPrefabProvider {
        private const string PlayerPath = "Gameplay/Player";
        private const string EnemiesFolderPath = "Gameplay/Enemies/";

        public GameObject Player => Resources.Load<GameObject>(PlayerPath);
        
        public GameObject Enemy(EnemyId enemyId) => Resources.Load<GameObject>(EnemiesFolderPath + enemyId);
    }
}