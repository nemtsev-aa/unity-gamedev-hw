using System;
using UnityEngine;
using System.Collections.Generic;

public class PatrolWaypoints : MonoBehaviour {
    [field: SerializeField] public List<Transform> Points { get; private set; }

    private void OnValidate() {

        if (Points.Count == 0)
            throw new ArgumentException($"{nameof(PatrolWaypoints)}: List is empty!");
    }
}
