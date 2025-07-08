using System.Collections.Generic;

namespace Character.Core {

    public class CharacterStates {
        
        public CharacterStates(CharacterStatesProvider provider) {

            var states = provider.States;

            for (int i = 0; i < states.Count; i++) {
                var iState = states[i];
                States.Add(iState);
            }
        }

        public List<CharacterStat> States { get; private set; }
    }
}


