using System;
using System.Collections.Generic;
using System.Text;

namespace SisGPS_por_MN.Exceptions
{
    public class ProjectoEncerradoException: Exception
    {
        public ProjectoEncerradoException()
            : base("Não é possível criar um sprint num projecto Concluído ou Cancelado.") { }

        public ProjectoEncerradoException(string mensagem)
            : base(mensagem) { }
    }
}
