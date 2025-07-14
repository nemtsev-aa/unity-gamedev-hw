using System;
using Newtonsoft.Json;

namespace SessionTrackerSystem {

    [Serializable]
    public sealed class SessionData {
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime LoginTime { get; set; }

        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime LogoutTime { get; set; }

        [JsonConverter(typeof(JsonTimeSpanConverter))]
        public TimeSpan Duration { get; set; }


        [JsonConstructor]
        public SessionData(DateTime loginTime, DateTime logoutTime, TimeSpan duration) {
            LoginTime = loginTime;
            LogoutTime = logoutTime;
            Duration = duration;
        }

        public void SetLoginTime(DateTime dataTime) {
            LoginTime = dataTime;
        }

        public void SetLogoutTime(DateTime dataTime) {
            LogoutTime = dataTime;
        }

        public void SetDuration(TimeSpan duration) {
            Duration = duration;
        }
    }
}

