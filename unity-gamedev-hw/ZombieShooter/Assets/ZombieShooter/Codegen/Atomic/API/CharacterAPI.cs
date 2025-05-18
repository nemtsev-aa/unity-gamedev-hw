/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using Atomic.Entities;
using Atomic.Elements;
using AtomicFramework.ZombieShooter;
using AtomicFramework.InputSystem;
using AtomicFramework.CameraFollowSystem;
using ZombieShooter.SceneObjects;

namespace Atomic.Contexts
{
	public static class CharacterAPI
	{
		///Keys
		public const int Character = 2; // SceneEntity
		public const int InputConfig = 3; // InputConfig
		public const int InputController = 4; // InputController
		public const int CameraConfig = 5; // CameraConfig
		public const int Weapon = 13; // SceneEntity
		public const int CanAttack = 14; // IValue<bool>
		public const int CharacterConfig = 21; // CharacterConfig
		public const int CharactreCreaetEvent = 22; // IEvent


		///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SceneEntity GetCharacter(this IContext obj) => obj.ResolveValue<SceneEntity>(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCharacter(this IContext obj, out SceneEntity value) => obj.TryResolveValue(Character, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharacter(this IContext obj, SceneEntity value) => obj.AddValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacter(this IContext obj) => obj.DelValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCharacter(this IContext obj, SceneEntity value) => obj.SetValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacter(this IContext obj) => obj.HasValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static InputConfig GetInputConfig(this IContext obj) => obj.ResolveValue<InputConfig>(InputConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetInputConfig(this IContext obj, out InputConfig value) => obj.TryResolveValue(InputConfig, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddInputConfig(this IContext obj, InputConfig value) => obj.AddValue(InputConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelInputConfig(this IContext obj) => obj.DelValue(InputConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInputConfig(this IContext obj, InputConfig value) => obj.SetValue(InputConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasInputConfig(this IContext obj) => obj.HasValue(InputConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static InputController GetInputController(this IContext obj) => obj.ResolveValue<InputController>(InputController);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetInputController(this IContext obj, out InputController value) => obj.TryResolveValue(InputController, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddInputController(this IContext obj, InputController value) => obj.AddValue(InputController, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelInputController(this IContext obj) => obj.DelValue(InputController);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInputController(this IContext obj, InputController value) => obj.SetValue(InputController, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasInputController(this IContext obj) => obj.HasValue(InputController);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static CameraFollowConfig GetCameraConfig(this IContext obj) => obj.ResolveValue<CameraFollowConfig>(CameraConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCameraConfig(this IContext obj, out CameraFollowConfig value) => obj.TryResolveValue(CameraConfig, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCameraConfig(this IContext obj, CameraFollowConfig value) => obj.AddValue(CameraConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCameraConfig(this IContext obj) => obj.DelValue(CameraConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCameraConfig(this IContext obj, CameraFollowConfig value) => obj.SetValue(CameraConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCameraConfig(this IContext obj) => obj.HasValue(CameraConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SceneEntity GetWeapon(this IContext obj) => obj.ResolveValue<SceneEntity>(Weapon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWeapon(this IContext obj, out SceneEntity value) => obj.TryResolveValue(Weapon, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddWeapon(this IContext obj, SceneEntity value) => obj.AddValue(Weapon, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWeapon(this IContext obj) => obj.DelValue(Weapon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWeapon(this IContext obj, SceneEntity value) => obj.SetValue(Weapon, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWeapon(this IContext obj) => obj.HasValue(Weapon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<bool> GetCanAttack(this IContext obj) => obj.ResolveValue<IValue<bool>>(CanAttack);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCanAttack(this IContext obj, out IValue<bool> value) => obj.TryResolveValue(CanAttack, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCanAttack(this IContext obj, IValue<bool> value) => obj.AddValue(CanAttack, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCanAttack(this IContext obj) => obj.DelValue(CanAttack);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCanAttack(this IContext obj, IValue<bool> value) => obj.SetValue(CanAttack, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCanAttack(this IContext obj) => obj.HasValue(CanAttack);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static CharacterConfig GetCharacterConfig(this IContext obj) => obj.ResolveValue<CharacterConfig>(CharacterConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCharacterConfig(this IContext obj, out CharacterConfig value) => obj.TryResolveValue(CharacterConfig, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharacterConfig(this IContext obj, CharacterConfig value) => obj.AddValue(CharacterConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacterConfig(this IContext obj) => obj.DelValue(CharacterConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCharacterConfig(this IContext obj, CharacterConfig value) => obj.SetValue(CharacterConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacterConfig(this IContext obj) => obj.HasValue(CharacterConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent GetCharactreCreaetEvent(this IContext obj) => obj.ResolveValue<IEvent>(CharactreCreaetEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCharactreCreaetEvent(this IContext obj, out IEvent value) => obj.TryResolveValue(CharactreCreaetEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharactreCreaetEvent(this IContext obj, IEvent value) => obj.AddValue(CharactreCreaetEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharactreCreaetEvent(this IContext obj) => obj.DelValue(CharactreCreaetEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCharactreCreaetEvent(this IContext obj, IEvent value) => obj.SetValue(CharactreCreaetEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharactreCreaetEvent(this IContext obj) => obj.HasValue(CharactreCreaetEvent);
    }
}
