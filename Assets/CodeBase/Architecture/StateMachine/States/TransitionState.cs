using General.StateMachine;

namespace Architecture.StateMachine.States {
    public class TransitionState : State {
        private readonly GameStateMachine _gameStateMachine;

        public TransitionState(GameStateMachine gameStateMachine) {
            _gameStateMachine = gameStateMachine;
        }
        
        public override void Enter() {
            _gameStateMachine.TranslateTo<FightState>();
        }
    }
}