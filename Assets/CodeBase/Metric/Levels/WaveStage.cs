using System;
using System.Collections.Generic;
using Architecture.Services.AssetProviding;
using Architecture.Services.General;
using UnityEngine;

namespace Metric.Levels {
    [Serializable]
    public class WaveStage {
        [SerializeField] private EnemyId[] _availableEnemies;
        [SerializeField] private float _targetPower;
        [SerializeField] private float _delay = 5f;

        public float Delay => _delay;

        public EnemyId[] GetEnemies(IRandomService random, IMetricProvider metricProvider) {
            List<EnemyId> chosenEnemies = new List<EnemyId>();

            float power = 0;
            while (power < _targetPower) {
                int randomIndex = random.Range(0, _availableEnemies.Length);
                var enemy = _availableEnemies[randomIndex];
                var metric = metricProvider.EnemyMetric(enemy);
                power += metric.Power;
                chosenEnemies.Add(enemy);
            }

            return chosenEnemies.ToArray();
        }
    }
}