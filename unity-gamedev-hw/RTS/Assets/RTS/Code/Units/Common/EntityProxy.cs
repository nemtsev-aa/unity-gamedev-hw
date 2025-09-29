using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.OOP {

    public sealed class EntityProxy : MonoBehaviour {

        [field: SerializeField] public Entity Entity { get; private set; }
    }
 }
