﻿using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Services.AssetProviding;
using Architecture.Services.Factories;
using Architecture.Services.General;
using Gameplay.HealthLogic;
using Gameplay.Setup.SpawnPoints;
using Gameplay.Utils;
using Metric;
using Metric.Levels;

namespace Architecture.Services.Gameplay {
    public class EnemySpawnService {
        private readonly IGameplayFactory _gameplayFactory;
        private readonly IEnemySpawnPoint[] _enemySpawnPoints;
        private readonly IRandomService _randomService;
        private readonly IMetricProvider _metricProvider;
        private readonly ISchedulerFactory _schedulerFactory;

        private int _remindedEnemiesOnStage = 0;
        private List<IEnemySpawnPoint> _availablePoints = new();
        private int _currentStage = 0;
        private Timer _nextStageTimer;

        public event Action Cleared;

        public int RemindedEnemies { get; private set; } = 0;

        public EnemySpawnService(
            IGameplayFactory gameplayFactory,
            IEnemySpawnPoint[] enemySpawnPoints,
            IRandomService randomService,
            IMetricProvider metricProvider,
            ISchedulerFactory schedulerFactory
        ) {
            _gameplayFactory = gameplayFactory;
            _enemySpawnPoints = enemySpawnPoints;
            _randomService = randomService;
            _metricProvider = metricProvider;
            _schedulerFactory = schedulerFactory;
        }

        public void SpawnWave(StandardWaveData waveData) {
            _currentStage = 0;
            _nextStageTimer?.Stop();
            List<EnemyId[]> stages = new List<EnemyId[]>();
            List<float> delays = new List<float>();
            foreach (var stage in waveData.Stages) {
                var enemiesOnStage = stage.GetEnemies(_randomService, _metricProvider);
                RemindedEnemies = RemindedEnemies + enemiesOnStage.Length;
                stages.Add(enemiesOnStage);
                delays.Add(stage.Delay);
            }
            
            SpawnSequence(stages, delays);
        }

        private void SpawnSequence(List<EnemyId[]> stages, List<float> delays) {
            SpawnStage(stages[_currentStage]);
            _currentStage++;
            if(_currentStage == stages.Count) return;
            _nextStageTimer = _schedulerFactory
                .CreateTimer(
                    delays[_currentStage], 
                    () => SpawnSequence(stages, delays)
                );
        }

        private void SpawnStage(EnemyId[] enemies) {
            foreach (var enemyId in enemies) {
                var randomEnemySpawn = TakeSpawnPoint();
                var enemy = _gameplayFactory.CreateEnemy(enemyId, randomEnemySpawn.Position, randomEnemySpawn.Rotation);
                Track(enemy.GetComponent<Health>());
            }

            _remindedEnemiesOnStage = enemies.Length;
        }

        private IEnemySpawnPoint TakeSpawnPoint() {
            if (_availablePoints.Count == 0) _availablePoints = _enemySpawnPoints.ToList();
            
            int chosenIndex = _randomService.Range(0, _availablePoints.Count);
            var randomEnemySpawn = _availablePoints[chosenIndex];
            _availablePoints.RemoveAt(chosenIndex);
            return randomEnemySpawn;
        }

        private void Track(Health health) {
            health.Died += Forget;
        }

        private void Forget(Health health) {
            health.Died -= Forget;
            
            RemindedEnemies--;
            _remindedEnemiesOnStage--;
            
            if (_remindedEnemiesOnStage == 0) _nextStageTimer.Skip();
            if (RemindedEnemies == 0) Cleared?.Invoke();
        }
    }
}