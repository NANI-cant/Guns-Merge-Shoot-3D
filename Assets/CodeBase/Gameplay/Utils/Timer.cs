using System;

namespace Gameplay.Utils {
    public class Timer {
        private readonly Action _timesUpAction;
        private float _remindedTime;

        public bool IsAlreadyStop { get; private set; }

        public Timer(float time, Action timesUpAction) {
            _remindedTime = time;
            _timesUpAction = timesUpAction;
            IsAlreadyStop = false;
        }
		
        public void Tick(float deltaTime) {
            if(IsAlreadyStop) return;
			
            _remindedTime -= deltaTime;
            if (_remindedTime > 0) return;
			
            _timesUpAction?.Invoke();
            IsAlreadyStop = true;
        }
    }
}