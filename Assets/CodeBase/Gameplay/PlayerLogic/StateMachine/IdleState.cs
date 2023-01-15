using General.StateMachine;

namespace Gameplay.PlayerLogic.StateMachine {
    public class IdleState : State {
        private readonly AutoAttacker _attacker;
        private readonly Rotator _rotator;
        private readonly CharacterAnimator _animator;

        public IdleState(
            AutoAttacker attacker,
            Rotator rotator,
            CharacterAnimator animator
        ) {
            _attacker = attacker;
            _rotator = rotator;
            _animator = animator;
        }
        
        public override void Enter() {
            _attacker.TurnOff();
            _rotator.ResetRotation();
            _animator.PlayIdle();
        }
    }
}