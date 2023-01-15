using Gameplay.Utils;
using Metric;
using UnityEngine;

namespace Architecture.Services.Factories {
    public interface IGameplayFactory {
        GameObject CreatePlayer(Vector3 position, Quaternion rotation);
        GameObject CreateEnemy(EnemyId enemyId, Vector3 position, Quaternion rotation);
    }
}