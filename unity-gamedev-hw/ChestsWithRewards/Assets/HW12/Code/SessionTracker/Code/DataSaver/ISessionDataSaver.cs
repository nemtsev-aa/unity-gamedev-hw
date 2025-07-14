using System.Collections.Generic;

namespace SessionTrackerSystem {

    public interface ISessionDataSaver {
        bool TryLoadSessionData(out List<SessionData> data);
        bool TrySaveSessionData(SessionData newData);
        bool TryClearSessionData();
    }
}


