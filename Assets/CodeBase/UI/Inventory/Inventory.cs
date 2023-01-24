using Architecture.Services.AssetProviding;
using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Architecture.Services.PersistentProgress;
using Gameplay.Economic;
using UI.Inventory.Merging;
using UnityEngine;

namespace UI.Inventory {
    [RequireComponent(typeof(RectTransform))]
    public class Inventory : MonoBehaviour{
        [SerializeField] private MergeGrid _mergeGrid;
        [SerializeField] private MergeArea _mergeArea;
        [SerializeField] private BankView _bankView;
        [SerializeField] private BuyButton _buyButton;

        public void Construct(
            IUIFactory uiFactory,
            IMetricProvider metricProvider,
            PlayerPointer playerPointer,
            Bank bank,
            IPersistentProgressService persistentProgressService
        ) {
            _mergeArea.Construct(_mergeGrid, uiFactory, metricProvider, playerPointer, persistentProgressService);
            _bankView.Construct(bank);
            _buyButton.Construct(metricProvider, playerPointer, _mergeArea);
        }
    }
}