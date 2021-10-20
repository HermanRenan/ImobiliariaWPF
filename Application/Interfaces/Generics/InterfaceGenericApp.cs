using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Generic
{
    public interface InterfaceGenericApp<T> where T : class
    {
        Task Add(T Object);
        Task Update(T Object);
        Task Delete(T Object);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List();
        List<T> ListaLL();
    }
}
