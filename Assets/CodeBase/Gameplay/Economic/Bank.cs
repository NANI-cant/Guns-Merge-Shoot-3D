using System;
using Architecture.Services.PersistentProgress;
using PersistentProgress;

namespace Gameplay.Economic {
    public class Bank: IProgressReader, IProgressWriter {
        private readonly IPersistentProgressService _persistentProgressService;
        public event Action Modified;
        
        public long Amount { get; private set; }

        public Bank(IPersistentProgressService persistentProgressService) {
            _persistentProgressService = persistentProgressService;
            _persistentProgressService.AddReader(this);
            _persistentProgressService.AddWriter(this);
            Amount = 12345;
        }

        public void Earn(long amount) {
            if (amount <= 0) throw new ArgumentException($"Earn amount must be > 0, now {amount}");

            Amount += amount;
            _persistentProgressService.Save();
            Modified?.Invoke();
        }

        public void Spend(long amount) {
            if (amount <= 0) throw new ArgumentException($"Spend amount must be > 0, now {amount}");
            if (Amount < amount) return;

            Amount -= amount;
            _persistentProgressService.Save();
            Modified?.Invoke();
        }

        public void Read(IReadOnlyPlayerProgress playerProgress) {
            Amount = playerProgress.BankAmount;
            Modified?.Invoke();
        }

        public bool IsCanSpend(long amount) => Amount >= amount;
        public void Write(PlayerProgress playerProgress) => playerProgress.BankAmount = Amount;
    }
}