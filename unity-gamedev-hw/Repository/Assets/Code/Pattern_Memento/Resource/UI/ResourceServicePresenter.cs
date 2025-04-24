using GameEngine;
using R3;
using System;
using System.Linq;
using UnityEngine;

namespace Pattern_Memento {

    public sealed class ResourceServicePresenter : IDisposable {
        private readonly ResourceService _model;
        private readonly ResourceServiceView _view;
        private readonly CompositeDisposable _compositeDisposable = new();

        public ResourceServicePresenter(ResourceService model, ResourceServiceView view) {
            _model = model;
            _view = view;

            CreateReactiveSubscribes();
        }

        private void CreateReactiveSubscribes() {
            _view.OnAddButtonClicked
                .Subscribe(OnAddButtonClick)
                .AddTo(_compositeDisposable);

            _view.OnRemoveButtonClicked
                .Subscribe(OnRemoveButtonClick)
                .AddTo(_compositeDisposable);

        }

        private void OnAddButtonClick(R3.Unit unit) {
            var randomResource = GetRandomResource();

            randomResource.Amount++;

            Debug.Log($"Remove Unit: {randomResource.gameObject.name} {randomResource.Amount}");
        }

        private void OnRemoveButtonClick(R3.Unit unit) {
            var randomResource = GetRandomResource();

            randomResource.Amount--;

            Debug.Log($"Remove Unit: {randomResource.gameObject.name} {randomResource.Amount}");
        }

        private Resource GetRandomResource() {
            var resource = _model.GetResources();
            int randomIndex = UnityEngine.Random.Range(0, resource.Count());

            return resource.ElementAt(randomIndex);
        }

        public void Dispose() {

            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();
        }
    }
}
