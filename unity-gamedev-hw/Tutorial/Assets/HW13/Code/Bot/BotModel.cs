using MBT;
using System;
using UnityEngine;
using BehaviorTree.Brain;
using BehaviorTree.PlayerCoreSubsystem;

namespace BehaviorTree.Bot {

    public sealed class BotModel : MonoBehaviour {
        private const string BRAIN_DATA_KEY = "BrainData";
        private const string PLAYER_KEY = "Player";

        public BotBrainData BrainData { get; private set; }
        public Player Player { get; private set; }

        public void Init(BotBrainData brainData, Player player) {
            BrainData = brainData;
            Player = player;
            Player.Init();
        }

        private void Start() {
            SetVariablesFromBlackboard();
        }

        private void SetVariablesFromBlackboard() {
            var blackboard = GetComponentInChildren<Blackboard>();

            if (blackboard == null)
                throw new ArgumentException($"Companent Blackboard not found!");

            var botBrainDataVariable = blackboard.GetVariable<BotBrainDataVariable>(BRAIN_DATA_KEY);
            botBrainDataVariable.Value = BrainData;

            Debug.Log($"Set Variable [BrainData] in Blackboard succes!");

            var playerVariable = blackboard.GetVariable<PlayerVariable>(PLAYER_KEY);
            playerVariable.Value = Player;

            Debug.Log($"Set Variable [Player] in Blackboard succes!");
        }
    }
}



