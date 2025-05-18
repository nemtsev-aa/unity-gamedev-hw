/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using AtomicFramework.BulletSystem;
using AtomicFramework.ZombieShooter;

namespace Atomic.Contexts
{
	public static class WeaponAPI
	{
		///Keys
		public const int BulletSystemConfig = 15; // BulletSystemConfig
		public const int BulletConfig = 16; // BulletConfig
		public const int BulletSystemData = 17; // BulletSystemData


		///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static BulletSystemConfig GetBulletSystemConfig(this IContext obj) => obj.ResolveValue<BulletSystemConfig>(BulletSystemConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetBulletSystemConfig(this IContext obj, out BulletSystemConfig value) => obj.TryResolveValue(BulletSystemConfig, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddBulletSystemConfig(this IContext obj, BulletSystemConfig value) => obj.AddValue(BulletSystemConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelBulletSystemConfig(this IContext obj) => obj.DelValue(BulletSystemConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetBulletSystemConfig(this IContext obj, BulletSystemConfig value) => obj.SetValue(BulletSystemConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasBulletSystemConfig(this IContext obj) => obj.HasValue(BulletSystemConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static BulletConfig GetBulletConfig(this IContext obj) => obj.ResolveValue<BulletConfig>(BulletConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetBulletConfig(this IContext obj, out BulletConfig value) => obj.TryResolveValue(BulletConfig, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddBulletConfig(this IContext obj, BulletConfig value) => obj.AddValue(BulletConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelBulletConfig(this IContext obj) => obj.DelValue(BulletConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetBulletConfig(this IContext obj, BulletConfig value) => obj.SetValue(BulletConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasBulletConfig(this IContext obj) => obj.HasValue(BulletConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static BulletSystemData GetBulletSystemData(this IContext obj) => obj.ResolveValue<BulletSystemData>(BulletSystemData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetBulletSystemData(this IContext obj, out BulletSystemData value) => obj.TryResolveValue(BulletSystemData, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddBulletSystemData(this IContext obj, BulletSystemData value) => obj.AddValue(BulletSystemData, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelBulletSystemData(this IContext obj) => obj.DelValue(BulletSystemData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetBulletSystemData(this IContext obj, BulletSystemData value) => obj.SetValue(BulletSystemData, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasBulletSystemData(this IContext obj) => obj.HasValue(BulletSystemData);
    }
}
