/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using Atomic.Entities;
using Atomic.Elements;
using AtomicFramework.Effects;
using AtomicFramework.EnemySystem;
using AtomicFramework.EnemyPointerSystem;
using AtomicFramework.ZombieShooter;
using ZombieShooter.SceneObjects;
using ZombieShooter.GameCycleSystem;

namespace Atomic.Contexts
{
	public static class GameContextAPI
	{
		///Keys
		public const int ContainersPresenter = 1; // ContainersPresenter
		public const int EnemySystemConfig = 6; // EnemySystemConfig
		public const int EnemyPositions = 7; // EnemyPositions
		public const int EnemyConfig = 8; // EnemyConfig
		public const int EnemyFactory = 9; // EnemyFactory
		public const int EnemyPool = 10; // EnemyPool
		public const int EnemyManager = 11; // EnemyManager
		public const int PointerIconPrefab = 12; // PointerIcon
		public const int GameCycle = 19; // GameCycle
		public const int GameStateChangeAction = 20; // IEvent<GameStates>


		///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ContainersPresenter GetContainersPresenter(this IContext obj) => obj.ResolveValue<ContainersPresenter>(ContainersPresenter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetContainersPresenter(this IContext obj, out ContainersPresenter value) => obj.TryResolveValue(ContainersPresenter, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddContainersPresenter(this IContext obj, ContainersPresenter value) => obj.AddValue(ContainersPresenter, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelContainersPresenter(this IContext obj) => obj.DelValue(ContainersPresenter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetContainersPresenter(this IContext obj, ContainersPresenter value) => obj.SetValue(ContainersPresenter, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasContainersPresenter(this IContext obj) => obj.HasValue(ContainersPresenter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EnemySystemConfig GetEnemySystemConfig(this IContext obj) => obj.ResolveValue<EnemySystemConfig>(EnemySystemConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEnemySystemConfig(this IContext obj, out EnemySystemConfig value) => obj.TryResolveValue(EnemySystemConfig, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddEnemySystemConfig(this IContext obj, EnemySystemConfig value) => obj.AddValue(EnemySystemConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEnemySystemConfig(this IContext obj) => obj.DelValue(EnemySystemConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEnemySystemConfig(this IContext obj, EnemySystemConfig value) => obj.SetValue(EnemySystemConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEnemySystemConfig(this IContext obj) => obj.HasValue(EnemySystemConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EnemyPositions GetEnemyPositions(this IContext obj) => obj.ResolveValue<EnemyPositions>(EnemyPositions);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEnemyPositions(this IContext obj, out EnemyPositions value) => obj.TryResolveValue(EnemyPositions, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddEnemyPositions(this IContext obj, EnemyPositions value) => obj.AddValue(EnemyPositions, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEnemyPositions(this IContext obj) => obj.DelValue(EnemyPositions);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEnemyPositions(this IContext obj, EnemyPositions value) => obj.SetValue(EnemyPositions, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEnemyPositions(this IContext obj) => obj.HasValue(EnemyPositions);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EnemyConfig GetEnemyConfig(this IContext obj) => obj.ResolveValue<EnemyConfig>(EnemyConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEnemyConfig(this IContext obj, out EnemyConfig value) => obj.TryResolveValue(EnemyConfig, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddEnemyConfig(this IContext obj, EnemyConfig value) => obj.AddValue(EnemyConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEnemyConfig(this IContext obj) => obj.DelValue(EnemyConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEnemyConfig(this IContext obj, EnemyConfig value) => obj.SetValue(EnemyConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEnemyConfig(this IContext obj) => obj.HasValue(EnemyConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EnemyFactory GetEnemyFactory(this IContext obj) => obj.ResolveValue<EnemyFactory>(EnemyFactory);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEnemyFactory(this IContext obj, out EnemyFactory value) => obj.TryResolveValue(EnemyFactory, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddEnemyFactory(this IContext obj, EnemyFactory value) => obj.AddValue(EnemyFactory, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEnemyFactory(this IContext obj) => obj.DelValue(EnemyFactory);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEnemyFactory(this IContext obj, EnemyFactory value) => obj.SetValue(EnemyFactory, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEnemyFactory(this IContext obj) => obj.HasValue(EnemyFactory);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EnemyPool GetEnemyPool(this IContext obj) => obj.ResolveValue<EnemyPool>(EnemyPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEnemyPool(this IContext obj, out EnemyPool value) => obj.TryResolveValue(EnemyPool, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddEnemyPool(this IContext obj, EnemyPool value) => obj.AddValue(EnemyPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEnemyPool(this IContext obj) => obj.DelValue(EnemyPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEnemyPool(this IContext obj, EnemyPool value) => obj.SetValue(EnemyPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEnemyPool(this IContext obj) => obj.HasValue(EnemyPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EnemyManager GetEnemyManager(this IContext obj) => obj.ResolveValue<EnemyManager>(EnemyManager);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEnemyManager(this IContext obj, out EnemyManager value) => obj.TryResolveValue(EnemyManager, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddEnemyManager(this IContext obj, EnemyManager value) => obj.AddValue(EnemyManager, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEnemyManager(this IContext obj) => obj.DelValue(EnemyManager);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEnemyManager(this IContext obj, EnemyManager value) => obj.SetValue(EnemyManager, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEnemyManager(this IContext obj) => obj.HasValue(EnemyManager);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PointerIcon GetPointerIconPrefab(this IContext obj) => obj.ResolveValue<PointerIcon>(PointerIconPrefab);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPointerIconPrefab(this IContext obj, out PointerIcon value) => obj.TryResolveValue(PointerIconPrefab, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddPointerIconPrefab(this IContext obj, PointerIcon value) => obj.AddValue(PointerIconPrefab, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPointerIconPrefab(this IContext obj) => obj.DelValue(PointerIconPrefab);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPointerIconPrefab(this IContext obj, PointerIcon value) => obj.SetValue(PointerIconPrefab, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPointerIconPrefab(this IContext obj) => obj.HasValue(PointerIconPrefab);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameCycle GetGameCycle(this IContext obj) => obj.ResolveValue<GameCycle>(GameCycle);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameCycle(this IContext obj, out GameCycle value) => obj.TryResolveValue(GameCycle, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddGameCycle(this IContext obj, GameCycle value) => obj.AddValue(GameCycle, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameCycle(this IContext obj) => obj.DelValue(GameCycle);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameCycle(this IContext obj, GameCycle value) => obj.SetValue(GameCycle, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameCycle(this IContext obj) => obj.HasValue(GameCycle);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<GameStates> GetGameStateChangeAction(this IContext obj) => obj.ResolveValue<IEvent<GameStates>>(GameStateChangeAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameStateChangeAction(this IContext obj, out IEvent<GameStates> value) => obj.TryResolveValue(GameStateChangeAction, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddGameStateChangeAction(this IContext obj, IEvent<GameStates> value) => obj.AddValue(GameStateChangeAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameStateChangeAction(this IContext obj) => obj.DelValue(GameStateChangeAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameStateChangeAction(this IContext obj, IEvent<GameStates> value) => obj.SetValue(GameStateChangeAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameStateChangeAction(this IContext obj) => obj.HasValue(GameStateChangeAction);
    }
}
