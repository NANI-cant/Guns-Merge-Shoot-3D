using Architecture.Services.Gameplay;
using Metric.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Arsenal {
    public class ArsenalItem: MonoBehaviour {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _dps;
        [SerializeField] private Image _image;
        [SerializeField] private Button _chooseButton;
        
        private WeaponData _weaponData;
        private PlayerPointer _playerPointer;

        private void OnEnable() => _chooseButton.onClick.AddListener(SwitchWeapon);
        private void OnDisable() => _chooseButton.onClick.RemoveListener(SwitchWeapon);

        public void Construct(WeaponData weaponData, PlayerPointer playerPointer) {
            _weaponData = weaponData;
            _playerPointer = playerPointer;

            _name.text = weaponData.Name;
            _level.text = weaponData.Level.ToString();
            _dps.text = weaponData.DPS.ToString();
            _image.sprite = weaponData.Image;
        }

        private void SwitchWeapon() => _playerPointer.Player.WeaponHolder.SetWeapon(_weaponData);
    }
}