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
    public static class AttackAPI
    {
        ///Keys
        public const int TargetTransform = 14; // ReactiveVariable<Transform>
        public const int AttackRequest = 15; // IEvent
        public const int AttackAction = 16; // IEvent
        public const int AttackEvent = 17; // IEvent<Bullet>
        public const int CanAttack = 18; // IValue<bool>
        public const int CanMeleeAttack = 19; // IValue<bool>
        public const int CanRangeAttack = 20; // IValue<bool>
        public const int TargetChanged = 21; // IEvent<Transform>
        public const int AttackTerminate = 44; // IEvent


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<Transform> GetTargetTransform(this IEntity obj) => obj.GetValue<ReactiveVariable<Transform>>(TargetTransform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTargetTransform(this IEntity obj, out ReactiveVariable<Transform> value) => obj.TryGetValue(TargetTransform, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTargetTransform(this IEntity obj, ReactiveVariable<Transform> value) => obj.AddValue(TargetTransform, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTargetTransform(this IEntity obj) => obj.HasValue(TargetTransform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTargetTransform(this IEntity obj) => obj.DelValue(TargetTransform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTargetTransform(this IEntity obj, ReactiveVariable<Transform> value) => obj.SetValue(TargetTransform, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent GetAttackRequest(this IEntity obj) => obj.GetValue<IEvent>(AttackRequest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackRequest(this IEntity obj, out IEvent value) => obj.TryGetValue(AttackRequest, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackRequest(this IEntity obj, IEvent value) => obj.AddValue(AttackRequest, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackRequest(this IEntity obj) => obj.HasValue(AttackRequest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackRequest(this IEntity obj) => obj.DelValue(AttackRequest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackRequest(this IEntity obj, IEvent value) => obj.SetValue(AttackRequest, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent GetAttackAction(this IEntity obj) => obj.GetValue<IEvent>(AttackAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackAction(this IEntity obj, out IEvent value) => obj.TryGetValue(AttackAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackAction(this IEntity obj, IEvent value) => obj.AddValue(AttackAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackAction(this IEntity obj) => obj.HasValue(AttackAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackAction(this IEntity obj) => obj.DelValue(AttackAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackAction(this IEntity obj, IEvent value) => obj.SetValue(AttackAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent<Bullet> GetAttackEvent(this IEntity obj) => obj.GetValue<IEvent<Bullet>>(AttackEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackEvent(this IEntity obj, out IEvent<Bullet> value) => obj.TryGetValue(AttackEvent, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackEvent(this IEntity obj, IEvent<Bullet> value) => obj.AddValue(AttackEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackEvent(this IEntity obj) => obj.HasValue(AttackEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackEvent(this IEntity obj) => obj.DelValue(AttackEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackEvent(this IEntity obj, IEvent<Bullet> value) => obj.SetValue(AttackEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValue<bool> GetCanAttack(this IEntity obj) => obj.GetValue<IValue<bool>>(CanAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanAttack(this IEntity obj, out IValue<bool> value) => obj.TryGetValue(CanAttack, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanAttack(this IEntity obj, IValue<bool> value) => obj.AddValue(CanAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanAttack(this IEntity obj) => obj.HasValue(CanAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanAttack(this IEntity obj) => obj.DelValue(CanAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanAttack(this IEntity obj, IValue<bool> value) => obj.SetValue(CanAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValue<bool> GetCanMeleeAttack(this IEntity obj) => obj.GetValue<IValue<bool>>(CanMeleeAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanMeleeAttack(this IEntity obj, out IValue<bool> value) => obj.TryGetValue(CanMeleeAttack, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanMeleeAttack(this IEntity obj, IValue<bool> value) => obj.AddValue(CanMeleeAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanMeleeAttack(this IEntity obj) => obj.HasValue(CanMeleeAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanMeleeAttack(this IEntity obj) => obj.DelValue(CanMeleeAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanMeleeAttack(this IEntity obj, IValue<bool> value) => obj.SetValue(CanMeleeAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValue<bool> GetCanRangeAttack(this IEntity obj) => obj.GetValue<IValue<bool>>(CanRangeAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanRangeAttack(this IEntity obj, out IValue<bool> value) => obj.TryGetValue(CanRangeAttack, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanRangeAttack(this IEntity obj, IValue<bool> value) => obj.AddValue(CanRangeAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanRangeAttack(this IEntity obj) => obj.HasValue(CanRangeAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanRangeAttack(this IEntity obj) => obj.DelValue(CanRangeAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanRangeAttack(this IEntity obj, IValue<bool> value) => obj.SetValue(CanRangeAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent<Transform> GetTargetChanged(this IEntity obj) => obj.GetValue<IEvent<Transform>>(TargetChanged);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTargetChanged(this IEntity obj, out IEvent<Transform> value) => obj.TryGetValue(TargetChanged, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTargetChanged(this IEntity obj, IEvent<Transform> value) => obj.AddValue(TargetChanged, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTargetChanged(this IEntity obj) => obj.HasValue(TargetChanged);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTargetChanged(this IEntity obj) => obj.DelValue(TargetChanged);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTargetChanged(this IEntity obj, IEvent<Transform> value) => obj.SetValue(TargetChanged, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent GetAttackTerminate(this IEntity obj) => obj.GetValue<IEvent>(AttackTerminate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackTerminate(this IEntity obj, out IEvent value) => obj.TryGetValue(AttackTerminate, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackTerminate(this IEntity obj, IEvent value) => obj.AddValue(AttackTerminate, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackTerminate(this IEntity obj) => obj.HasValue(AttackTerminate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackTerminate(this IEntity obj) => obj.DelValue(AttackTerminate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackTerminate(this IEntity obj, IEvent value) => obj.SetValue(AttackTerminate, value);
    }
}
