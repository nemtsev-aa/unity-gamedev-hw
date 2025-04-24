
namespace SaveLoadSystem {

    namespace Core {

        public interface IContext {
            TService GetService<TService>();
        }
    }
}

