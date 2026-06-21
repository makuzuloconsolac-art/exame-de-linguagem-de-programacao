using System;
using System.Collections.Generic;
using System.Text;

namespace SisGPS_por_MN.Exceptions
{
    public class SprintFechadoException:Exception
    {
        public SprintFechadoException()
            : base("Não é possível adicionar tarefas a um sprint já encerrado.") { }

        public SprintFechadoException(string mensagem)
            : base(mensagem) { }
    }
}
