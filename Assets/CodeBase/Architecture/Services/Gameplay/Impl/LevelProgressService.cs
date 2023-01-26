using System;
using System.Collections;
using Architecture.Services.AssetProviding;
using Architecture.Services.General;
using Metric.Levels;
using Metric.Levels.Stages;
using UnityEngine;

namespace Architecture.Services.Gameplay.Impl {
    public class LevelProgressService: ILevelProgressService {
        private readonly IMetricProvider _metricProvider;
        private readonly StageService _stageService;
        private readonly IRandomService _randomService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ITimeProvider _timeProvider;
        
        private int _levelNumber;
        private int _stageNumber;

        public event Action Modified;

        public StageData[] Stages { get; private set; }
        public float LevelProgress { get; private set; }
        public StageData ActiveStage => Stages[_stageNumber];
        public bool IsLevelOver => _stageNumber == Stages.Length;

        public LevelProgressService(
            IMetricProvider metricProvider,
            StageService stageService,
            IRandomService randomService,
            ICoroutineRunner coroutineRunner,
            ITimeProvider timeProvider
        ) {
            _metricProvider = metricProvider;
            _stageService = stageService;
            _randomService = randomService;
            _coroutineRunner = coroutineRunner;
            _timeProvider = timeProvider;
        }

        public void ResetLevel() {
            var level = _metricProvider.LevelData(_levelNumber);
            Stages = new StageData[level.Stages.Length];
            for (int i = 0; i < Stages.Length; i++) {
                Stages[i] = level.Stages[i].GetStageData(level.AvailableEnemies, _levelNumber, _randomService, _metricProvider);
            }
            
            _stageNumber = 0;
            Modified?.Invoke();
        }

        public void NextLevel() {
            _levelNumber++;
            
            var level = _metricProvider.LevelData(_levelNumber);
            Stages = new StageData[level.Stages.Length];
            for (int i = 0; i < Stages.Length; i++) {
                Stages[i] = level.Stages[i].GetStageData(level.AvailableEnemies, _levelNumber, _randomService, _metricProvider);
            }
            
            _stageNumber = 0;
            Modified?.Invoke();
        }

        public void NextStage() => _stageNumber++;
        public void TranslateToNextStage(Action callback) => _coroutineRunner.StartCoroutine(ExecuteTranslation(callback));

        private IEnumerator ExecuteTranslation(Action callback) {
            float passedTime = 0;
            while (passedTime < 3f) {
                yield return null;
                passedTime += _timeProvider.DeltaTime;
                float progress = Mathf.Clamp01(passedTime / 3f); 
                LevelProgress = 1f / (Stages.Length + 1) * (_stageNumber + progress);
                Modified?.Invoke();
            }
            callback?.Invoke();
        }
    }
}