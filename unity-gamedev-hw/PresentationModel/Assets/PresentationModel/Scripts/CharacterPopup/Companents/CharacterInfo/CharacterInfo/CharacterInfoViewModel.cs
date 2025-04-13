using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PresentationModel {
    public sealed class CharacterInfoViewModel : ICharacterInfoViewModel {
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private List<ICharacterStatViewModel> _statViewModels = new();

        public CharacterInfo CharacterInfo { get; }
        
        public CharacterInfoViewModel(CharacterInfo characterInfo) {
            CharacterInfo = characterInfo;

            CreateCharacterStatViewModels();
            CreateSubscribes();
        }

        public Observable<Unit> OnAnyStatChanged { get; private set; }
        public List<ICharacterStatViewModel> StatViewModels => _statViewModels;

        private void CreateCharacterStatViewModels() {
            var stateArray = CharacterInfo.Stats.CurrentValue;

            for (int i = 0; i < stateArray.Count; i++) {
                CharacterStatViewModel viewModel = new CharacterStatViewModel(stateArray[i]);

                _statViewModels.Add(viewModel);
            }
        }

        private void CreateSubscribes() {
            
            OnAnyStatChanged = CreateAnyStatChangedObservable();

            OnAnyStatChanged
                .Subscribe(AnyStatChanged)
                .AddTo(_compositeDisposable);

            CharacterInfo.ObserveCollectionChanged()
                .Subscribe(_ => {
                    Debug.Log("Collection changed!");
                    UpdateStatViewModels();
                })
                .AddTo(_compositeDisposable);
        }

        private Observable<Unit> CreateAnyStatChangedObservable() {
            // 1. Observable изменений коллекции
            var collectionChanges = CharacterInfo.ObserveCollectionChanged()
                .Select(_ => Unit.Default);

            // 2. Observable изменений значений характеристик
            var valueChanges = CharacterInfo.Stats
                 // Преобразуем List в Observable
                .Select(stats => stats.ToObservable())
                // Переключаемся на последнюю коллекцию
                .Switch()
                // Подписываемся на изменения каждого stat.Value
                .SelectMany(stat => stat.Value)
                // Преобразуем в Unit
                .Select(_ => Unit.Default); 

            // 3. Объединяем оба потока
            return Observable.Merge(collectionChanges, valueChanges)
                // Делаем горячий observable
                .Publish()
                // Автоматически управляем подпиской
                .RefCount(); 
        }

        private void AnyStatChanged(Unit unit) {
            
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