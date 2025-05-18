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
    public static class AttackParametersAPI
    {
        ///Keys
        public const int Damage = 27; // int
        public const int Range = 28; // float
        public const int Delay = 29; // float


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDamage(this IEntity obj) => obj.GetValue<int>(Damage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetDamage(this IEntity obj, out int value) => obj.TryGetValue(Damage, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddDamage(this IEntity obj, int value) => obj.AddValue(Damage, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasDamage(this IEntity obj) => obj.HasValue(Damage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelDamage(this IEntity obj) => obj.DelValue(Damage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDamage(this IEntity obj, int value) => obj.SetValue(Damage, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetRange(this IEntity obj) => obj.GetValue<float>(Range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRange(this IEntity obj, out float value) => obj.TryGetValue(Range, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRange(this IEntity obj, float value) => obj.AddValue(Range, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRange(this IEntity obj) => obj.HasValue(Range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRange(this IEntity obj) => obj.DelValue(Range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRange(this IEntity obj, float value) => obj.SetValue(Range, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetDelay(this IEntity obj) => obj.GetValue<float>(Delay);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetDelay(this IEntity obj, out float value) => obj.TryGetValue(Delay, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddDelay(this IEntity obj, float value) => obj.AddValue(Delay, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasDelay(this IEntity obj) => obj.HasValue(Delay);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelDelay(this IEntity obj) => obj.DelValue(Delay);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDelay(this IEntity obj, float value) => obj.SetValue(Delay, value);
    }
}
