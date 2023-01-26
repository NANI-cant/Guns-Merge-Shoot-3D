using Metric.Levels.Stages;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud {
    [RequireComponent(typeof(RectTransform))]
    public class StageView: MonoBehaviour {
        [SerializeField] private Image _progressImage;
        [SerializeField] private Image _typeImage;
        
        private StageData _trackedStage;

        public void Construct(StageData trackedStage) {
            _trackedStage = trackedStage;
            _trackedStage.ProgressModified += UpdateUI;
            _typeImage.sprite = _trackedStage.Image;
            UpdateUI();
        }

        private void OnDestroy() => _trackedStage.ProgressModified -= UpdateUI;
        
        private void UpdateUI() => _progressImage.fillAmount = _trackedStage.Progress;
    }
}