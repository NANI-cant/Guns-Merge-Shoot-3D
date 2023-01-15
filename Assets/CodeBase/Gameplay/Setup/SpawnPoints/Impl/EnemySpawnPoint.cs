using System;
using UnityEngine;

namespace Gameplay.Setup.SpawnPoints.Impl {
    public class EnemySpawnPoint : MonoBehaviour, IEnemySpawnPoint {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        
#if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(Position,1f);
        }
#endif
    }
}