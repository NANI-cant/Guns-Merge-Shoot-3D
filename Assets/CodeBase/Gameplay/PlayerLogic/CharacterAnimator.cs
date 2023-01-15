using UnityEngine;

namespace Gameplay.PlayerLogic {
    public class CharacterAnimator: MonoBehaviour {
        [SerializeField] private Animator _animator;
        
        private readonly int AttackSpeedKey = Animator.StringToHash("AttackSpeed");

        public float AttackSpeed {
            get => _animator.GetFloat(AttackSpeedKey);
            set => _animator.SetFloat(AttackSpeedKey, value);
        }

        public void PlayAttack() => _animator.Play("Shoot");
        public void PlayIdle() => _animator.Play("Idle");
        public void PlayRun() => _animator.Play("Run");
    }
}