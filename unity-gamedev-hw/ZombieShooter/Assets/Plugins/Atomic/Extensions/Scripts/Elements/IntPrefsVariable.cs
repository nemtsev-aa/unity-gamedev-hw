using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Extensions
{
    public sealed class IntPrefsVariable : IReactiveVariable<int>
    {
        public event Action<int> OnValueChanged;

        [ShowInInspector]
        public int Value
        {
            get
            {
                return PlayerPrefs.GetInt(this.key, this.defaultValue);
            }
            set
            {
                if (this.Value != value)
                {
                    PlayerPrefs.SetInt(this.key, value);
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        private readonly string key;
        private readonly int defaultValue;

        public IntPrefsVariable(string key, int defaultValue = default)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        public Action<int> Subscribe(Action<int> listener)
        {
            this.OnValueChanged += listener;
            return listener;
        }

        public void Unsubscribe(Action<int> listener)
        {
            this.OnValueChanged -= listener;
        }
    }
}