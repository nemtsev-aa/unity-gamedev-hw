using UnityEngine;

namespace UI {

    public sealed class UIService : MonoBehaviour {
        [SerializeField] private HeroListView _bluePlayer;
        [SerializeField] private HeroListView _redPlayer;

        public HeroListView GetBluePlayer() { 
            return _bluePlayer;
        }

        public HeroListView GetRedPlayer() {
            return _redPlayer;
        }
    }
}