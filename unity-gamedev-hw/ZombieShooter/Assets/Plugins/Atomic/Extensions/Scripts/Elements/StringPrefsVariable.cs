using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Extensions
{
    public sealed class StringPrefsVariable : IReactiveVariable<string>
    {
        public event Action<string> OnValueChanged;

        [ShowInInspector]
        public string Value
        {
            get { return PlayerPrefs.GetString(this.key, this.defaultValue); }
            set
            {
                if (this.Value != value)
                {
                    PlayerPrefs.SetString(this.key, value);
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        private readonly string key;
        private readonly string defaultValue;

        public StringPrefsVariable(string key, string defaultValue = null)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        public Action<string> Subscribe(Action<string> listener)
        {
            this.OnValueChanged += listener;
            return listener;
        }

        public void Unsubscribe(Action<string> listener)
        {
            this.OnValueChanged -= listener;
        }
    }
}