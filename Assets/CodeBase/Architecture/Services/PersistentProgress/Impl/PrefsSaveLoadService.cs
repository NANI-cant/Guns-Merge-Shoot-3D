using PersistentProgress;
using UnityEngine;

namespace Architecture.Services.PersistentProgress.Impl {
    public class PrefsSaveLoadService : ISaveLoadService {
        private const string PlayerProgressKey = "PlayerProgress";

        public void Save(PlayerProgress playerProgress) {
            string json = JsonUtility.ToJson(playerProgress);
            PlayerPrefs.SetString(PlayerProgressKey, json);
        }

        public PlayerProgress Load() {
            string json = PlayerPrefs.GetString(PlayerProgressKey, "");
            var playerProgress = JsonUtility.FromJson<PlayerProgress>(json) ?? new PlayerProgress();
            return playerProgress;
        }
    }
}