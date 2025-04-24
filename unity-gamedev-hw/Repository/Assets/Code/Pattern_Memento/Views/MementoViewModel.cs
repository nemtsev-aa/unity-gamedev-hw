
namespace Pattern_Memento {

    public sealed class MementoViewModel : IMementoViewModel {
        public MementoViewModel(string name) {
            ID = name;
        }

        public string ID { get; }
    }
}

