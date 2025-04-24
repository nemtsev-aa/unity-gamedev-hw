using System.Collections.Generic;

namespace SaveLoadSystem {
    public interface IGameStateSaver {
        bool SaveData(Dictionary<string, string> data);
        Dictionary<string, string> LoadData();
    }
}