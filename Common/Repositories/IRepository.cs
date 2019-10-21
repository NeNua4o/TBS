using System.Collections.Generic;

namespace Common.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetItems();
        void LoadItems();
        void SaveItems();
        T CreateItem();
        void RemoveItem(T item);
    }
}
