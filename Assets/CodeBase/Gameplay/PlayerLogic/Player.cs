using Gameplay.HealthLogic;
using Gameplay.PlayerLogic.StateMachine;
using UnityEngine;

namespace Gameplay.PlayerLogic {
    public class Player: MonoBehaviour {
        private Health _health;
        private AutoAttacker _attacker;
        private Rotator _rotator;
        private CharacterAnimator _animator;

        public PlayerStateMachine StateMachine { get; private set; }

        private void Awake() {
            _health = GetComponent<Health>();
            _attacker = GetComponent<AutoAttacker>();
            _rotator = GetComponent<Rotator>();
            _animator = GetComponent<CharacterAnimator>();
            
            StateMachine = new PlayerStateMachine(_health, _attacker, _rotator, _animator);
        }

        private void Start() {
            StateMachine.TranslateTo<IdleState>();
        }
    }
}