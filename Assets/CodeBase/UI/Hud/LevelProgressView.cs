using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud {
    [RequireComponent(typeof(RectTransform))]
    public class LevelProgressView: MonoBehaviour {
        [SerializeField] private Slider _levelSlider;
        
        private ILevelProgressService _levelProgressService;
        private IUIFactory _uiFactory;

        public void Construct(
            ILevelProgressService levelProgressService,
            IUIFactory uiFactory
        ) {
            _levelProgressService = levelProgressService;
            _uiFactory = uiFactory;
            
            _levelProgressService.Modified += UpdateUI;
        }

        private void OnDestroy() => _levelProgressService.Modified -= UpdateUI;

        private void Start() => Initialize();

        private void Initialize() {
            var trackedStages = _levelProgressService.Stages;
            float spacing = 1f / (trackedStages.Length + 1);
            for (int i = 0; i < trackedStages.Length; i++) {
                _uiFactory.CreateStage(transform, spacing * (i + 1), trackedStages[i]);
            }
            
            UpdateUI();
        }

        private void UpdateUI() {
            _levelSlider.value = _levelProgressService.LevelProgress;
        }
    }
}