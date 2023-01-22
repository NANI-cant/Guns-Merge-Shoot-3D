using UnityEngine;

namespace Metric.Weapons {
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/Weapon Data")]
    public class WeaponData: ScriptableObject {
        [SerializeField][Min(0)] private int _level;
        [Space]
        [SerializeField] private string _name;
        [SerializeField][Min(1)] private long _startPrice;
        [Space]
        [SerializeField] private Sprite _image;
        [SerializeField] private GameObject _template;
        [SerializeField] private AnimatorOverrideController _animatorController;
        [Space]
        [SerializeField][Min(int.MinValue)] private int _damage;
        [SerializeField][Min(float.MinValue)] private float _speed;
        [SerializeField][Range(0,100)] private int _critChance;
        [SerializeField][Min(1f)] private float _critValue;
        [Space]
        [SerializeField] private float _dps;

        public int Damage => _damage;
        public float Speed => _speed;
        public int CritChance => _critChance;
        public float CritValue => _critValue;
        public float DPS => _dps;
        public GameObject Template => _template;
        public Sprite Image => _image;
        public long StartPrice => _startPrice;
        public string Name => _name;
        public int Level => _level;
        public AnimatorOverrideController AnimatorController => _animatorController;

#if UNITY_EDITOR
        private void OnValidate() {
            _dps = (1 + (float)_critChance/100f * (_critValue-1)) * _damage * _speed;
        }
#endif
    }
}