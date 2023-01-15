using Gameplay.Utils;
using Metric;
using Metric.Levels;

namespace Architecture.Services.AssetProviding {
    public interface IMetricProvider {
        PlayerMetric PlayerMetric { get; }
        
        EnemyMetric EnemyMetric(EnemyId enemyId);
        LevelData LevelData(int number);
    }
}