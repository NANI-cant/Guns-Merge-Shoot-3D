using Gameplay.HealthLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI {
    public class HealthView: MonoBehaviour {
        [SerializeField] private Slider _slider;

        private Camera _camera;
        private Health _trackedHealth;

        private void Awake() {
            _camera = Camera.main;
            _trackedHealth = GetComponentInParent<Health>();
            _trackedHealth.HitTaken += UpdateSlider;
        }

        private void OnEnable() => SetupSlider(_trackedHealth.CurrentHealth);
        private void Start() => SetupSlider(_trackedHealth.CurrentHealth);
        private void OnDestroy() => _trackedHealth.HitTaken -= UpdateSlider;
        private void Update() => LookAtCamera();

        //private void LookAtCamera() => transform.forward = (_camera.transform.position - transform.position).normalized;
        private void LookAtCamera() => transform.rotation = _camera.transform.rotation;

        private void SetupSlider(float maxHealth) {
            _slider.maxValue = maxHealth;
            _slider.value = maxHealth;
        }

        private void UpdateSlider(float damage, bool isCrit) {
            if(isCrit) Debug.Log("Crit");
            _slider.maxValue = _trackedHealth.MaxHealth;
            _slider.value = _trackedHealth.CurrentHealth;
        }

#if UNITY_EDITOR
        private void OnValidate() {
            if(_slider != null) return;
            _slider = GetComponentInChildren<Slider>();
        }
#endif
    }
}