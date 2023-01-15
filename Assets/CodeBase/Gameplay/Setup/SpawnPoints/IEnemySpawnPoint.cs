using UnityEngine;

namespace Gameplay.Setup.SpawnPoints {
    public interface IEnemySpawnPoint {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
    }
}