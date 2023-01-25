using System;
using UnityEngine;

namespace Metric.Levels {
    [Serializable]
    public class StandardWaveData {
        [SerializeField] private WaveStage[] _stages;

        public WaveStage[] Stages => _stages;
    }
}