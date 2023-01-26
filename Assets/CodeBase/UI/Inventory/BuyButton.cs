using Architecture.Services.AssetProviding;
using Architecture.Services.Gameplay;
using Architecture.Services.Gameplay.Impl;
using Gameplay.Economic;
using Metric.Weapons;
using PersistentProgress;
using TMPro;
using UI.Inventory.Merging;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory {
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Button))]
    public class BuyButton: MonoBehaviour, IProgressReader, IProgressWriter {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _priceText;

        private Button _button;
        private WeaponData _weaponData;
        private MergeArea _mergeArea;
        private IMetricProvider _metricProvider;
        private PlayerPointer _playerPointer;
        private int _buyCount;

        private long Price => (long)(_weaponData.StartPrice * Mathf.Pow(1.2f, _buyCount));

        private void Awake() => _button = GetComponent<Button>();

        public void Construct(IMetricProvider metricProvider, PlayerPointer playerPointer ,MergeArea mergeArea) {
            _playerPointer = playerPointer;
            _metricProvider = metricProvider;
            _mergeArea = mergeArea;
            _playerPointer.Player.WeaponHolder.Switched += UpdateData;
        }

        private void OnEnable() => _button.onClick.AddListener(Buy);
        private void OnDisable() => _button.onClick.RemoveListener(Buy);
        private void OnDestroy() => _playerPointer.Player.WeaponHolder.Switched -= UpdateData;

        public void Read(IReadOnlyPlayerProgress playerProgress) {
            int targetLvl = Mathf.Max(playerProgress.MaxWeaponLevel - 2, 0); 
            _weaponData = _metricProvider.WeaponData[targetLvl];
            _buyCount = playerProgress.WeaponBuyCount;
            UpdateUI();
        }

        public void Write(PlayerProgress playerProgress) {
            playerProgress.WeaponBuyCount = _buyCount;
        }

        private void UpdateData() {
            var currentLvl = _weaponData != null ? _weaponData.Level : 0;
            int targetLvl = Mathf.Max(_playerPointer.Player.WeaponHolder.WeaponData.Level - 2, 0);
            if (currentLvl != targetLvl) _buyCount = 0;
            _weaponData = _metricProvider.WeaponData[targetLvl];
            UpdateUI();
        }

        private void UpdateUI() {
            if(_weaponData == null) return;
            
            _image.sprite = _weaponData.Image;
            _priceText.text = Price.ToEconomicString();
        }

        private void Buy() {
            _buyCount++;
            _mergeArea.AddWeapon(_weaponData);
            UpdateUI();
        }
    }
}