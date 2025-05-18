/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using AtomicFramework.Effects;

namespace Atomic.Contexts
{
	public static class EffectSystemAPI
	{
		///Keys
		public const int EffectSystemConfig = 18; // EffectSystemConfig


		///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EffectSystemConfig GetEffectSystemConfig(this IContext obj) => obj.ResolveValue<EffectSystemConfig>(EffectSystemConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEffectSystemConfig(this IContext obj, out EffectSystemConfig value) => obj.TryResolveValue(EffectSystemConfig, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddEffectSystemConfig(this IContext obj, EffectSystemConfig value) => obj.AddValue(EffectSystemConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEffectSystemConfig(this IContext obj) => obj.DelValue(EffectSystemConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEffectSystemConfig(this IContext obj, EffectSystemConfig value) => obj.SetValue(EffectSystemConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEffectSystemConfig(this IContext obj) => obj.HasValue(EffectSystemConfig);
    }
}
