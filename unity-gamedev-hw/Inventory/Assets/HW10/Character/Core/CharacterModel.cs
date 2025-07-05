using System;
using Character.Core;
using Sirenix.OdinInspector;
using System.Linq;
using System.Collections.Generic;

namespace Character {

    [Serializable]
    public sealed class CharacterModel {
        [ShowInInspector] public List<CharacterStat> States { get; private set; }

        public CharacterModel(CharacterDefaultData defaultData) {

            States = new();

            for (int i = 0; i < defaultData.DafaultData.Count; i++) {
                var iData = defaultData.DafaultData[i];

                States.Add(new CharacterStat(iData.Name, iData.Value));
            }
        }

        public bool RemoveStat(string statName) {
            var stat = States.FirstOrDefault(s => s.Name == statName);

            if (stat != null && States.Remove(stat) == true) {
                return true;
            }

            return false;
        }
    }
}
