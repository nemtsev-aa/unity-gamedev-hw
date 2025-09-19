using UnityEngine;

namespace Characters {

    public class Character : MonoBehaviour {
        [field: SerializeField] public CharacterTypes Type { get; private set; }
        [field: SerializeField] public CharacterStates State { get; private set; }

        public virtual void SetState(CharacterStates state) {
            State = state;
        }
    }
}