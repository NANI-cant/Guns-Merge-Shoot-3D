using System.IO;
using Metric;
using Metric.Levels;
using UnityEngine;

namespace Architecture.Services.AssetProviding.Impl {
    public class ResourcesMetricProvider : IMetricProvider {
        private const string PlayerPath = "Metric/Player";
        private const string EnemiesFolderPath = "Metric/Enemies/";
        private const string LevelPath = "Metric/Levels/Level";

        public PlayerMetric PlayerMetric => Resources.Load<PlayerMetric>(PlayerPath);
        public EnemyMetric EnemyMetric(EnemyId enemyId) => Resources.Load<EnemyMetric>(EnemiesFolderPath + enemyId);
        
        public LevelData LevelData(int number) {
            var level = Resources.Load<LevelData>(LevelPath + number);
            if(level == null) throw new InvalidDataException($"Level {number} does not exist");
            return level;
        }
    }
}