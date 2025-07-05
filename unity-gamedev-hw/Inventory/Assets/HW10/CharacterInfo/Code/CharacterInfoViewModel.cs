using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Character.UI;

namespace CharacterInfoWidget {
    public sealed class CharacterInfoViewModel : ICharacterInfoViewModel {
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private List<ICharacterStatViewModel> _statViewModels = new();

        public CharacterInfo CharacterInfo { get; }
        public Observable<Unit> OnAnyStatChanged { get; private set; }
        public List<ICharacterStatViewModel> StatViewModels => _statViewModels;

        public CharacterInfoViewModel(CharacterInfo characterInfo) {
            CharacterInfo = characterInfo;

            CreateCharacterStatViewModels();
            CreateSubscribes();
        }

        private void CreateCharacterStatViewModels() {
            var stateArray = CharacterInfo.Stats.CurrentValue;

            for (int i = 0; i < stateArray.Count; i++) {
                CharacterStatViewModel viewModel = new CharacterStatViewModel(stateArray[i]);

                _statViewModels.Add(viewModel);
            }
        }

        private void CreateSubscribes() {
            
            OnAnyStatChanged = CreateAnyStatChangedObservable();

            CharacterInfo.ObserveCollectionChanged()
                .Subscribe(_ => {
                    Debug.Log("Collection changed!");
                    UpdateStatViewModels();
                })
                .AddTo(_compositeDisposable);
        }

        private Observable<Unit> CreateAnyStatChangedObservable() {

            var collectionChanges = CharacterInfo.ObserveCollectionChanged()
                .Select(_ => Unit.Default);

            var valueChanges = CharacterInfo.Stats
                .Select(stats => stats.ToObservable())
                .Switch()
                .SelectMany(stat => stat.Value)
                .Select(_ => Unit.Default); 

              return Observable.Merge(collectionChanges, valueChanges)
                .Publish()
                .RefCount(); 
        }

        private void UpdateStatViewModels() {
            
            foreach (var iViewModel in _statViewModels) {
                
                if (iViewModel is IDisposable disposable)
                    disposable.Dispose();
            }

            _statViewModels.Clear();

            CreateCharacterStatViewModels();
        }

        public void Dispose() {
            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();
        }
    }
}