using System;
using Architecture.Services.AssetProviding;
using Metric.Levels;

namespace Architecture.Services.Gameplay.Impl {
    public class LevelProgressService: ILevelProgressService {
        private readonly IMetricProvider _metricProvider;
        private readonly EnemySpawnService _enemySpawnService;

        private LevelData _levelData;
        private int _levelNumber;

        public event Action Modified;
        
        public WayPointData WayPointData => _levelData.WayPoints[CurrentPoint];
        public int PointsCount => _levelData.WayPoints.Length;
        public int CurrentPoint { get; private set; }
        public float LevelProgress => (float) CurrentPoint / PointsCount;
        public float PointProgress => _enemySpawnService.WaveProgress;
        public bool IsLevelOver => CurrentPoint == PointsCount;

        public LevelProgressService(
            IMetricProvider metricProvider,
            EnemySpawnService enemySpawnService
        ) {
            _metricProvider = metricProvider;
            _enemySpawnService = enemySpawnService;
            _enemySpawnService.ProgressModified += () => Modified?.Invoke();
        }

        public void NextLevel() {
            _levelNumber++;
            CurrentPoint = 0;
            _levelData = _metricProvider.LevelData(_levelNumber);
            Modified?.Invoke();
        }

        public void NextPoint() {
            CurrentPoint++;
            Modified?.Invoke();
        }

        public void ResetLevel() {
            CurrentPoint = 0;
            Modified?.Invoke();
        }
    }
}