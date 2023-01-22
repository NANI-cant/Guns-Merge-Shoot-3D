using System.Collections.Generic;
using Architecture.Services.AssetProviding;
using Architecture.Services.Gameplay;
using Architecture.Services.General;
using Architecture.Services.PersistentProgress;
using Gameplay.Economic;
using UI;
using UI.Arsenal;
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

        public UIFactory(
            IUIProvider uiProvider,
            IInstantiateProvider instantiateProvider,
            IMetricProvider metricProvider,
            PlayerPointer playerPointer,
            Bank bank,
            IPersistentProgressService persistentProgressService
        ) {
            _uiProvider = uiProvider;
            _instantiateProvider = instantiateProvider;
            _metricProvider = metricProvider;
            _playerPointer = playerPointer;
            _bank = bank;
            _persistentProgressService = persistentProgressService;
        }

        public GameObject CreateCampUI() {
            var campUI = _instantiateProvider.Instantiate(_uiProvider.CampUI, Vector3.zero, Quaternion.identity);
            
            campUI.GetComponent<CampUI>().Inventory.Construct(this, _metricProvider, _playerPointer, _bank, _persistentProgressService.PlayerProgress);
            campUI.GetComponent<CampUI>().Arsenal.Construct(this);
            
            return campUI;
        }

        public GameObject CreateMergeWeapon(int level, MergeCell cell, Transform container, MergeArea mergeArea) {
            var weapon = _instantiateProvider.InstantiateUI(_uiProvider.MergeWeapon, cell.Transform.anchoredPosition, container);

            weapon.GetComponent<MergeWeapon>().Construct(_metricProvider.WeaponData[level], cell, mergeArea);

            return weapon;
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
    }
}