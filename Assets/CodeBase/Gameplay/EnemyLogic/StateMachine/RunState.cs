using Gameplay.PlayerLogic;
using General.StateMachine;

namespace Gameplay.EnemyLogic.StateMachine {
    public class RunState : State {
        private readonly EnemyStateMachine _stateMachine;
        private readonly Mover _mover;
        private readonly AutoAttacker _attacker;
        private readonly CharacterAnimator _animator;

        public RunState(
            EnemyStateMachine stateMachine,
            Mover mover,
            AutoAttacker attacker,
            CharacterAnimator animator
        ) {
            _stateMachine = stateMachine;
            _mover = mover;
            _attacker = attacker;
            _animator = animator;
        }

        public override void Enter() {
            _attacker.TurnOff();
            _mover.enabled = true;
            _animator.PlayRun();
            _mover.Reached += TranslateToAttack;
        }

        public override void Exit() {
            _mover.Reached -= TranslateToAttack;
        }

        private void TranslateToAttack() => _stateMachine.TranslateTo<AttackState>();
    }
}