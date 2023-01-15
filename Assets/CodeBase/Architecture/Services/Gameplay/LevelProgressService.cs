using Architecture.Services.AssetProviding;
using Metric.Levels;

namespace Architecture.Services.Gameplay {
    public class LevelProgressService {
        private readonly IMetricProvider _metricProvider;
        
        private int _currentLevel;

        public int WaveCount => LevelData.Waves.Length;
        
        private LevelData LevelData => _metricProvider.LevelData(_currentLevel);

        public LevelProgressService(
            IMetricProvider metricProvider
        ) {
            _metricProvider = metricProvider;
            _currentLevel = 1;
        }

        public void ChangeLevel() {
            _currentLevel++;
        }
    }
}