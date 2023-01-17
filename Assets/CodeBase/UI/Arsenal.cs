using Architecture.Services.Factories;
using UI.Utils;
using UnityEngine;

namespace UI {
    public class Arsenal : MonoBehaviour {
        [SerializeField] private Transform _weaponList;

        private IUIFactory _uiFactory;

        public void Construct(IUIFactory uiFactory) => _uiFactory = uiFactory;

        private void Start() {
            _uiFactory.CreateArsenal(_weaponList);
            _weaponList.GetComponent<AutoWidth>().Calculate();
        }

        private void CloseWindow() => gameObject.SetActive(false);
    }
}