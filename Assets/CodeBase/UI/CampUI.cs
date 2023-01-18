using System;
using Architecture.Services.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class CampUI: MonoBehaviour {
        [SerializeField] private Button _fightButton;
        [SerializeField] private Transform _mergeGrid;
        [SerializeField] private Button _arsenalButton;
        [SerializeField] private Arsenal _arsenal;
        
        private IUIFactory _uiFacttory;

        public event Action FightButtonClicked;

        public Arsenal Arsenal => _arsenal;

        public void Construct(IUIFactory uiFactory) {
            _uiFacttory = uiFactory;
        }

        private void OnEnable() {
            _fightButton.onClick.AddListener(RaiseFightEvent);
            _arsenalButton.onClick.AddListener(HandleArsenalWindow);
        }

        private void OnDisable() {
            _fightButton.onClick.RemoveListener(RaiseFightEvent);
            _arsenalButton.onClick.RemoveListener(HandleArsenalWindow);
        }

        private void Start() {
            for (int i = 0; i < _mergeGrid.childCount; i++) {
                var child = _mergeGrid.GetChild(i) as RectTransform;
                var position = child.anchoredPosition;
                _uiFacttory.CreateMergeWeapon(0, position, child);
            }
        }

        private void HandleArsenalWindow() => _arsenal.gameObject.SetActive(!_arsenal.gameObject.activeInHierarchy);
        private void RaiseFightEvent() => FightButtonClicked?.Invoke();
    }
}