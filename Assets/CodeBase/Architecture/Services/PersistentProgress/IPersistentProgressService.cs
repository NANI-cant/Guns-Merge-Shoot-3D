﻿using PersistentProgress;

namespace Architecture.Services.PersistentProgress {
    public interface IPersistentProgressService {
        public void Save();
        public void Load();
        
        public void AddReader(params IProgressReader[] readers);
        public void RemoveReader(params IProgressReader[] readers);
        
        public void AddWriter(params IProgressWriter[] writers);
        public void RemoveWriter(params IProgressWriter[] writers);
        IReadOnlyPlayerProgress PlayerProgress { get; }
    }
}