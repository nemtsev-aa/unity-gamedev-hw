using System;
using System.Collections.Generic;

namespace StepByStepButler.Gameplay.Heroes {

    public sealed class Entity : IDisposable {
        public event Action<Entity, IComponent> CompanentAdded;
        public event Action<Entity, IComponent> CompanentRemoved;

        public int Id { get; }

        private readonly Dictionary<Type, IComponent> _components = new();

        public Entity(int id) {
            Id = id;
        }

        public void AddComponent<T>(T component) where T : IComponent {
            _components[typeof(T)] = component;

            CompanentAdded?.Invoke(this, component);
        }

        public T GetComponent<T>() where T : IComponent {
            return _components.ContainsKey(typeof(T)) ? (T)_components[typeof(T)] : default;
        }

        public void UpdateComponent<T>(T component) where T : IComponent {
            _components[typeof(T)] = component;
        }

        public bool HasComponent<T>() where T : IComponent {
            return _components.ContainsKey(typeof(T));
        }

        public void RemoveComponent<T>() where T : IComponent {

            if (_components.TryGetValue(typeof(T), out var component) == true)
                CompanentRemoved?.Invoke(this, component);

            _components.Remove(typeof(T));
        }

        public void Dispose() {
            _components.Clear();
        }
    }
}