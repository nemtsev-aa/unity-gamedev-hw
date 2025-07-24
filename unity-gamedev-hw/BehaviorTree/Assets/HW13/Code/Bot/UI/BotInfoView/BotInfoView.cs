using R3;
using TMPro;
using System;
using UnityEngine;
using DG.Tweening;
using FarmingSystem;
using BehaviorTree.Brain;
using System.Collections.Generic;

namespace BehaviorTree.Bot {

    public sealed class BotInfoView : MonoBehaviour, IDisposable {
        [Space, SerializeField] private bool _showDefault;
        [SerializeField] private ShowHideButton _showHideButton;
        [SerializeField] private float _animationDuration = 0.3f;

        [SerializeField] private TMP_Text _stateText;
        [SerializeField] private TMP_Text _priorityText;
        [SerializeField] private TMP_Text _moveTargetText;
        [SerializeField] private TMP_Text _distanceToTargetText;
        [SerializeField] private TMP_Text _partolPointText;
        [SerializeField] private TMP_Text _nearestResourceSourceText;
        [SerializeField] private TMP_Text _nearestResourceLootText;
        [SerializeField] private TMP_Text _cargoAmountText;

        private List<TMP_Text> _labels = new();
        
        private CompositeDisposable _disposables = new();
        private IBotInfoViewModel _viewModel;
        private RectTransform _rectTransform;
        private Vector2 _defaultSize;

        public void Init(IBotInfoViewModel viewModel) {
            _viewModel = viewModel;
            _rectTransform = gameObject.GetComponent<RectTransform>();

            CreateLabelList();
            ShowDefaultState();
            CreateReactiveSubscribes();
        }

        private void ShowDefaultState() {
            _showHideButton.Init(_showDefault);
            ShowLabels(_showDefault);
            
            _defaultSize = _rectTransform.sizeDelta;
            
            if (_showDefault == false)
                _rectTransform.sizeDelta = new Vector2(_defaultSize.x, 50f);
        }

        private void CreateLabelList() {
            _labels.Add(_priorityText);
            _labels.Add(_moveTargetText);
            _labels.Add(_distanceToTargetText);
            _labels.Add(_partolPointText);
            _labels.Add(_nearestResourceSourceText);
            _labels.Add(_nearestResourceLootText);
            _labels.Add(_cargoAmountText);
        }

        private void ShowLabels(bool status) {

            foreach (var item in _labels) {
                item.gameObject.SetActive(status);
            }
        }

        private void ShowAnimation() {

            if (_rectTransform.sizeDelta.y == _defaultSize.y) {
                ShowLabels(false);
                _rectTransform.DOSizeDelta(new Vector2(_defaultSize.x, 50f), _animationDuration);
                return;
            }

            ShowLabels(true);
            _rectTransform.DOSizeDelta(_defaultSize, _animationDuration);
        }

        private void CreateReactiveSubscribes() {

            _showHideButton.ActionButton
                .Subscribe(OnActionButtonClick)
                .AddTo(_disposables);

            _viewModel.CurrentBotState
                .Subscribe(BotStateChanged)
                .AddTo(_disposables);

            _viewModel.MoveTarget
                .Subscribe(MoveTargetChanged)
                .AddTo(_disposables);

            _viewModel.DistanceToTarget
                .Subscribe(DistanceToTargetChanged)
                .AddTo(_disposables);

            _viewModel.PartolPoint
                .Subscribe(PartolPointChanged)
                .AddTo(_disposables);

            _viewModel.NearestResourceSource
                .Subscribe(NearestResourceSourceChanged)
                .AddTo(_disposables);

            _viewModel.NearestResourceLoot
                .Subscribe(NearestResourceLootChanged)
                .AddTo(_disposables);

            _viewModel.MaxInventoryAmount
               .Subscribe(CargoMaxAmountChanged)
               .AddTo(_disposables);

            _viewModel.CurrentInventoryAmount
                .Subscribe(CargoCurrentAmountChanged)
                .AddTo(_disposables);

            _viewModel.Priority
                .Subscribe(PriorityChanged)
                .AddTo(_disposables);
        }

        private void OnActionButtonClick(Unit unit) {
            ShowAnimation();
        }

        private void BotStateChanged(BotStates state) {
            _stateText.text = $"<color=green>BotState:</color> {state}";
        }

        private void PriorityChanged(WorkOperationPriority priority) {
            _priorityText.text = $"<color=green>Priority:</color> {priority}";
        }

        private void MoveTargetChanged(Transform transform) {

            if (transform != null)
                _moveTargetText.text = $"<color=orange>MoveTarget:</color> {transform.gameObject.name}";
            else
                _moveTargetText.text = $"MoveTarget: - ";
        }

        private void DistanceToTargetChanged(float distance) {

            if (distance > 0)
                _distanceToTargetText.text = $"<color=orange>DistanceToTarget:</color> {distance.ToString("0.00")}";
            else
                _distanceToTargetText.text = $"DistanceToTarget: 0";
        }

        private void PartolPointChanged(Transform transform) {

            if (transform != null)
                _partolPointText.text = $"<color=orange>PartolPoint:</color> {transform.gameObject.name}";
            else
                _partolPointText.text = $"PartolPoint: - ";
        }

        private void NearestResourceSourceChanged(ResourceSpot source) {

            if (source != null)
                _nearestResourceSourceText.text = $"<color=orange>ResourceSpot:</color> {source.gameObject.name}";
            else
                _nearestResourceSourceText.text = $"ResourceSpot: - ";
        }

        private void NearestResourceLootChanged(ResourceLoot loot) {

            if (loot != null)
                _nearestResourceLootText.text = $"<color=orange>ResourceLoot:</color> {loot.gameObject.name}";
            else
                _nearestResourceLootText.text = $"ResourceLoot: - ";
        }

        private void CargoMaxAmountChanged(int maxAmount) {

            var currentAmount = _viewModel.CurrentInventoryAmount.CurrentValue;

            if (maxAmount > 0)
                _cargoAmountText.text = $"<color=orange>LootAmount:</color> {currentAmount}/{maxAmount}";
            else
                _cargoAmountText.text = $"LootAmount: -";
        }

        private void CargoCurrentAmountChanged(int amount) {

            var maxAmount = _viewModel.MaxInventoryAmount.CurrentValue;

            if (amount > 0 && maxAmount > 0)
                _cargoAmountText.text = $"<color=orange>LootAmount:</color> {amount}/{maxAmount}";
            else
                _cargoAmountText.text = $"LootAmount: 0/{maxAmount}";
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}



