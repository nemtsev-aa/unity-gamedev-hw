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
    public static class RotateAPI
    {
        ///Keys
        public const int RotateSpeed = 10; // float
        public const int IsRotating = 11; // ReactiveVariable<bool>
        public const int RotationModes = 12; // RotationModes
        public const int SmoothTime = 13; // float
        public const int CanRotate = 40; // IValue<bool>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetRotateSpeed(this IEntity obj) => obj.GetValue<float>(RotateSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRotateSpeed(this IEntity obj, out float value) => obj.TryGetValue(RotateSpeed, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRotateSpeed(this IEntity obj, float value) => obj.AddValue(RotateSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRotateSpeed(this IEntity obj) => obj.HasValue(RotateSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRotateSpeed(this IEntity obj) => obj.DelValue(RotateSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRotateSpeed(this IEntity obj, float value) => obj.SetValue(RotateSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsRotating(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsRotating);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsRotating(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsRotating, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsRotating(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsRotating, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsRotating(this IEntity obj) => obj.HasValue(IsRotating);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsRotating(this IEntity obj) => obj.DelValue(IsRotating);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsRotating(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsRotating, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotationModes GetRotationModes(this IEntity obj) => obj.GetValue<RotationModes>(RotationModes);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRotationModes(this IEntity obj, out RotationModes value) => obj.TryGetValue(RotationModes, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRotationModes(this IEntity obj, RotationModes value) => obj.AddValue(RotationModes, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRotationModes(this IEntity obj) => obj.HasValue(RotationModes);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRotationModes(this IEntity obj) => obj.DelValue(RotationModes);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRotationModes(this IEntity obj, RotationModes value) => obj.SetValue(RotationModes, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetSmoothTime(this IEntity obj) => obj.GetValue<float>(SmoothTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSmoothTime(this IEntity obj, out float value) => obj.TryGetValue(SmoothTime, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddSmoothTime(this IEntity obj, float value) => obj.AddValue(SmoothTime, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasSmoothTime(this IEntity obj) => obj.HasValue(SmoothTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelSmoothTime(this IEntity obj) => obj.DelValue(SmoothTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSmoothTime(this IEntity obj, float value) => obj.SetValue(SmoothTime, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValue<bool> GetCanRotate(this IEntity obj) => obj.GetValue<IValue<bool>>(CanRotate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanRotate(this IEntity obj, out IValue<bool> value) => obj.TryGetValue(CanRotate, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanRotate(this IEntity obj, IValue<bool> value) => obj.AddValue(CanRotate, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanRotate(this IEntity obj) => obj.HasValue(CanRotate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanRotate(this IEntity obj) => obj.DelValue(CanRotate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanRotate(this IEntity obj, IValue<bool> value) => obj.SetValue(CanRotate, value);
    }
}
