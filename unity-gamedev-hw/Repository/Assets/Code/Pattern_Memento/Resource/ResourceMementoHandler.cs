using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Pattern_Memento {

    public sealed class ResourceMementoHandler : IMementoHandler {
        private readonly ResourceService _resourceService;

        private List<Resource> _cach;
        private List<Resource> _restoreResources;

        public ResourceMementoHandler(ResourceService service) {
            _resourceService = service;
        }

        public IMementos SaveState(string name = "") {
            string mementoID = name == "" ? DateTime.Now.ToString("hhmmss") : name;

            _cach = _resourceService.GetResources().ToList();

            if (_cach.Count() == 0)
                throw new ArgumentNullException($"Resources List is empty!");

            List<ResourceMemento> mementolist = new List<ResourceMemento>();

            for (int i = 0; i < _cach.Count(); i++) {
                Resource iRecource = _cach.ElementAt(i);

                ResourceData data = new ResourceData(
                    iRecource.ID,
                    iRecource.Amount
                );

                ResourceMemento resourceMemento = new ResourceMemento(mementoID, data);

                mementolist.Add(resourceMemento);
            }

            Debug.Log($"ResourceMementosManager: SaveState success! The number of resurces: {_cach.Count()}");

            //PrintStatistic(mementolist);

            return new ResourceMementos(mementoID, mementolist);
        }

        public void RestoreState(IMementos memento) {

            if (_cach != null && _cach.Count > 0)
                _cach.Clear();
            else
                _cach = new();

            _cach.AddRange(_resourceService.GetResources().ToList());

            _restoreResources = new List<Resource>();
            ResourceMementos mementos = (ResourceMementos)memento;

            foreach (ResourceMemento iMemento in mementos.Mementos) {
                ResourceData data = iMemento.Data;

                if (CheckAvailabilityResourceOnMap(data.ID, out Resource resource)) {

                    SynchronizationResourceData(data, resource);

                    _cach.Remove(resource);
                    continue;
                }
            }

            Debug.Log($"The state of the [Resources] has been restored based on the memento: {memento.ID}");
        }

        private void SynchronizationResourceData(ResourceData data, Resource resource) {

            if (resource.gameObject.name != data.ID)
                resource.gameObject.name = data.ID;

            if (CheckAmountValue(data, resource) == false)
                UpdateAmount(data, resource);
        }

        private bool CheckAvailabilityResourceOnMap(string id, out Resource resource) {

            foreach (Resource iResource in _cach) {

                if (iResource.gameObject.name == id || iResource.ID == id) {
                    resource = iResource;

                    return true;
                }
            }

            resource = null;
            return false;
        }

        private bool CheckAmountValue(ResourceData data, Resource resource) {
            if (data.Amount != resource.Amount)
                return false;

            return true;
        }

        private void UpdateAmount(ResourceData data, Resource resource) {
            resource.Amount = data.Amount;
        }

        private void PrintStatistic(List<ResourceMemento> mementolist) {
            foreach (var iMemento in mementolist) {
                Debug.Log($"ResourceID: {iMemento.Data.ID}," +
                         $" Amount {iMemento.Data.Amount}");
            }
        }
    }
}

