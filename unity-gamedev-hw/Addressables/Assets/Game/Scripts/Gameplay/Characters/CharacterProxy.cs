using UnityEngine;

namespace CharactersSystem {
    public sealed class CharacterProxy : MonoBehaviour {
        [field: SerializeField] public Character Character { get; private set; }
    }
}