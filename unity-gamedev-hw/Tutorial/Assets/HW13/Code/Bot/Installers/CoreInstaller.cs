using System;
using Zenject;
using UnityEngine;
using BehaviorTree.Brain;
using BehaviorTree.PlayerCoreSubsystem;

namespace BehaviorTree.Bot {

    [Serializable]
    public sealed class CoreInstaller {
        
        [Space, SerializeField] private Player _player;
        [SerializeField] private BotModel _bot;

        public void Install(DiContainer container, BotBrainData brainData) {

            container.BindInstance(_player)
                .AsSingle()
                .NonLazy();

            _bot.Init(brainData, _player);

            container.BindInstance(_bot)
                .AsSingle()
                .NonLazy();
        }
    }
}



