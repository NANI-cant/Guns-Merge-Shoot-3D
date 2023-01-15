using System;
using Architecture.Services.General;
using UnityEngine;

namespace Gameplay.EnemyLogic {
    public class Mover: MonoBehaviour {
        private float _maxSpeed;
        private Transform _target;
        private Transform _transform;
        private ITimeProvider _timeProvider;
        private float _stopDistance;

        public event Action Reached;

        public bool IsReachTarget => Vector3.Distance(_transform.position, _target.position) <= _stopDistance; 

        private void Awake() => _transform = transform;

        public void Construct(float speed, float stopDistance, Transform target, ITimeProvider timeProvider) {
            _stopDistance = stopDistance;
            _maxSpeed = speed;
            _timeProvider = timeProvider;
            _target = target;
        }

        private void Update() {
            if(IsReachTarget) return;
            
            var direction = (_target.position - _transform.position).normalized;
            _transform.forward = direction;
            _transform.Translate(direction * _maxSpeed * _timeProvider.DeltaTime, Space.World);
            
            if(IsReachTarget) Reached?.Invoke(); 
        }
    }
}