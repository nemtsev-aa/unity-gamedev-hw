using System.Collections.Generic;

namespace CursorChangeService {
    public sealed class CursorChangeViewModel : ICursorChangeViewModel {
        public List<CursorData> CursorDataList => _config.CursorDataList;
        public CursorData DefaultCursor => _config.DefaultCursor;

        private readonly CursorChangeServiceConfig _config;

        public CursorChangeViewModel(CursorChangeServiceConfig config) {
            _config = config;
        }
    }
}

