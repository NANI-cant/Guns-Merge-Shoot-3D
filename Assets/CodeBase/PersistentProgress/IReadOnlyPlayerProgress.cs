using UnityEngine;

namespace PersistentProgress {
    public interface IReadOnlyPlayerProgress {
        public int ChosenWeaponLevel { get; }
        public int MaxWeaponLevel { get; }
        public int[] InventoryWeapons { get; }
        public Vector2[] InventoryPositions { get; }
        public long BankAmount { get; }
    }
}