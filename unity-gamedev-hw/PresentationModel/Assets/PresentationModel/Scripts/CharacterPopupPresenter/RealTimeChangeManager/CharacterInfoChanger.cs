using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace PresentationModel {
    public sealed class CharacterInfoChanger : MonoBehaviour, ICharacterInfoChanger {
        [Range(0, 6)]
        [SerializeField] private int _statIndex;
        [Range(1, 100)]
        [SerializeField] private int _statValue;

        private CharacterInfoView _characterInfoView;
        private CharacterInfo _characterInfo;

        public void Init(ICharacterPopupViewModel viewModel, UIView uIView) {

            if (uIView is not CharacterInfoView characterInfoView)
                throw new ArgumentNullException($"Invalid UIView: {uIView}");

            _characterInfoView = (CharacterInfoView)uIView;
            _characterInfo = viewModel.CharacterInfoViewModel.CharacterInfo;
        }

        [Button]
        public void SetNewValueInCharacterStat() {
            if (_characterInfo.TryGetStatByIndex(_statIndex, out CharacterStat stat) == true) {
                stat.Value.OnNext(_statValue);

                Debug.Log($"Current Stat: Name [{stat.Name}], Value [{stat.Value.Value}]");
            }

            Debug.Log($"StatIndex [{_statIndex}] out of CharacterStat range!");
        }

        [Button]
        public void AddNewStat() {
            var stat = new CharacterStat("Health", 100);
            _characterInfo.AddStat(stat);
        }
    }
}
