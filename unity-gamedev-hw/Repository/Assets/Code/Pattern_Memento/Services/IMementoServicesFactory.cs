namespace Pattern_Memento {
    public interface IMementoServicesFactory {
        MementoServices Create(MementoTypes type);
    }
}

