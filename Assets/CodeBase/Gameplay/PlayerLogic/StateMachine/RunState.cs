using General.StateMachine;

namespace Gameplay.PlayerLogic.StateMachine {
    public class RunState : State {
        private readonly AutoAttacker _attacker;
        private readonly CharacterAnimator _animator;
        private readonly Rotator _rotator;

        public RunState(
            AutoAttacker attacker,
            CharacterAnimator animator,
            Rotator rotator
        ) {
            _attacker = attacker;
            _animator = animator;
            _rotator = rotator;
        }

        public override void Enter() {
            _rotator.ResetRotation();
            _attacker.TurnOff();
            _animator.PlayRun();
        }
    }
}