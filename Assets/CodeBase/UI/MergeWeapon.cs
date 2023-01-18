using Metric.Weapons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI {
    public class MergeWeapon : MonoBehaviour, IDragHandler, IDropHandler {
        [SerializeField] private Image _image;
        
        private WeaponData _weaponData;

        public void Construct(WeaponData weaponData) {
            _weaponData = weaponData;

            _image.sprite = _weaponData.Image;
        }

        public void OnDrag(PointerEventData eventData) {
            var data = eventData;
            if (dragOnSurfaces &amp;&amp; data.pointerEnter != null &amp;&amp; data.pointerEnter.transform as RectTransform != null) 
                m_DraggingPlane = data.pointerEnter.transform as RectTransform;

            var rt = m_DraggingIcon.GetComponent<RectTransform>();
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
            {
                rt.position = globalMousePos;
                rt.rotation = m_DraggingPlane.rotation;
            }
        }

        public void OnDrop(PointerEventData eventData) {
            
        }
    }
}