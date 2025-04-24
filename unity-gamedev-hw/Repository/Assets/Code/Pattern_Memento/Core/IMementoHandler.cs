namespace Pattern_Memento {

    public interface IMementoHandler {
        public IMementos SaveState(string id = "");
        public void RestoreState(IMementos memento);
    }
}

