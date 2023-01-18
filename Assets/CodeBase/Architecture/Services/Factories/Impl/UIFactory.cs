using System.Collections.Generic;
using Architecture.Services.AssetProviding;
using Architecture.Services.Gameplay;
using Architecture.Services.General;
using UI;
using UnityEngine;

namespace Architecture.Services.Factories.Impl {
    public class UIFactory : IUIFactory {
        private readonly IUIProvider _uiProvider;
        private readonly IInstantiateProvider _instantiateProvider;
        private readonly IMetricProvider _metricProvider;
        private readonly PlayerPointer _playerPointer;

        public UIFactory(
            IUIProvider uiProvider,
            IInstantiateProvider instantiateProvider,
            IMetricProvider metricProvider,
            PlayerPointer playerPointer
        ) {
            _uiProvider = uiProvider;
            _instantiateProvider = instantiateProvider;
            _metricProvider = metricProvider;
            _playerPointer = playerPointer;
        }

        public GameObject CreateCampUI() {
            var campUI = _instantiateProvider.Instantiate(_uiProvider.CampUI, Vector3.zero, Quaternion.identity);
            
            campUI.GetComponent<CampUI>().Construct(this);
            campUI.GetComponent<CampUI>().Arsenal.Construct(this);
            
            return campUI;
        }

        public GameObject CreateMergeWeapon(int level, Vector3 position, Transform container) {
            var weapon = _instantiateProvider.InstantiateUI(_uiProvider.MergeWeapon, position, container);

            weapon.GetComponent<MergeWeapon>().Construct(_metricProvider.WeaponData[level]);

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