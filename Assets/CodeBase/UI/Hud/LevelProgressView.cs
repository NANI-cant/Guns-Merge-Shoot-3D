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
        private WayPointProgressView[] _points;

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
            float spacing = 1f / (_levelProgressService.PointsCount + 1);
            _points = new WayPointProgressView[_levelProgressService.PointsCount];
            for (int i = 0; i < _levelProgressService.PointsCount; i++) {
                _points[i] = _uiFactory.CreateWayPoint(transform, spacing * (i + 1)).GetComponent<WayPointProgressView>();
                _points[i].Progress = 0;
            }
            
            UpdateUI();
        }

        private void UpdateUI() {
            for (int i = 0; i < _levelProgressService.CurrentPoint-1; i++) {
                _points[i].Progress = 1;
            }

            if (_levelProgressService.CurrentPoint != _points.Length) {
                _points[_levelProgressService.CurrentPoint].Progress = _levelProgressService.PointProgress;    
            } 
            
            _levelSlider.value = _levelProgressService.LevelProgress;
        }
    }
}