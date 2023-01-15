using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class CampUI: MonoBehaviour {
        [SerializeField] private Button _fightButton;

        public event Action FightButtonClicked;

        private void OnEnable() {
            _fightButton.onClick.AddListener(RaiseFightEvent);
        }

        private void OnDisable() {
            _fightButton.onClick.RemoveListener(RaiseFightEvent);
        }

        private void RaiseFightEvent() => FightButtonClicked?.Invoke();
    }
}