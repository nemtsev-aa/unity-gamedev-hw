using System;
using UnityEngine;
using System.Collections.Generic;

namespace FarmingSystem {

    public class FellingZones : MonoBehaviour {
        [field: SerializeField] public List<FellingZone> Zones { get; private set; }

        private void OnValidate() {

            if (Zones.Count == 0)
                throw new ArgumentException($"{nameof(FellingZones)}: List is empty!");
        }
    }
}