using UnityEngine;

namespace Architecture.Services.Factories {
    public interface IUIFactory {
        GameObject CreateCampUI();
        GameObject[] CreateArsenal(Transform container);
    }
}