using R3;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UI.Components {
    
    public sealed class OperationButton : MonoBehaviour {
        public OperationButtonStates CurrentState { get; private set; }
        public Observable<Unit> OperationButtonClick => _operationButton.OnClickAsObservable();

        [SerializeField] private Button _operationButton;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TMP_Text _labelText;

        private Dictionary<OperationButtonStates, OperationButtonParameters> States = new() {
            { OperationButtonStates.InActive, 
              new OperationButtonParameters { 
                  ActiveStatus = false,
                  Text = "",
                  BackgroundColor = Color.white} },
            { OperationButtonStates.Play,
              new OperationButtonParameters {
                  ActiveStatus = true,
                  Text = "Play",
                  BackgroundColor = Color.green} },
            { OperationButtonStates.Cancel,
               new OperationButtonParameters {
                  ActiveStatus = true,
                  Text = "Cancel",
                  BackgroundColor = Color.red} }
        };

        public void SetOperationButtonState(OperationButtonStates state) {

            if (States.TryGetValue(state, out var currentParameters) == false)
                throw new ArgumentException($"Invalid OperationButtonState: {state}");

            CurrentState = state;

            _operationButton.gameObject.SetActive(currentParameters.ActiveStatus);
            _labelText.text = currentParameters.Text;
            _backgroundImage.color = currentParameters.BackgroundColor;
        }
    }

    public enum OperationButtonStates {
        InActive = 0,
        Play = 1,
        Cancel = 2
    }

    public struct OperationButtonParameters {
        public bool ActiveStatus;
        public string Text;
        public Color BackgroundColor;
    }
}