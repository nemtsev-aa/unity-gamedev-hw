using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace StepByStepButler.Gameplay.Systems {
    public static class ParticleSystemExtensions {

        public static async UniTask WaitForCompletionAsync(this ParticleSystem particleSystem,
                                                           CancellationToken cancellationToken = default) {
            
            if (particleSystem == null || particleSystem.gameObject.activeInHierarchy == false)
                return;

            if (particleSystem.isPlaying == false)
                particleSystem.Play();

            while (cancellationToken.IsCancellationRequested == false &&
                  (particleSystem.IsAlive(withChildren: true) || particleSystem.isPlaying == true)) {

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }
        }
    }
}