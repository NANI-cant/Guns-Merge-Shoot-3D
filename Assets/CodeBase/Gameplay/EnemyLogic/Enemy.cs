using System;
using Gameplay.EnemyLogic.StateMachine;
using Gameplay.HealthLogic;
using Gameplay.PlayerLogic;
using UnityEngine;

namespace Gameplay.EnemyLogic {
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CharacterAnimator))]
    [RequireComponent(typeof(AutoAttacker))]
    public class Enemy: MonoBehaviour{
        private Health _health;
        private CharacterAnimator _animator;
        private Mover _mover;
        private AutoAttacker _attacker;

        public EnemyStateMachine StateMachine { get; private set; }

        private void Awake() {
            _health = GetComponent<Health>();
            _animator = GetComponent<CharacterAnimator>();
            _attacker = GetComponent<AutoAttacker>();
            _mover = GetComponent<Mover>();

            StateMachine = new EnemyStateMachine(_attacker, _animator, _mover);
        }

        private void Start() {
            StateMachine.TranslateTo<RunState>();
        }

        private void OnEnable() => _health.Died += SelfDestroy;
        private void OnDisable() => _health.Died -= SelfDestroy;

        private void SelfDestroy(Health health) => Destroy(gameObject);
    }
}