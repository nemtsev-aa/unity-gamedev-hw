using System;
using System.Collections.Generic;
using UnityEngine;

namespace CursorChangeService {

    [Serializable]
    public sealed class CursorChangeView {
        private ICursorChangeViewModel _viewModel;

        private List<CursorData> _cursorDataList => _viewModel.CursorDataList;
        private CursorData _defaultCursor => _viewModel.DefaultCursor;

        private Dictionary<CursorType, CursorData> _cursorDictionary;
        private CursorType _currentCursorType = CursorType.Default;

        public void Init(ICursorChangeViewModel viewModel) {
            _viewModel = viewModel;

            InitializeCursorDictionary();
            SetCursor(_currentCursorType);
        }

        private void InitializeCursorDictionary() {
            _cursorDictionary = new Dictionary<CursorType, CursorData>();

            foreach (var data in _cursorDataList) {
                _cursorDictionary[data.Type] = data;
            }

            if (_cursorDictionary.ContainsKey(CursorType.Default) == true)
                _cursorDictionary[CursorType.Default] = _defaultCursor;
        }

        public void SetCursor(CursorType cursorType) {

            if (_cursorDictionary.TryGetValue(cursorType, out CursorData data) == false) {
                Debug.LogWarning($"Cursor type {cursorType} not found in the dictionary");
                return;
            }

            Cursor.SetCursor(data.Texture, data.Hotspot, CursorMode.Auto);
            _currentCursorType = cursorType;
        }
    }
}

