using Architecture.Services.Factories;
using UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class Arsenal : MonoBehaviour {
        [SerializeField] private Transform _weaponList;
        [SerializeField] private Button _closeButton;

        private IUIFactory _uiFactory;

        public void Construct(IUIFactory uiFactory) => _uiFactory = uiFactory;

        private void OnEnable() => _closeButton.onClick.AddListener(CloseWindow);
        private void OnDisable() => _closeButton.onClick.RemoveListener(CloseWindow);

        private void Start() {
            _uiFactory.CreateArsenal(_weaponList);
            _weaponList.GetComponent<AutoWidth>().Calculate();
        }

        private void CloseWindow() => gameObject.SetActive(false);
    }
}