using UnityEngine;

namespace CursorChangeService {

    public class NPC_CursorChanger : MonoBehaviour, ICursorChanger {
        [SerializeField] private string npcName = "NPC";
        [SerializeField] private bool isHostile = false;

        public CursorType GetCursorType() {
            return isHostile ? CursorType.Attack : CursorType.Talk;
        }

        public string GetCursorTooltip() {
            return isHostile ? $"Атаковать {npcName}" : $"Поговорить с {npcName}";
        }
    }
}

