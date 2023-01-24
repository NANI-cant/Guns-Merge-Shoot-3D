using Gameplay.Economic;
using TMPro;
using UnityEngine;

namespace UI.Inventory {
    [RequireComponent(typeof(RectTransform))]
    public class BankView: MonoBehaviour {
        [SerializeField] private TMP_Text _text;
        
        private Bank _bank;

        public void Construct(Bank bank) {
            _bank = bank;
            _bank.Modified += UpdateUI;
        }

        private void Start() => UpdateUI();
        private void OnDestroy() => _bank.Modified -= UpdateUI;
        
        private void UpdateUI() => _text.text = _bank.Amount.ToEconomicString();
    }
}