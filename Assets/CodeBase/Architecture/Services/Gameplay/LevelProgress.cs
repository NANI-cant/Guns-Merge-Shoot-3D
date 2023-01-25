using Architecture.Services.AssetProviding;
using Metric.Levels;
using Zenject;

namespace Architecture.Services.Gameplay {
    public class LevelProgress {
        private readonly IMetricProvider _metricProvider;
        
        private LevelData _levelData;
        private int _levelNumber;
        private int _waveNumber;

        public StandartWaveData WaveData => _levelData.Waves[_waveNumber];
        public int WaveCount => _levelData.Waves.Length;
        public bool IsLevelOver => _waveNumber == WaveCount;

        public LevelProgress(IMetricProvider metricProvider) {
            _metricProvider = metricProvider;
        }

        public void NextLevel() {
            _levelNumber++;
            _waveNumber = 0;
            _levelData = _metricProvider.LevelData(_levelNumber);
        }

        public void NextWave() => _waveNumber++;
        public void ResetLevel() => _waveNumber = 0;
    }
}