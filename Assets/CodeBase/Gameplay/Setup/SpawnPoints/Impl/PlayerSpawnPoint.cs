using UnityEngine;

namespace Gameplay.Setup.SpawnPoints.Impl {
    public class PlayerSpawnPoint :MonoBehaviour, IPlayerSpawnPoint {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        
#if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(Position,1f);
        }
#endif
    }
}