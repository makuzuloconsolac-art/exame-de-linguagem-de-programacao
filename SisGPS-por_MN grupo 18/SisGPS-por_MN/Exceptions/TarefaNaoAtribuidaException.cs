using System;
using System.Collections.Generic;
using System.Text;

namespace SisGPS_por_MN.Exceptions
{
    public class TarefaNaoAtribuidaException:Exception
    {
        public int TarefaId { get; }

        public TarefaNaoAtribuidaException(int tarefaId)
            : base($"A tarefa {tarefaId} não tem membro atribuído e não pode mudar de estado.")
        {
            TarefaId = tarefaId;
        }

        public TarefaNaoAtribuidaException(int tarefaId, string mensagem)
            : base(mensagem)
        {
            TarefaId = tarefaId;
        }
    }
}
