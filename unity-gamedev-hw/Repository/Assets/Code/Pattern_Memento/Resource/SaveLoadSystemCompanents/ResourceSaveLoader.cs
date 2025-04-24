using System;
using UnityEngine;
using SaveLoadSystem.Core;
using System.Collections.Generic;

namespace Pattern_Memento {

    public sealed class ResourceSaveLoader : SaveLoader<MementoCoordinator, List<ResourceMementos>> {

        protected override List<ResourceMementos> ConvertToData(MementoCoordinator service) {

            var mementosList = service.Services[MementoTypes.Resources].History.History;

            List<ResourceMementos> mementos = new();

            if (mementosList.Count == 0)
                throw new ArgumentNullException($"Resources Memento History is empty!");

            for (int i = 0; i < mementosList.Count; i++) {
                IMementos memento = mementosList[i];
                mementos.Add((ResourceMementos)memento);
            }

            return mementos;
        }

        protected override void SetupData(MementoCoordinator service, List<ResourceMementos> data) {
            MementoHistory history = service.Services[MementoTypes.Resources].History;

            List<IMementos> mementos = new();
            mementos.AddRange(data);

            history.SetHistory(mementos);
        }

        protected override void SetupDefaultData(MementoCoordinator service) {
            Debug.Log($"<color=yellow> {nameof(ResourceSaveLoader)}: Setup default data! </color>");

            MementoHistory history = service.Services[MementoTypes.Resources].History;
            List<IMementos> mementos = new();

            history.SetHistory(mementos);
        }
    }
}

