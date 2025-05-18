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
    public static class VFXAPI
    {
        ///Keys
        public const int VFXCollection = 32; // List<VFXData>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<VFXData> GetVFXCollection(this IEntity obj) => obj.GetValue<List<VFXData>>(VFXCollection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetVFXCollection(this IEntity obj, out List<VFXData> value) => obj.TryGetValue(VFXCollection, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddVFXCollection(this IEntity obj, List<VFXData> value) => obj.AddValue(VFXCollection, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasVFXCollection(this IEntity obj) => obj.HasValue(VFXCollection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelVFXCollection(this IEntity obj) => obj.DelValue(VFXCollection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetVFXCollection(this IEntity obj, List<VFXData> value) => obj.SetValue(VFXCollection, value);
    }
}
