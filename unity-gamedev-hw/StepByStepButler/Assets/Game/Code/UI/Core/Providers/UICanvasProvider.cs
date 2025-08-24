using System;
using UnityEngine;

namespace UI.Core {
    
    public class UICanvasProvider : MonoBehaviour {

        [SerializeField] private Camera _uiCamera;

        public Camera UICamera => _uiCamera;
        public Canvas Canvas { get; private set; }

        private void Awake() {

            var canvas = gameObject.GetComponentInChildren<Canvas>();

            if (canvas == null)
                throw new ArgumentNullException($"PersistentUI: Canvas not found!");

            Canvas = canvas;
            Canvas.worldCamera = _uiCamera;

            DontDestroyOnLoad(gameObject);
        }

        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }
    }
}
