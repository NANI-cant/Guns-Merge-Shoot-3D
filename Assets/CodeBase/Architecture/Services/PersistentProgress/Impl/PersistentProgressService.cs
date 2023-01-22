using System.Collections.Generic;
using PersistentProgress;

namespace Architecture.Services.PersistentProgress.Impl {
    public class PersistentProgressService: IPersistentProgressService {
        private readonly ISaveLoadService _saveLoadService;
        private readonly List<IProgressReader> _readers = new();
        private readonly List<IProgressWriter> _writers = new();
        
        private PlayerProgress _playerProgress;

        public IReadOnlyPlayerProgress PlayerProgress => _playerProgress;

        public PersistentProgressService(
            ISaveLoadService saveLoadService
        ) {
            _saveLoadService = saveLoadService;
        }

        public void Save() {
            foreach (var writer in _writers) {
                writer.Write(_playerProgress);
            }
            _saveLoadService.Save(_playerProgress);
        }

        public void Load() {
            _playerProgress = _saveLoadService.Load();
            foreach (var reader in _readers) {
                reader.Read(_playerProgress);
            }
        }

        public void AddReader(IProgressReader reader) => _readers.Add(reader);
        public void RemoveReader(IProgressReader reader) => _readers.Remove(reader);

        public void AddWriter(IProgressWriter writer) => _writers.Add(writer);
        public void RemoveWriter(IProgressWriter writer) => _writers.Remove(writer);
    }
}