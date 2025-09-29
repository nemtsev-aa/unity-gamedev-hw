using Client.Components.Common;
using System;
using UnityEngine;

namespace GameplayCompanents {

    [Serializable]
    public sealed class ContainersPresenter {
        [field: SerializeField] public Transform WorldContainer { get; private set; }
        [field: SerializeField] public Transform ArcherContainer { get; private set; }
        [field: SerializeField] public Transform SwordsmanContainer { get; private set; }

        public Transform GetContainerByUnitType(UnitTypes type) {

            switch (type) {
                case UnitTypes.Archer:
                    return ArcherContainer;

                case UnitTypes.Swordsman:
                    return SwordsmanContainer;

                default:
                    throw new ArgumentNullException($"Invalid UnitTypes {type}!");
            }
        }
    }
}