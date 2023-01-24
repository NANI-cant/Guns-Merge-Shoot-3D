using Architecture.Services.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Arsenal {
    public class Arsenal : MonoBehaviour {
        [SerializeField] private Transform _weaponList;
        [SerializeField] private Button _closeButton;

        private IUIFactory _uiFactory;

        public void Construct(IUIFactory uiFactory) => _uiFactory = uiFactory;

        private void OnEnable() => _closeButton.onClick.AddListener(CloseWindow);
        private void OnDisable() => _closeButton.onClick.RemoveListener(CloseWindow);

        private void Start() {
            _uiFactory.CreateArsenal(_weaponList);
        }

        private void CloseWindow() => gameObject.SetActive(false);
    }
}