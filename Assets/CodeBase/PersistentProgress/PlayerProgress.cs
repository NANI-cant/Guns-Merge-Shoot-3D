using System;
using UnityEngine;

namespace PersistentProgress {
    [Serializable]
    public class PlayerProgress: IReadOnlyPlayerProgress {
        [SerializeField] private int _maxWeaponLevel = 0;
        [SerializeField] private int[] _inventoryWeapons = Array.Empty<int>();
        [SerializeField] private int[] _inventoryCells = Array.Empty<int>();
        [SerializeField] private long _bankAmount = 0;
        [SerializeField] private int _weaponBuyCount = 0;

        public int MaxWeaponLevel {
            get => _maxWeaponLevel;
            set => _maxWeaponLevel = value;
        }

        public int[] InventoryWeapons {
            get => _inventoryWeapons;
            set => _inventoryWeapons = value;
        }

        public long BankAmount {
            get => _bankAmount;
            set => _bankAmount = value;
        }

        public int[] InventoryCells {
            get => _inventoryCells;
            set => _inventoryCells = value;
        }

        public int WeaponBuyCount {
            get => _weaponBuyCount;
            set => _weaponBuyCount = value;
        }
    }
}