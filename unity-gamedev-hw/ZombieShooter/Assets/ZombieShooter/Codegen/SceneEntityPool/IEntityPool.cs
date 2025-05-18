using Atomic.Entities;

namespace AtomicFramework.EntityPool {
    public interface IEntityPool {
        public IEntity Rent();
        public void Return(IEntity entity);
    }
}
