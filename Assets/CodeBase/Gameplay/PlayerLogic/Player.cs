using Gameplay.HealthLogic;
using Gameplay.PlayerLogic.StateMachine;
using Gameplay.PlayerLogic.Weapons;
using UnityEngine;

namespace Gameplay.PlayerLogic {
    [RequireComponent(typeof(WeaponHolder))]
    [RequireComponent(typeof(AutoAttacker))]
    public class Player: MonoBehaviour {
        public Health Health { get; private set; }
        public AutoAttacker Attacker { get; private set; }
        public Rotator Rotator { get; private set; }
        public CharacterAnimator Animator { get; private set; }
        public WeaponHolder WeaponHolder { get; private set; }

        public PlayerStateMachine StateMachine { get; private set; }

        private void Awake() {
            Health = GetComponent<Health>();
            Attacker = GetComponent<AutoAttacker>();
            Rotator = GetComponent<Rotator>();
            Animator = GetComponent<CharacterAnimator>();
            WeaponHolder = GetComponent<WeaponHolder>();
            
            StateMachine = new PlayerStateMachine(Health, Attacker, Rotator, Animator);
        }

        private void Start() {
            StateMachine.TranslateTo<IdleState>();
        }
    }
}