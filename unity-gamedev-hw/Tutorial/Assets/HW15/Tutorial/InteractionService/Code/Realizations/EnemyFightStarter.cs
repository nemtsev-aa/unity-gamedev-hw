using Characters;
using UnityEngine;

namespace InteractionService {

    public sealed class EnemyFightStarter : InteractionSource {
        [field: SerializeField] public Character Character { get; private set; }
    }
}


