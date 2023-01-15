using Gameplay.Utils;
using Metric;
using UnityEngine;

namespace Architecture.Services.AssetProviding {
    public interface IPrefabProvider {
        GameObject Player { get; }
        GameObject Enemy(EnemyId enemyId);
    }
}