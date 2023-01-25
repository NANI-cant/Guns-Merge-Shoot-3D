using UnityEngine;

namespace PersistentProgress {
    public interface IReadOnlyPlayerProgress {
        int MaxWeaponLevel { get; }
        int[] InventoryWeapons { get; }
        int[] InventoryCells { get; }
        long BankAmount { get; }
        int WeaponBuyCount { get; set; }
    }
}