using System;
using UnityEngine;

namespace Gameplay.Utils {
    public class DestroyObserver: MonoBehaviour{
        private Collider _collider;
        
        public event Action<Collider> Destroyed;

        private void Awake() => _collider = GetComponent<Collider>();
        private void OnDestroy() => Destroyed?.Invoke(_collider);
    }

    [RequireComponent(typeof(Camera))]
    public class StatifFOV : MonoBehaviour {
        [SerializeField] [Range(0,360)] private float _fieldOfView;
        
        private Camera _camera;

        private void Awake() => _camera = GetComponent<Camera>();

        private void Update() => _camera.fieldOfView = _fieldOfView;
    }
}