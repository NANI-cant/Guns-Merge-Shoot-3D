using UnityEngine;

namespace Gameplay.PlayerLogic {
    public class Rotator : MonoBehaviour {
        public void LookRotate(Vector3 target) {
            transform.forward = (target - transform.position).normalized;
        }

        public void ResetRotation() {
            transform.rotation = Quaternion.identity;
        }
    }
}