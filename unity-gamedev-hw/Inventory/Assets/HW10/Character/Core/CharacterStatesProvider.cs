using Character.Core;
using System;
using System.Collections.Generic;

namespace Character {

    [Serializable]
    public class CharacterStatesProvider {
        private readonly List<CharacterStat> _states;

        public IReadOnlyList<CharacterStat> States => _states;

        public CharacterStatesProvider(CharacterModel model) {
            _states = model.States;
        }

        public bool TryGetStateByName(string statName, out CharacterStat stat) {

            if (statName != "") {

                for (int i = 0; i < _states.Count; i++) {
                    var iState = _states[i];

                    if (statName == iState.Name) {
                        stat = iState;
                        return true;
                    }
                }
            }

            stat = null;
            return false;
        }
    }
}

