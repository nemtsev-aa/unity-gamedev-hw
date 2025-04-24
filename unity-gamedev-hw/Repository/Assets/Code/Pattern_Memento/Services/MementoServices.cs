namespace Pattern_Memento {

    public sealed class MementoServices {
        public MementoServices(
            MementoTypes type,
            MementoHistory history,
            MementoHistoryController controller,
            MomemtosPopup momemtosPopup,
            IMementoHandler manager) {

            Type = type;
            History = history;
            Controller = controller;
            Popup = momemtosPopup;
            Manager = manager;
        }

        public MementoTypes Type { get; private set; }
        public MementoHistory History { get; private set; }
        public MementoHistoryController Controller { get; }
        public IMementoHandler Manager { get; private set; }
        public MomemtosPopup Popup { get; private set; }
        
    }
}

