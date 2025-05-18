/**
* Code generation. Don't modify! 
**/

using AtomicFramework.View.SFX;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Entities {
    public static class SFXAPI {
        ///Keys
        public const int AudioSource = 33; // AudioSource
        public const int SFXCollection = 34; // List<SFXData>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AudioSource GetAudioSource(this IEntity obj) => obj.GetValue<AudioSource>(AudioSource);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAudioSource(this IEntity obj, out AudioSource value) => obj.TryGetValue(AudioSource, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAudioSource(this IEntity obj, AudioSource value) => obj.AddValue(AudioSource, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAudioSource(this IEntity obj) => obj.HasValue(AudioSource);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAudioSource(this IEntity obj) => obj.DelValue(AudioSource);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAudioSource(this IEntity obj, AudioSource value) => obj.SetValue(AudioSource, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<SFXData> GetSFXCollection(this IEntity obj) => obj.GetValue<List<SFXData>>(SFXCollection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSFXCollection(this IEntity obj, out List<SFXData> value) => obj.TryGetValue(SFXCollection, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddSFXCollection(this IEntity obj, List<SFXData> value) => obj.AddValue(SFXCollection, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasSFXCollection(this IEntity obj) => obj.HasValue(SFXCollection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelSFXCollection(this IEntity obj) => obj.DelValue(SFXCollection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSFXCollection(this IEntity obj, List<SFXData> value) => obj.SetValue(SFXCollection, value);
    }
}
