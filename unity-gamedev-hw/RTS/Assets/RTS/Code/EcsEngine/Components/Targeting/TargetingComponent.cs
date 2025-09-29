using System;
using UnityEngine;

namespace Client.Components.Targeting {

    [Serializable]
    public struct TargetingComponent {
        public int CurrentTargetId;
        public Vector3 LastKnownTargetPosition;
        public float LastTargetUpdateTime;
        public TargetPriority Priority;
        public bool CanChangeTarget;
    }

    [Serializable]
    public struct TargetableComponent {
        public bool IsActive;
        public float ThreatLevel;
        public TargetType Type;
    }

    [Serializable]
    public struct VisionComponent {
        public float VisionRange;
        public float VisionAngle; // в градусах
        public LayerMask VisionBlockingLayers;
    }

    [Serializable]
    public enum TargetPriority {
        Lowest = 0,
        Low = 1,
        Medium = 2,
        High = 3,
        Highest = 4
    }

    [Serializable]
    public enum TargetType {
        Unit = 0,
        Building = 1,
        Resource = 2,
        Hero = 3
    }

    [Serializable]
    public struct TargetCandidate {
        public int EntityId;
        public float Score;
        public Vector3 Position;
        public float Distance;
        public Collider Collider;

        public void SetScore(float score) {
            Score = score;
        }
    }

}