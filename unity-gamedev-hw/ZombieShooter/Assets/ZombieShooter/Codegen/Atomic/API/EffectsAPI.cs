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
    public static class EffectsAPI
    {
        ///Keys
        public const int StunAction = 41; // IEvent<bool>
        public const int SpeedBoostAction = 42; // IEvent<bool>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent<bool> GetStunAction(this IEntity obj) => obj.GetValue<IEvent<bool>>(StunAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetStunAction(this IEntity obj, out IEvent<bool> value) => obj.TryGetValue(StunAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddStunAction(this IEntity obj, IEvent<bool> value) => obj.AddValue(StunAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasStunAction(this IEntity obj) => obj.HasValue(StunAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelStunAction(this IEntity obj) => obj.DelValue(StunAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetStunAction(this IEntity obj, IEvent<bool> value) => obj.SetValue(StunAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent<bool> GetSpeedBoostAction(this IEntity obj) => obj.GetValue<IEvent<bool>>(SpeedBoostAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSpeedBoostAction(this IEntity obj, out IEvent<bool> value) => obj.TryGetValue(SpeedBoostAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddSpeedBoostAction(this IEntity obj, IEvent<bool> value) => obj.AddValue(SpeedBoostAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasSpeedBoostAction(this IEntity obj) => obj.HasValue(SpeedBoostAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelSpeedBoostAction(this IEntity obj) => obj.DelValue(SpeedBoostAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSpeedBoostAction(this IEntity obj, IEvent<bool> value) => obj.SetValue(SpeedBoostAction, value);
    }
}
