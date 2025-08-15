namespace AssetManagementSystem {

    public interface ISceneLoader {
        public void LoadGame();
        public void UnloadGame();
        public void LoadMenu();
        void CancelLoading();
    }
}
