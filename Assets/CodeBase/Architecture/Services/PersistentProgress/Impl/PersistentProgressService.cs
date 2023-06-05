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
            _playerProgress = new PlayerProgress();
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

        public void AddReader(params IProgressReader[] readers) => _readers.AddRange(readers);
        public void AddWriter(params IProgressWriter[] writers) => _writers.AddRange(writers);

        public void RemoveReader(params IProgressReader[] readers) {
            foreach (var reader in readers) {
                _readers.Remove(reader);    
            }
        }

        public void RemoveWriter(params IProgressWriter[] writers) {
            foreach (var writer in writers) {
                _writers.Remove(writer);    
            }
        }
    }
}