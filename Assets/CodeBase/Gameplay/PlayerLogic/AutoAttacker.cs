using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Services.General;
using Gameplay.Utils;
using Gameplay.Utils.TriggerObservers;
using Metric;
using UnityEngine;

namespace Gameplay.PlayerLogic {
    public class AutoAttacker: MonoBehaviour {
        [SerializeField] private SphereTriggerObserver _trigger;
        
        private Team _team;
        private ITimeProvider _timeProvider;
        
        private float _coolDown;
        private float _damage;

        private List<AttackTarget> _targetsQueue = new ();
        private Timer _cooldownTimer;
        private bool _isReady = true;
        private bool _isOn = true;
        private Rotator _rotator;

        private AttackTarget Target => _targetsQueue.Count > 0 ? _targetsQueue[0] : null;

        public event Action Attacked;

        public void Construct(float coolDown, float radius, float damage, ITimeProvider timeProvider) {
            _coolDown = coolDown;
            _damage = damage;
            _timeProvider = timeProvider;
            _trigger.Radius = radius;
        }

        private void Awake() {
            _team = GetComponent<TeamHolder>().Team;
            _rotator = GetComponent<Rotator>();
        }

        private void Update() {
            _targetsQueue = _targetsQueue.OrderBy((at) => Vector3.Distance(at.Transform.position, transform.position)).ToList();
            
            _cooldownTimer?.Tick(_timeProvider.DeltaTime);
            if(!_isOn) return;
            if(Target == null) return;

            if(!_isReady) return;
            _rotator.LookRotate(Target.Transform.position);
            Attack(Target.Damageable);
        }

        private void OnEnable() {
            _trigger.Enter += ReactTriggerEnter;
            _trigger.Exit += ReactTriggerExit;
        }

        private void OnDisable() {
            _trigger.Enter -= ReactTriggerEnter;
            _trigger.Exit -= ReactTriggerExit;
        }

        private void Attack(IDamageable damageable){
            damageable.TakeDamage(_damage);
            Attacked?.Invoke();

            _isReady = false;
            _cooldownTimer = new Timer(_coolDown, () => _isReady = true);
        }

        public void TurnOn() => _isOn = true;
        public void TurnOff() => _isOn = false;

        private void ReactTriggerEnter(Collider other) {
            if (other.gameObject == gameObject) return;
            if (!TrySetupTarget(other, out AttackTarget potentialTarget)) return;
            
            _targetsQueue.Add(potentialTarget);
        }

        private void ReactTriggerExit(Collider other) {
            if(other.gameObject == gameObject) return;
            if (!TrySetupTarget(other, out AttackTarget potentialTarget)) return;
            
            _targetsQueue.Remove(potentialTarget);
        }

        private bool TrySetupTarget(Collider other, out AttackTarget potentialTarget) {
            potentialTarget = null;
            if (!other.TryGetComponent<IDamageable>(out var damageable)) return false;
            if (other.TryGetComponent<TeamHolder>(out var targetTeamHolder) && _team == targetTeamHolder.Team) return false;
            
            potentialTarget = new AttackTarget(damageable, other.transform);
            return true;
        }
    }
}