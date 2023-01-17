using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class CampUI: MonoBehaviour {
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _arsenalButton;
        [SerializeField] private Arsenal _arsenal;

        public event Action FightButtonClicked;

        public Arsenal Arsenal => _arsenal;

        private void OnEnable() {
            _fightButton.onClick.AddListener(RaiseFightEvent);
            _arsenalButton.onClick.AddListener(HandleArsenalWindow);
        }

        private void OnDisable() {
            _fightButton.onClick.RemoveListener(RaiseFightEvent);
            _arsenalButton.onClick.RemoveListener(HandleArsenalWindow);
        }

        private void HandleArsenalWindow() => _arsenal.gameObject.SetActive(!_arsenal.gameObject.activeInHierarchy);
        private void RaiseFightEvent() => FightButtonClicked?.Invoke();
    }
}