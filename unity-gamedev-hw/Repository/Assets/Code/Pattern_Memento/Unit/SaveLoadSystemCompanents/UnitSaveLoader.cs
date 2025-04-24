using SaveLoadSystem.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pattern_Memento {

    public class UnitSaveLoader : SaveLoader<MementoCoordinator, List<UnitMementos>> {

        protected override List<UnitMementos> ConvertToData(MementoCoordinator service) {
            var mementosList = service.Services[MementoTypes.Units].History.History;

            if (mementosList.Count == 0) {
                Debug.LogWarning("Units Memento History is empty! Creating empty list.");
                return new List<UnitMementos>();
            }

            return mementosList.OfType<UnitMementos>().ToList();
        }

        protected override void SetupData(MementoCoordinator service, List<UnitMementos> data) {
            MementoHistory history = service.Services[MementoTypes.Units].History;
            List<IMementos> mementos = new List<IMementos>(data);
            
            history.SetHistory(mementos);
        }

        protected override void SetupDefaultData(MementoCoordinator service) {
            Debug.Log($"<color=yellow> {nameof(UnitSaveLoader)}: Setup default data! </color>");
            MementoHistory history = service.Services[MementoTypes.Units].History;
            
            history.SetHistory(new List<IMementos>());
        }
    }
}

