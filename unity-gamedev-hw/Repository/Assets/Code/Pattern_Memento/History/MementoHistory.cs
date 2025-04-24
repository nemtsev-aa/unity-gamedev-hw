using ObservableCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pattern_Memento {

    public sealed class MementoHistory {

        private ObservableList<IMementos> _history;

        public IReadOnlyObservableList<IMementos> History => _history;

        public MementoHistory() {
            _history = new();
        }

        public void SetHistory(List<IMementos> data) {
            _history.Clear();

            for (int i = 0; i < data.Count; i++) {
                _history.Add(data[i]);
            }
        }

        public void AddMemento(IMementos memento) {

            if (_history.Contains(memento) == false)
                _history.Add(memento);

        }

        public void RemoveMemento(IMementos memento) {

            if (_history.Contains(memento) == true)
                _history.Remove(memento);

        }

        public IMementos GetLastMemento() {

            if (_history.Count == 0)
                throw new ArgumentNullException($"GameHistory is empty!");

            return _history[_history.Count - 1];
        }

        public IMementos GetMementosByID(string id) {
            for (int i = 0; i < _history.Count; i++) {
                var iMementos = _history[i];

                if (iMementos.ID == id)
                    return iMementos;
            }

            return null;
        }

        public void ShowHistory() {
            for (int i = 0; i < _history.Count; i++) {
                var iMemento = _history[i];
                Debug.Log($"[{i}] Lives {iMemento.ID}");
            }
        }
    }
}
