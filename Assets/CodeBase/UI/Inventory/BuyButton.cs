using Gameplay.Economic;
using Metric.Weapons;
using TMPro;
using UI.Inventory.Merging;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory {
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Button))]
    public class BuyButton: MonoBehaviour {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _priceText;

        private Button _button;
        private WeaponData _weaponData;
        private MergeArea _mergeArea;

        private void Awake() => _button = GetComponent<Button>();

        public void Construct(WeaponData weaponData, MergeArea mergeArea) {
            _weaponData = weaponData;
            _mergeArea = mergeArea;
            UpdateUI();
        }

        private void OnEnable() => _button.onClick.AddListener(Buy);
        private void OnDisable() => _button.onClick.RemoveListener(Buy);

        private void UpdateUI() {
            _image.sprite = _weaponData.Image;
            _priceText.text = _weaponData.StartPrice.ToEconomicString();
        }

        private void Buy() {
            _mergeArea.AddWeapon(_weaponData);
        }
    }
}