using System.Collections.Generic;
using Architecture.Services.General;
using Metric.Weapons;
using UnityEngine;

namespace Gameplay.PlayerLogic.Weapons {
    public class WeaponHolder: MonoBehaviour {
        [SerializeField] private Transform _weaponPivot;

        private AutoAttacker _autoAttacker;
        private CharacterAnimator _characterAnimator;
        private IInstantiateProvider _instantiateProvider;
        private Dictionary<int, GameObject> _cashedWeapons = new();

        private void Awake() {
            _characterAnimator = GetComponent<CharacterAnimator>();
            _autoAttacker = GetComponent<AutoAttacker>();
        }

        public void Construct(IInstantiateProvider instantiateProvider) {
            _instantiateProvider = instantiateProvider;
        }

        public void SetWeapon(WeaponData weaponData) {
            var weapon = CashWeapon(weaponData);
            weapon.SetActive(true);
            _autoAttacker.Setup(weaponData.Damage, 1 / weaponData.Speed, weaponData.CritChance, weaponData.CritValue);
            _characterAnimator.Controller = weaponData.AnimatorController;
            _characterAnimator.AttackSpeed = weaponData.Speed;
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
    }
}