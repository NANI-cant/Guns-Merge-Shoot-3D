using System;
using System.Collections.Generic;
using Gameplay.HealthLogic;
using General.StateMachine;

namespace Gameplay.PlayerLogic.StateMachine {
    public class PlayerStateMachine : General.StateMachine.StateMachine {
        private Dictionary<Type, State> _states;
        
        protected override Dictionary<Type, State> States => _states;

        public PlayerStateMachine(
            Health health,
            AutoAttacker attacker,
            Rotator rotator,
            CharacterAnimator animator
        ) {
            _states = new Dictionary<Type, State>() {
                [typeof(IdleState)] = new IdleState(attacker, rotator, animator),
                [typeof(AttackState)] = new AttackState(attacker, animator),
                [typeof(RunState)] = new RunState(),
                [typeof(DeathState)] = new DeathState(),
            };
        }
    }
}