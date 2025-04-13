using R3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PresentationModel {
    public sealed class CharacterInfo {
        private readonly List<CharacterStat> _statsList = new();
        private readonly Subject<Unit> _collectionChanged = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        public ReadOnlyReactiveProperty<IReadOnlyList<CharacterStat>> Stats { get; private set; }
        public IObservable<Unit> OnAnyStatChanged { get; }

        public CharacterInfo(CharacterStatConfigs configs) {
            CreateCharacterStats(configs);
            CreateSubscribes();
        }

        public void AddStat(CharacterStat stat) {
            if (_statsList.Any(s => s.Name == stat.Name)) {
                throw new ArgumentException($"Stat '{stat.Name}' already exists");
            }

            _statsList.Add(stat);
            _collectionChanged.OnNext(Unit.Default); // Уведомляем об изменении
        }

        public bool RemoveStat(string statName) {
            var stat = _statsList.FirstOrDefault(s => s.Name == statName);
            if (stat != null && _statsList.Remove(stat)) {
                _collectionChanged.OnNext(Unit.Default);
                return true;
            }
            return false;
        }

        public bool TryGetStatByIndex(int index, out CharacterStat stat) {
            if (index <= _statsList.Count - 1) {
                stat = _statsList[index];
                return true;
            }

            stat = null;
            return false;
        }

        public bool TryGetStatByName(string statName, out CharacterStat stat) {
            if (statName != "") {
                stat = _statsList.FirstOrDefault(s => s.Name == statName);
                return true;
            }

            stat = null;
            return false;
        }

        public Observable<Unit> ObserveCollectionChanged() {
            return _collectionChanged;
        }

        public Observable<int> ObserveStatValue(string statName) {
            var stat = _statsList.FirstOrDefault(s => s.Name == statName);
            return stat?.Value ?? Observable.Return(0);
        }

        private void CreateCharacterStats(CharacterStatConfigs configs) {
            foreach (CharacterStatConfig iConfig in configs.Configs) {
                CharacterStat stat = new CharacterStat(iConfig.Name, iConfig.Value);
                AddStat(stat);
            }
        }

        private void CreateSubscribes() {
            Stats = _collectionChanged
                .Select(_ => (IReadOnlyList<CharacterStat>)_statsList.AsReadOnly())
                .ToReadOnlyReactiveProperty(_statsList.AsReadOnly())
                .AddTo(_compositeDisposable);
        }
        public void Dispose() {
            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();
        }
    }
}