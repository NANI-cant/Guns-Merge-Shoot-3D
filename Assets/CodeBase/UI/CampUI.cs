using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class CampUI: MonoBehaviour {
        [SerializeField] private Button _fightButton;
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private Button _arsenalButton;
        [SerializeField] private Arsenal.Arsenal _arsenal;

        public event Action FightButtonClicked;

        public Arsenal.Arsenal Arsenal => _arsenal;
        public Inventory.Inventory Inventory => _inventory;

        private void OnEnable() {
            _fightButton.onClick.AddListener(RaiseFightEvent);
            _arsenalButton.onClick.AddListener(HandleArsenalWindow);
        }

        private void OnDisable() {
            _fightButton.onClick.RemoveListener(RaiseFightEvent);
            _arsenalButton.onClick.RemoveListener(HandleArsenalWindow);
        }

        private void Start() {
            _fightButton.gameObject.SetActive(true);
            _arsenalButton.gameObject.SetActive(true);
            Inventory.gameObject.SetActive(true);
            
            Arsenal.gameObject.SetActive(false);
        }

        private void HandleArsenalWindow() => _arsenal.gameObject.SetActive(!_arsenal.gameObject.activeInHierarchy);
        private void RaiseFightEvent() => FightButtonClicked?.Invoke();
    }
}