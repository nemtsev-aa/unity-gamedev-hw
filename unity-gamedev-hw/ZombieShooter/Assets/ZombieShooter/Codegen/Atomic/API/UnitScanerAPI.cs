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
    public static class UnitScanerAPI
    {
        ///Keys
        public const int ScanRadius = 35; // float
        public const int ScanInreval = 36; // float
        public const int ScanerLayerMask = 37; // LayerMask
        public const int CanScane = 38; // ReactiveVariable<bool>
        public const int ClosestUnit = 39; // ReactiveVariable<Unit>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetScanRadius(this IEntity obj) => obj.GetValue<float>(ScanRadius);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetScanRadius(this IEntity obj, out float value) => obj.TryGetValue(ScanRadius, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddScanRadius(this IEntity obj, float value) => obj.AddValue(ScanRadius, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasScanRadius(this IEntity obj) => obj.HasValue(ScanRadius);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelScanRadius(this IEntity obj) => obj.DelValue(ScanRadius);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScanRadius(this IEntity obj, float value) => obj.SetValue(ScanRadius, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetScanInreval(this IEntity obj) => obj.GetValue<float>(ScanInreval);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetScanInreval(this IEntity obj, out float value) => obj.TryGetValue(ScanInreval, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddScanInreval(this IEntity obj, float value) => obj.AddValue(ScanInreval, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasScanInreval(this IEntity obj) => obj.HasValue(ScanInreval);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelScanInreval(this IEntity obj) => obj.DelValue(ScanInreval);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScanInreval(this IEntity obj, float value) => obj.SetValue(ScanInreval, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LayerMask GetScanerLayerMask(this IEntity obj) => obj.GetValue<LayerMask>(ScanerLayerMask);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetScanerLayerMask(this IEntity obj, out LayerMask value) => obj.TryGetValue(ScanerLayerMask, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddScanerLayerMask(this IEntity obj, LayerMask value) => obj.AddValue(ScanerLayerMask, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasScanerLayerMask(this IEntity obj) => obj.HasValue(ScanerLayerMask);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelScanerLayerMask(this IEntity obj) => obj.DelValue(ScanerLayerMask);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScanerLayerMask(this IEntity obj, LayerMask value) => obj.SetValue(ScanerLayerMask, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetCanScane(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(CanScane);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanScane(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(CanScane, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanScane(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(CanScane, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanScane(this IEntity obj) => obj.HasValue(CanScane);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanScane(this IEntity obj) => obj.DelValue(CanScane);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanScane(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(CanScane, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<Unit> GetClosestUnit(this IEntity obj) => obj.GetValue<ReactiveVariable<Unit>>(ClosestUnit);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetClosestUnit(this IEntity obj, out ReactiveVariable<Unit> value) => obj.TryGetValue(ClosestUnit, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddClosestUnit(this IEntity obj, ReactiveVariable<Unit> value) => obj.AddValue(ClosestUnit, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasClosestUnit(this IEntity obj) => obj.HasValue(ClosestUnit);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelClosestUnit(this IEntity obj) => obj.DelValue(ClosestUnit);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetClosestUnit(this IEntity obj, ReactiveVariable<Unit> value) => obj.SetValue(ClosestUnit, value);
    }
}
