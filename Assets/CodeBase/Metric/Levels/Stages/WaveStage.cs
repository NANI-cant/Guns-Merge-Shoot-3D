using System.Collections.Generic;
using Architecture.Services.AssetProviding;
using Architecture.Services.General;
using UnityEngine;

namespace Metric.Levels.Stages {
    [CreateAssetMenu(fileName = "WaveStage", menuName = "Levels/Stages/Wave")]
    public class WaveStage : Stage {
        [SerializeField] private float _targetPower;
        [SerializeField] [Min(0f)] private float _levelCoefficient = 1.3f;

        public override StageData GetStageData(EnemyId[] availableEnemies, int level, IRandomService randomService, IMetricProvider metricProvider) {
            List<EnemyId> chosenEnemies = new();
            float power = 0;
            while (power < _targetPower + Mathf.Pow(_levelCoefficient, level)) {
                int randomIndex = randomService.Range(0, availableEnemies.Length);
                EnemyId id = availableEnemies[randomIndex];
                chosenEnemies.Add(id);
                power += metricProvider.EnemyMetric(id).Power;
            }

            return new StageData(chosenEnemies.ToArray(), Image);
        }
    }
}