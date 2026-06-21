using System;
using System.Collections.Generic;
using System.Text;

namespace SisGPS_por_MN.Exceptions
{
    public class MembroNaoDisponivelException: Exception
    {
        public string NomeMembro { get; }

        public MembroNaoDisponivelException(string nomeMembro)
            : base($"O membro '{nomeMembro}' não está disponível para receber tarefas.")
        {
            NomeMembro = nomeMembro;
        }

        public MembroNaoDisponivelException(string nomeMembro, string mensagem)
            : base(mensagem)
        {
            NomeMembro = nomeMembro;
        }
    }
}
