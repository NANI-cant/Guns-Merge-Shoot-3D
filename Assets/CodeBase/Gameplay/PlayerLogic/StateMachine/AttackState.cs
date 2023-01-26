using General.StateMachine;

namespace Gameplay.PlayerLogic.StateMachine {
    public class AttackState : State {
        private readonly AutoAttacker _attacker;
        private readonly CharacterAnimator _animator;

        public AttackState(
            AutoAttacker attacker,
            CharacterAnimator animator
        ) {
            _attacker = attacker;
            _animator = animator;
        }
        
        public override void Enter() {
            _animator.PlayIdle();
            _attacker.TurnOn();
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