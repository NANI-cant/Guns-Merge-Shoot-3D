using System;
using System.Collections.Generic;
using Gameplay.PlayerLogic;
using General.StateMachine;

namespace Gameplay.EnemyLogic.StateMachine {
    public class EnemyStateMachine: General.StateMachine.StateMachine {
        private Dictionary<Type, State> _states;
        protected override Dictionary<Type, State> States => _states;

        public EnemyStateMachine(
            AutoAttacker attacker,
            CharacterAnimator animator,
            Mover mover
        ) {
            _states = new Dictionary<Type, State>() {
                [typeof(RunState)] = new RunState(this, mover, attacker, animator),
                [typeof(AttackState)] = new AttackState(attacker, mover, animator),
                [typeof(DeathState)] = new DeathState(),
            };
        }
    }
}