using Gameplay.Economic;
using UI;
using UI.Inventory;
using UI.Inventory.Merging;
using UnityEngine;

namespace Architecture.Services.Factories {
    public interface IUIFactory {
        GameObject CreateCampUI();
        GameObject CreateMergeWeapon(int level, MergeCell cell, Transform container, MergeArea mergeArea);
        GameObject[] CreateArsenal(Transform container);
    }
}