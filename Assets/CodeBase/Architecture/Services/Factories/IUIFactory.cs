using UnityEngine;

namespace Architecture.Services.Factories {
    public interface IUIFactory {
        GameObject CreateCampUI();
        GameObject CreateMergeWeapon(int level, Vector3 position, Transform container);
        GameObject[] CreateArsenal(Transform container);
    }
}