using System;
using UnityEngine;

namespace PersistentProgress {
    [Serializable]
    public class PlayerProgress: IReadOnlyPlayerProgress {
        private int _chosenWeaponLevel = 0;
        private int _maxWeaponLevel = 0;
        private int[] _inventoryWeapons = Array.Empty<int>();
        private Vector2[] _inventoryPositions = Array.Empty<Vector2>();
        private long _bankAmount = 0;

        public int ChosenWeaponLevel {
            get => _chosenWeaponLevel;
            set => _chosenWeaponLevel = value;
        }

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

        public Vector2[] InventoryPositions {
            get => _inventoryPositions;
            set => _inventoryPositions = value;
        }
    }
}