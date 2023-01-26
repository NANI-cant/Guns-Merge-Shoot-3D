using Metric.Levels.Stages;
using UI.Inventory.Merging;
using UnityEngine;

namespace Architecture.Services.Factories {
    public interface IUIFactory {
        GameObject CreateCampUI();
        GameObject CreateMergeWeapon(int level, MergeCell cell, Transform container, MergeArea mergeArea);
        GameObject CreateHUD();
        GameObject[] CreateArsenal(Transform container);
        GameObject CreateStage(Transform container, float anchor, StageData trackedStage);
    }
}