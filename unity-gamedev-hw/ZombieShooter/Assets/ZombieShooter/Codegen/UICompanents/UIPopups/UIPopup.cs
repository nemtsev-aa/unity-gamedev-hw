using System;
using UnityEngine;

namespace ZombieShooter.UI {
    public abstract class UIPopup : MonoBehaviour, IDisposable {
        public abstract void Show(bool status);
        
        public virtual void Dispose() {

        }
    }
}

