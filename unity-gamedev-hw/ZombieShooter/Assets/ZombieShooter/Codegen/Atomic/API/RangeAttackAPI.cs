/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;
using AtomicFramework.ZombieShooter;
using System.Collections.Generic;
using AtomicFramework.BulletSystem;
using AtomicFramework.View.Visual;
using AtomicFramework.Effects;
using AtomicFramework.View.VFX;
using AtomicFramework.View.SFX;
using AtomicFramework.RotationCompanent;
using ZombieShooter.SceneObjects;
using AtomicFramework.CameraFollowSystem;
using AtomicFramework.InputSystem;

namespace Atomic.Entities
{
    public static class RangeAttackAPI
    {
        ///Keys
        public const int CurrentBulletAmount = 22; // ReactiveVariable<int>
        public const int MaxBulletAmount = 23; // ReactiveVariable<int>
        public const int IsWithinReach = 24; // ReactiveVariable<bool>
        public const int FirePoint = 25; // FirePoint
        public const int BulletPrefab = 26; // Bullet
        public const int PointerPosition = 43; // ReactiveVariable<Vector3>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<int> GetCurrentBulletAmount(this IEntity obj) => obj.GetValue<ReactiveVariable<int>>(CurrentBulletAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCurrentBulletAmount(this IEntity obj, out ReactiveVariable<int> value) => obj.TryGetValue(CurrentBulletAmount, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCurrentBulletAmount(this IEntity obj, ReactiveVariable<int> value) => obj.AddValue(CurrentBulletAmount, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCurrentBulletAmount(this IEntity obj) => obj.HasValue(CurrentBulletAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCurrentBulletAmount(this IEntity obj) => obj.DelValue(CurrentBulletAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCurrentBulletAmount(this IEntity obj, ReactiveVariable<int> value) => obj.SetValue(CurrentBulletAmount, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<int> GetMaxBulletAmount(this IEntity obj) => obj.GetValue<ReactiveVariable<int>>(MaxBulletAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMaxBulletAmount(this IEntity obj, out ReactiveVariable<int> value) => obj.TryGetValue(MaxBulletAmount, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMaxBulletAmount(this IEntity obj, ReactiveVariable<int> value) => obj.AddValue(MaxBulletAmount, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMaxBulletAmount(this IEntity obj) => obj.HasValue(MaxBulletAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelMaxBulletAmount(this IEntity obj) => obj.DelValue(MaxBulletAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMaxBulletAmount(this IEntity obj, ReactiveVariable<int> value) => obj.SetValue(MaxBulletAmount, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsWithinReach(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsWithinReach);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsWithinReach(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsWithinReach, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsWithinReach(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsWithinReach, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsWithinReach(this IEntity obj) => obj.HasValue(IsWithinReach);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsWithinReach(this IEntity obj) => obj.DelValue(IsWithinReach);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsWithinReach(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsWithinReach, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FirePoint GetFirePoint(this IEntity obj) => obj.GetValue<FirePoint>(FirePoint);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFirePoint(this IEntity obj, out FirePoint value) => obj.TryGetValue(FirePoint, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddFirePoint(this IEntity obj, FirePoint value) => obj.AddValue(FirePoint, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFirePoint(this IEntity obj) => obj.HasValue(FirePoint);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelFirePoint(this IEntity obj) => obj.DelValue(FirePoint);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetFirePoint(this IEntity obj, FirePoint value) => obj.SetValue(FirePoint, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bullet GetBulletPrefab(this IEntity obj) => obj.GetValue<Bullet>(BulletPrefab);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetBulletPrefab(this IEntity obj, out Bullet value) => obj.TryGetValue(BulletPrefab, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddBulletPrefab(this IEntity obj, Bullet value) => obj.AddValue(BulletPrefab, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasBulletPrefab(this IEntity obj) => obj.HasValue(BulletPrefab);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelBulletPrefab(this IEntity obj) => obj.DelValue(BulletPrefab);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBulletPrefab(this IEntity obj, Bullet value) => obj.SetValue(BulletPrefab, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<Vector3> GetPointerPosition(this IEntity obj) => obj.GetValue<ReactiveVariable<Vector3>>(PointerPosition);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetPointerPosition(this IEntity obj, out ReactiveVariable<Vector3> value) => obj.TryGetValue(PointerPosition, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddPointerPosition(this IEntity obj, ReactiveVariable<Vector3> value) => obj.AddValue(PointerPosition, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasPointerPosition(this IEntity obj) => obj.HasValue(PointerPosition);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelPointerPosition(this IEntity obj) => obj.DelValue(PointerPosition);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPointerPosition(this IEntity obj, ReactiveVariable<Vector3> value) => obj.SetValue(PointerPosition, value);
    }
}
