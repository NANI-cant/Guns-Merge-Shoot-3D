using System.Collections.Generic;
using Architecture.Services.AssetProviding;
using Architecture.Services.Gameplay;
using Architecture.Services.Gameplay.Impl;
using Architecture.Services.General;
using Architecture.Services.PersistentProgress;
using Gameplay.Economic;
using PersistentProgress;
using UI;
using UI.Arsenal;
using UI.Hud;
using UI.Inventory;
using UI.Inventory.Merging;
using UnityEngine;

namespace Architecture.Services.Factories.Impl {
    public class UIFactory : IUIFactory {
        private readonly IUIProvider _uiProvider;
        private readonly IInstantiateProvider _instantiateProvider;
        private readonly IMetricProvider _metricProvider;
        private readonly PlayerPointer _playerPointer;
        private readonly Bank _bank;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ILevelProgressService _levelProgressService;

        public UIFactory(
            IUIProvider uiProvider,
            IInstantiateProvider instantiateProvider,
            IMetricProvider metricProvider,
            PlayerPointer playerPointer,
            Bank bank,
            IPersistentProgressService persistentProgressService,
            ILevelProgressService levelProgressService
        ) {
            _uiProvider = uiProvider;
            _instantiateProvider = instantiateProvider;
            _metricProvider = metricProvider;
            _playerPointer = playerPointer;
            _bank = bank;
            _persistentProgressService = persistentProgressService;
            _levelProgressService = levelProgressService;
        }

        public GameObject CreateCampUI() {
            var campUI = _instantiateProvider.Instantiate(_uiProvider.CampUI, Vector3.zero, Quaternion.identity);
            
            campUI.GetComponentInChildren<Inventory>(true).Construct(this, _metricProvider, _playerPointer, _bank, _persistentProgressService);
            campUI.GetComponentInChildren<Arsenal>(true).Construct(this);
            
            SubscribeToProgress(campUI);
            
            return campUI;
        }

        public GameObject CreateMergeWeapon(int level, MergeCell cell, Transform container, MergeArea mergeArea) {
            var weapon = _instantiateProvider.InstantiateUI(_uiProvider.MergeWeapon, cell.Transform.anchoredPosition, container);

            weapon.GetComponent<MergeWeapon>().Construct(_metricProvider.WeaponData[level], cell, mergeArea);

            return weapon;
        }

        public GameObject CreateHUD() {
            var hudUI = _instantiateProvider.Instantiate(_uiProvider.HUD, Vector3.zero, Quaternion.identity);
            
            hudUI.GetComponentInChildren<LevelProgressView>(true).Construct(_levelProgressService, this);

            return hudUI;
        }

        public GameObject[] CreateArsenal(Transform container) {
            List<GameObject> items = new();
            foreach (var weaponData in _metricProvider.WeaponData) {
                var item = _instantiateProvider.Instantiate(
                    _uiProvider.ArsenalItem, 
                    Vector3.zero, 
                    Quaternion.identity,
                    container
                );
                item.GetComponent<ArsenalItem>().Construct(weaponData, _playerPointer);
            }

            return items.ToArray();
        }

        public GameObject CreateWayPoint(Transform container, float anchor) {
            var point = _instantiateProvider.Instantiate(_uiProvider.WayPoint, Vector3.zero, Quaternion.identity, container);
            
            var pointTransform = point.GetComponent<RectTransform>();
            pointTransform.anchorMin = new Vector2(anchor, 0);
            pointTransform.anchorMax = new Vector2(anchor, 1);
            pointTransform.anchoredPosition = Vector2.zero;
            
            return point;
        }

        private void SubscribeToProgress(GameObject gameObject) {
            foreach (var reader in gameObject.GetComponentsInChildren<IProgressReader>(true)) {
                _persistentProgressService.AddReader(reader);
            }
            
            foreach (var writer in gameObject.GetComponentsInChildren<IProgressWriter>(true)) {
                _persistentProgressService.AddWriter(writer);
            }
        }
    }
}