namespace SaveLoadSystem {

    namespace Core {

        public abstract class SaveLoader<TService, TData> : ISaveLoader {
            protected abstract TData ConvertToData(TService service);
            protected abstract void SetupData(TService service, TData data);
            protected virtual void SetupDefaultData(TService service) { }

            public void SaveGame(IContext context, IGameRepository gameRepository) {
                TService service = context.GetService<TService>();
                TData data = ConvertToData(service);

                gameRepository.SetData(data);
            }

            public void LoadGame(IContext context, IGameRepository gameRepository) {
                TService service = context.GetService<TService>();

                if (gameRepository.TryGetData(out TData data)) {
                    SetupData(service, data);
                    return;
                }

                SetupDefaultData(service);
            }
        }
    }
}


