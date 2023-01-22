using PersistentProgress;

namespace Architecture.Services.PersistentProgress {
    public interface ISaveLoadService {
        public void Save(PlayerProgress playerProgress);
        public PlayerProgress Load();
    }
}