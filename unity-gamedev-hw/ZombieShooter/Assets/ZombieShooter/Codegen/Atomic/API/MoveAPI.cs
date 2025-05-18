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
    public static class MoveAPI
    {
        ///Keys
        public const int Root = 5; // Transform
        public const int MoveSpeed = 6; // ReactiveVariable<float>
        public const int MoveDirection = 7; // ReactiveVariable<Vector3>
        public const int IsMoving = 8; // ReactiveVariable<bool>
        public const int CanMove = 9; // IValue<bool>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform GetRoot(this IEntity obj) => obj.GetValue<Transform>(Root);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRoot(this IEntity obj, out Transform value) => obj.TryGetValue(Root, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRoot(this IEntity obj, Transform value) => obj.AddValue(Root, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRoot(this IEntity obj) => obj.HasValue(Root);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRoot(this IEntity obj) => obj.DelValue(Root);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRoot(this IEntity obj, Transform value) => obj.SetValue(Root, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<float> GetMoveSpeed(this IEntity obj) => obj.GetValue<ReactiveVariable<float>>(MoveSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMoveSpeed(this IEntity obj, out ReactiveVariable<float> value) => obj.TryGetValue(MoveSpeed, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMoveSpeed(this IEntity obj, ReactiveVariable<float> value) => obj.AddValue(MoveSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMoveSpeed(this IEntity obj) => obj.HasValue(MoveSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelMoveSpeed(this IEntity obj) => obj.DelValue(MoveSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMoveSpeed(this IEntity obj, ReactiveVariable<float> value) => obj.SetValue(MoveSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<Vector3> GetMoveDirection(this IEntity obj) => obj.GetValue<ReactiveVariable<Vector3>>(MoveDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMoveDirection(this IEntity obj, out ReactiveVariable<Vector3> value) => obj.TryGetValue(MoveDirection, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMoveDirection(this IEntity obj, ReactiveVariable<Vector3> value) => obj.AddValue(MoveDirection, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMoveDirection(this IEntity obj) => obj.HasValue(MoveDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelMoveDirection(this IEntity obj) => obj.DelValue(MoveDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMoveDirection(this IEntity obj, ReactiveVariable<Vector3> value) => obj.SetValue(MoveDirection, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsMoving(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsMoving);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsMoving(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsMoving, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsMoving(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsMoving, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsMoving(this IEntity obj) => obj.HasValue(IsMoving);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsMoving(this IEntity obj) => obj.DelValue(IsMoving);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsMoving(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsMoving, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValue<bool> GetCanMove(this IEntity obj) => obj.GetValue<IValue<bool>>(CanMove);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanMove(this IEntity obj, out IValue<bool> value) => obj.TryGetValue(CanMove, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanMove(this IEntity obj, IValue<bool> value) => obj.AddValue(CanMove, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanMove(this IEntity obj) => obj.HasValue(CanMove);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanMove(this IEntity obj) => obj.DelValue(CanMove);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanMove(this IEntity obj, IValue<bool> value) => obj.SetValue(CanMove, value);
    }
}
