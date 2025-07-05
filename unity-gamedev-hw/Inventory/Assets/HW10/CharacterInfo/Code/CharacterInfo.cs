using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using Character;
using Character.Core;

namespace CharacterInfoWidget {

    public sealed class CharacterInfo {
        private readonly List<CharacterStat> _statsList = new();
        private readonly Subject<Unit> _collectionChanged = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        public ReadOnlyReactiveProperty<IReadOnlyList<CharacterStat>> Stats { get; private set; }
        public IObservable<Unit> OnAnyStatChanged { get; }

        public CharacterInfo(CharacterStatesProvider provider) {

            foreach (var iState in provider.States) {
                AddStat(iState);
            }

            CreateSubscribes();
        }

        private void AddStat(CharacterStat stat) {

            if (_statsList.Any(s => s.Name == stat.Name) == true)
                throw new ArgumentException($"Stat '{stat.Name}' already exists");

            _statsList.Add(stat);
            _collectionChanged.OnNext(Unit.Default);
        }

        public Observable<Unit> ObserveCollectionChanged() {
            return _collectionChanged;
        }

        public Observable<int> ObserveStatValue(string statName) {
            var stat = _statsList.FirstOrDefault(s => s.Name == statName);
            return stat?.Value ?? Observable.Return(0);
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