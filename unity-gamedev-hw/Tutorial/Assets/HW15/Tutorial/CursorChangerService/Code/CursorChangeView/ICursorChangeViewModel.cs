using System.Collections.Generic;

namespace CursorChangeService {
    public interface ICursorChangeViewModel {
        public List<CursorData> CursorDataList { get; }
        public CursorData DefaultCursor { get; }
    }
}

