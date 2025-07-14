using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SessionTrackerSystem {

    [Serializable]
    public sealed class SessionDataWrapper {

        [JsonProperty]
        public List<SessionData> Sessions { get; set; }
    }
}

