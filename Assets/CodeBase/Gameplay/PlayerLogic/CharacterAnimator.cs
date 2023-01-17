using System;
using UnityEngine;

namespace Gameplay.PlayerLogic {
    public class CharacterAnimator: MonoBehaviour {
        private readonly int AttackSpeedKey = Animator.StringToHash("AttackSpeed");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private float _attackClipLength = 1;

        public AnimatorOverrideController Controller {
            get => _animator.runtimeAnimatorController as AnimatorOverrideController;
            set => _animator.runtimeAnimatorController = value;
        }

        public float AttackSpeed {
            get => _animator.GetFloat(AttackSpeedKey)/_attackClipLength;
            set => _animator.SetFloat(AttackSpeedKey, value * _attackClipLength);
        }

        public void PlayAttack() => _animator.Play("Attack");
        public void PlayIdle() => _animator.Play("Idle");
        public void PlayRun() => _animator.Play("Run");
    }
}