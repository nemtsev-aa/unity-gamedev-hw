using System.Collections.Generic;

namespace Atomic.Elements
{
    public interface IReactiveArray<T> : IEnumerable<T>
    {
        event ChangeItemHandler<T> OnItemChanged;
        event StateChangedHandler OnStateChanged;

        int Length { get; }

        T this[int index] { get; set; }
    }
}