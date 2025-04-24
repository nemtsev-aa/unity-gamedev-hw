namespace SaveLoadSystem {

    namespace Core {

        public interface ISaveLoader {
            void SaveGame(IContext context, IGameRepository gameRepository);
            void LoadGame(IContext context, IGameRepository gameRepository);
        }
    }
}

