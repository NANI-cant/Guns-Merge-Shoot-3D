using System;
using System.Collections.Generic;
using Architecture.Services.AssetProviding;
using Architecture.Services.General;
using Metric.Weapons;
using PersistentProgress;
using UnityEngine;

namespace Gameplay.PlayerLogic.Weapons {
    public class WeaponHolder: MonoBehaviour, IProgressReader, IProgressWriter {
        [SerializeField] private Transform _weaponPivot;

        public event Action Switched;
        
        private AutoAttacker _autoAttacker;
        private CharacterAnimator _characterAnimator;
        private IInstantiateProvider _instantiateProvider;
        private Dictionary<int, GameObject> _cashedWeapons = new();
        private GameObject _activeWeapon;
        private IMetricProvider _metricProvider;
        
        public WeaponData WeaponData { get; private set; }

        private void Awake() {
            _characterAnimator = GetComponent<CharacterAnimator>();
            _autoAttacker = GetComponent<AutoAttacker>();
        }

        public void Construct(IInstantiateProvider instantiateProvider, IMetricProvider metricProvider) {
            _instantiateProvider = instantiateProvider;
            _metricProvider = metricProvider;
        }

        public void SetWeapon(WeaponData weaponData) {
            WeaponData = weaponData;
            var weapon = CashWeapon(weaponData);
            SwitchWeapon(weapon);

            _autoAttacker.Setup(weaponData.Damage, 1 / weaponData.Speed, weaponData.CritChance, weaponData.CritValue);
            _characterAnimator.Controller = weaponData.AnimatorController;
            _characterAnimator.AttackSpeed = weaponData.Speed;
        }

        private void SwitchWeapon(GameObject weapon) {
            _activeWeapon?.SetActive(false);
            _activeWeapon = weapon;
            _activeWeapon.SetActive(true);
            Switched?.Invoke();
        }

        private GameObject CashWeapon(WeaponData weaponData) {
            if (_cashedWeapons.ContainsKey(weaponData.Level)) return _cashedWeapons[weaponData.Level];
            
            _cashedWeapons[weaponData.Level] = _instantiateProvider.Instantiate(
                weaponData.Template,
                _weaponPivot.position,
                _weaponPivot.rotation,
                _weaponPivot
            );
            _cashedWeapons[weaponData.Level].SetActive(false);
            return _cashedWeapons[weaponData.Level];
        }

        public void Read(IReadOnlyPlayerProgress playerProgress) {
            SetWeapon(_metricProvider.WeaponData[playerProgress.MaxWeaponLevel]);
        }

        public void Write(PlayerProgress playerProgress) {
            playerProgress.MaxWeaponLevel = WeaponData.Level;
        }
    }
}