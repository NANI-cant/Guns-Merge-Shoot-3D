using TMPro;
using UnityEngine;

namespace UI.Inventory {
    [RequireComponent(typeof(RectTransform))]
    public class MoneyPerSecView: MonoBehaviour {
        [SerializeField] private TMP_Text _text;
    }
}