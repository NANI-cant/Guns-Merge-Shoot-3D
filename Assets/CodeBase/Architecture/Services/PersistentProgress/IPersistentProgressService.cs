using PersistentProgress;

namespace Architecture.Services.PersistentProgress {
    public interface IPersistentProgressService {
        public void Save();
        public void Load();
        
        public void AddReader(IProgressReader reader);
        public void RemoveReader(IProgressReader reader);
        
        public void AddWriter(IProgressWriter writer);
        public void RemoveWriter(IProgressWriter writer);
        IReadOnlyPlayerProgress PlayerProgress { get; }
    }
}