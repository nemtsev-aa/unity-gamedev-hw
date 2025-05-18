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
    public static class LifeAPI
    {
        ///Keys
        public const int HitPoints = 1; // ReactiveVariable<float>
        public const int IsDeath = 2; // ReactiveVariable<bool>
        public const int IsDestroy = 3; // BaseEvent<IEntity>
        public const int TakeDamageAction = 4; // BaseEvent<float>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<float> GetHitPoints(this IEntity obj) => obj.GetValue<ReactiveVariable<float>>(HitPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetHitPoints(this IEntity obj, out ReactiveVariable<float> value) => obj.TryGetValue(HitPoints, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddHitPoints(this IEntity obj, ReactiveVariable<float> value) => obj.AddValue(HitPoints, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasHitPoints(this IEntity obj) => obj.HasValue(HitPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelHitPoints(this IEntity obj) => obj.DelValue(HitPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHitPoints(this IEntity obj, ReactiveVariable<float> value) => obj.SetValue(HitPoints, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsDeath(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsDeath);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsDeath(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsDeath, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsDeath(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsDeath, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsDeath(this IEntity obj) => obj.HasValue(IsDeath);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsDeath(this IEntity obj) => obj.DelValue(IsDeath);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsDeath(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsDeath, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseEvent<IEntity> GetIsDestroy(this IEntity obj) => obj.GetValue<BaseEvent<IEntity>>(IsDestroy);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsDestroy(this IEntity obj, out BaseEvent<IEntity> value) => obj.TryGetValue(IsDestroy, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsDestroy(this IEntity obj, BaseEvent<IEntity> value) => obj.AddValue(IsDestroy, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsDestroy(this IEntity obj) => obj.HasValue(IsDestroy);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsDestroy(this IEntity obj) => obj.DelValue(IsDestroy);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsDestroy(this IEntity obj, BaseEvent<IEntity> value) => obj.SetValue(IsDestroy, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseEvent<float> GetTakeDamageAction(this IEntity obj) => obj.GetValue<BaseEvent<float>>(TakeDamageAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTakeDamageAction(this IEntity obj, out BaseEvent<float> value) => obj.TryGetValue(TakeDamageAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTakeDamageAction(this IEntity obj, BaseEvent<float> value) => obj.AddValue(TakeDamageAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTakeDamageAction(this IEntity obj) => obj.HasValue(TakeDamageAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTakeDamageAction(this IEntity obj) => obj.DelValue(TakeDamageAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTakeDamageAction(this IEntity obj, BaseEvent<float> value) => obj.SetValue(TakeDamageAction, value);
    }
}
