using System;
using Zenject;
using UnityEngine;
using BehaviorTree.Brain;
using BehaviorTree.Bot.UI;

namespace BehaviorTree.Bot {

    [Serializable]
    public sealed class UIInstaller {
        [SerializeField] private BotStateViewConfigs _config;
        [Space, SerializeField] private RectTransform _guiRoot;
        [SerializeField] private RectTransform _playerRoot;
        [Space, SerializeField] private BotStateView _botStateViewPrefab;
        [SerializeField] private BotInfoView _botInfoViewPrefab;

        public void Install(DiContainer container, BotBrainData brainData) {
            CreateBotStateView(brainData);
            CreateBotInfoView(brainData);
        }

        private void CreateBotStateView(BotBrainData data) {

            var stateViewModel = new BotStateViewModel(data.BotState, _config);
            var view = GameObject.Instantiate(_botStateViewPrefab, _playerRoot);
            view.Init(stateViewModel);
        }

        private void CreateBotInfoView(BotBrainData data) {

            var infoViewModel = new BotInfoViewModel(data);
            var infoView = GameObject.Instantiate(_botInfoViewPrefab, _guiRoot);
            infoView.Init(infoViewModel);
        }
    } 
}



