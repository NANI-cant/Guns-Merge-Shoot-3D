using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory.Merging {
    [RequireComponent(typeof(RectTransform))]
    public class MergeCell : MonoBehaviour {
        [SerializeField] private Image _image;
        [SerializeField] private Color _selected;
        [SerializeField] private Color _unselected;

        public RectTransform Transform { get; private set; }
        public bool IsEmpty = true;

        private void Awake() => Transform = GetComponent<RectTransform>();

        public void Select() => _image.color = _selected;
        public void UnSelect() => _image.color = _unselected;
    }
}