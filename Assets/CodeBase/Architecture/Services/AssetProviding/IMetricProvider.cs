using Metric;
using Metric.Levels;
using Metric.Weapons;

namespace Architecture.Services.AssetProviding {
    public interface IMetricProvider {
        PlayerMetric PlayerMetric { get; }
        WeaponData[] WeaponData { get; }
        DifficultMetric Difficult { get; }

        EnemyMetric EnemyMetric(EnemyId enemyId);
        Level LevelData(int number);
    }
}