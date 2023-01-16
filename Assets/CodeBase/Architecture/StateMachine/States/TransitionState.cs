using Architecture.Services.Gameplay;
using General.StateMachine;

namespace Architecture.StateMachine.States {
    public class TransitionState : State {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LevelProgress _levelProgress;

        public TransitionState(
            GameStateMachine gameStateMachine,
            LevelProgress levelProgress
        ) {
            _gameStateMachine = gameStateMachine;
            _levelProgress = levelProgress;
        }
        
        public override void Enter() {
            if (_levelProgress.IsLevelOver) {
                _gameStateMachine.TranslateTo<CampState>();
            }
            else {
                _gameStateMachine.TranslateTo<FightState>();    
            }
        }
    }
}