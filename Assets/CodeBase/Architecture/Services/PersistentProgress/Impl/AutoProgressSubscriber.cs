using PersistentProgress;
using Zenject;

namespace Architecture.Services.PersistentProgress.Impl {
    public class AutoProgressSubscriber: IInitializable {
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IProgressWriter[] _progressWriter;
        private readonly IProgressReader[] _progressReaders;

        public AutoProgressSubscriber(
            IPersistentProgressService persistentProgressService,
            IProgressWriter[] progressWriter,
            IProgressReader[] progressReaders
        ) {
            _persistentProgressService = persistentProgressService;
            _progressWriter = progressWriter;
            _progressReaders = progressReaders;
        }

        public void Initialize() {
            foreach (var writer in _progressWriter) {
                _persistentProgressService.AddWriter(writer);
            }
            foreach (var reader in _progressReaders) {
                _persistentProgressService.AddReader(reader);
            }
        }
    }
}