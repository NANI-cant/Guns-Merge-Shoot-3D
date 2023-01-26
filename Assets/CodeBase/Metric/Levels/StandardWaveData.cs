using System;
using Architecture.Services.AssetProviding;
using Architecture.Services.General;
using UnityEngine;

namespace Metric.Levels {
    [Serializable]
    public class StandardWaveData: WayPointData {
        [SerializeField] private WaveStage[] _stages;
        
        public override StageData[] GetStages(IRandomService randomService, IMetricProvider metricProvider) {
            StageData[] stageDataset = new StageData[_stages.Length];
            for (int i = 0; i < stageDataset.Length; i++) {
                var delay = _stages[i].Delay;
                var enemies = _stages[i].GetEnemies(randomService, metricProvider);
                stageDataset[i] = new StageData(delay, enemies);
            }

            return stageDataset;
        }
    }
}