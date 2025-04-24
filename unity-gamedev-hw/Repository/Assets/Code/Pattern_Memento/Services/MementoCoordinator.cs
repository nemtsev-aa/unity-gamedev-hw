using R3;
using System;
using System.Collections.Generic;

namespace Pattern_Memento {
    public sealed class MementoCoordinator {
        private readonly IMementoServicesFactory _factory;
        private readonly Subject<Unit> _mementosCreated = new Subject<Unit>();
        private readonly Subject<Unit> _mementosRestored = new Subject<Unit>();

        public MementoCoordinator(IMementoServicesFactory factory) {
            _factory = factory;

            CreateServiceDictionary();
        }

        public Dictionary<MementoTypes, MementoServices> Services { get; private set; }
        public Observable<Unit> MementosCreated => _mementosCreated;
        public Observable<Unit> MementosRestored => _mementosRestored;

        public void CreateMementos() {

            if (Services.Values.Count == 0)
                throw new ArgumentNullException($"MementoServices Dictionary is empty!");

            foreach (MementoServices iService in Services.Values) {
                iService.Controller.AddMemento();
            }

            _mementosCreated.OnNext(Unit.Default);
        }

        public void RestoreMementos() {

            if (Services.Values.Count == 0)
                throw new ArgumentNullException($"MementoServices Dictionary is empty!");

            foreach (MementoServices iService in Services.Values) {

                if (iService.History.History.Count == 0)
                    throw new ArgumentNullException($"{iService.Type} History is empty!");

                iService.Controller.RestoreMemento();
            }

            _mementosRestored.OnNext(Unit.Default);
        }

        private void CreateServiceDictionary() {
            Services = new Dictionary<MementoTypes, MementoServices> {
                { MementoTypes.Units, _factory.Create(MementoTypes.Units) },
                { MementoTypes.Resources, _factory.Create(MementoTypes.Resources) }
            };
        }
    }
}

