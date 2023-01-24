using UnityEngine;

namespace PersistentProgress {
    public interface IReadOnlyPlayerProgress {
        public int ChosenWeaponLevel { get; }
        public int MaxWeaponLevel { get; }
        public int[] InventoryWeapons { get; }
        public int[] InventoryCells { get; }
        public long BankAmount { get; }
    }
}