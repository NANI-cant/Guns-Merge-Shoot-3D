using Architecture.Services.AssetProviding;
using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Architecture.Services.PersistentProgress;
using Gameplay.Economic;
using PersistentProgress;
using UI.Inventory.Merging;
using UnityEngine;

namespace UI.Inventory {
    [RequireComponent(typeof(RectTransform))]
    public class Inventory : MonoBehaviour, IProgressReader, IProgressWriter {
        [SerializeField] private MergeGrid _mergeGrid;
        [SerializeField] private MergeArea _mergeArea;
        [SerializeField] private BankView _bankView;
        [SerializeField] private BuyButton _buyButton;
        
        private IReadOnlyPlayerProgress _playerProgress;

        public void Construct(
            IUIFactory uiFactory,
            IMetricProvider metricProvider,
            PlayerPointer playerPointer,
            Bank bank,
            IReadOnlyPlayerProgress playerProgress
        ) {
            _playerProgress = playerProgress;
            
            _mergeArea.Construct(_mergeGrid, uiFactory, metricProvider, playerPointer);
            _bankView.Construct(bank);
            _buyButton.Construct(metricProvider.WeaponData[0], _mergeArea);
        }

        private void Start() {
            _mergeArea.Initialize(_playerProgress.InventoryWeapons, _playerProgress.InventoryPositions);
        }

        public void Read(IReadOnlyPlayerProgress playerProgress) {
            _mergeArea.Read(playerProgress);
        }

        public void Write(PlayerProgress playerProgress) {
            _mergeArea.Write(playerProgress);
        }
    }
}