using Architecture.Services.Gameplay;
using General.StateMachine;

namespace Architecture.StateMachine.States {
    public class TransitionState : State {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ILevelProgressService _levelProgressService;

        public TransitionState(
            GameStateMachine gameStateMachine,
            ILevelProgressService levelProgressService
        ) {
            _gameStateMachine = gameStateMachine;
            _levelProgressService = levelProgressService;
        }
        
        public override void Enter() {
            if (_levelProgressService.IsLevelOver) {
                _gameStateMachine.TranslateTo<CampState>();
            }
            else {
                _gameStateMachine.TranslateTo<FightState>();    
            }
        }
    }
}