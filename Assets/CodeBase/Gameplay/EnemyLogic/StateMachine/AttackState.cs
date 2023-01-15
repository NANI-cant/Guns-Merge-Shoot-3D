using Gameplay.PlayerLogic;
using General.StateMachine;

namespace Gameplay.EnemyLogic.StateMachine {
    public class AttackState : State {
        private readonly AutoAttacker _attacker;
        private readonly Mover _mover;
        private readonly CharacterAnimator _animator;

        public AttackState(
            AutoAttacker attacker,
            Mover mover,
            CharacterAnimator animator
        ) {
            _attacker = attacker;
            _mover = mover;
            _animator = animator;
        }

        public override void Enter() {
            _attacker.TurnOn();
            _mover.enabled = false;
            _animator.PlayIdle();

            _attacker.Attacked += PlayAnimation;
        }
        
        public override void Exit() {
            _attacker.Attacked -= PlayAnimation;
        }

        private void PlayAnimation() {
            _animator.PlayAttack();
        }
    }
}