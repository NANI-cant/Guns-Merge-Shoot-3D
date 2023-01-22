using Metric.Weapons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Inventory.Merging {
    [RequireComponent(typeof(RectTransform))]
    public class MergeWeapon : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {
        [SerializeField] private Image _image;

        private Vector2 _pointerOffset;
        private MergeArea _mergeArea;
        
        public MergeCell AttachedCell { get; private set; }
        public WeaponData WeaponData { get; private set; }
        public RectTransform Transform { get; private set; }

        private void Awake() => Transform = GetComponent<RectTransform>();

        public void Construct(WeaponData weaponData, MergeCell attachedCell, MergeArea mergeArea) {
            Attach(attachedCell);
            _mergeArea = mergeArea;

            Upgrade(weaponData);
        }

        public void Upgrade(WeaponData weaponData) {
            WeaponData = weaponData;
            _image.sprite = WeaponData.Image;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            _pointerOffset = eventData.position - (Vector2)Transform.position;
        }

        public void OnDrag(PointerEventData eventData) {
            Transform.position = eventData.position - _pointerOffset;
            _mergeArea.OnWeaponDragged(this, eventData.position);
        }

        public void Attach(MergeCell cell) {
            if(AttachedCell != null) AttachedCell.IsEmpty = true;
            AttachedCell = cell;
            AttachedCell.IsEmpty = false;
        }

        public void Detach() {
            AttachedCell.IsEmpty = true;
            AttachedCell = null;
        }
        
        public void ReturnToGrid() => Transform.anchoredPosition = AttachedCell.Transform.anchoredPosition;
        public void OnEndDrag(PointerEventData eventData) => _mergeArea.OnWeaponDropped(this, eventData.position);

        public void Swap(MergeWeapon weapon) {
            (weapon.AttachedCell, AttachedCell) = (AttachedCell, weapon.AttachedCell);
            ReturnToGrid();
            weapon.ReturnToGrid();
        }
    }
}