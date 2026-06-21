using System;
using System.Collections.Generic;
using System.Text;

namespace SisGPS_por_MN.Interfaces
{
    public interface IRepositorio<T>
    {
        void Inserir(T entidade);
        T? ObterPorId(int id);
        IEnumerable<T> ObterTodos();
        void Actualizar(T entidade);
        void Eliminar(int id);
    }
}
