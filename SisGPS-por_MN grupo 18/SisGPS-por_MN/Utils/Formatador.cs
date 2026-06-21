using SisGPS_por_MN.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SisGPS_por_MN.Utils
{
    public static class Formatador
    {
        public static string FormatarHoras(decimal horas) =>
            $"{horas:0.0} h";

        public static string FormatarEstado(EstadoTarefa estado) => estado switch
        {
            EstadoTarefa.Backlog => "Backlog",
            EstadoTarefa.EmProgresso => "Em Progresso",
            EstadoTarefa.Concluida => "Concluída",
            EstadoTarefa.Bloqueada => "Bloqueada",
            _ => estado.ToString()
        };

        public static string FormatarPapel(PapelMembro papel) => papel switch
        {
            PapelMembro.Developer => "Developer",
            PapelMembro.QA => "QA",
            PapelMembro.ProjectManager => "Project Manager",
            PapelMembro.Designer => "Designer",
            PapelMembro.DevOps => "DevOps",
            _ => papel.ToString()
        };

        public static Color CorPorPrioridade(Prioridade prioridade) => prioridade switch
        {
            Prioridade.Baixa => Color.LightGreen,
            Prioridade.Media => Color.LightYellow,
            Prioridade.Alta => Color.Orange,
            Prioridade.Critica => Color.OrangeRed,
            _ => Color.White
        };

        public static Color CorPorEstado(EstadoTarefa estado) => estado switch
        {
            EstadoTarefa.Backlog => Color.LightGray,
            EstadoTarefa.EmProgresso => Color.LightBlue,
            EstadoTarefa.Concluida => Color.LightGreen,
            EstadoTarefa.Bloqueada => Color.LightCoral,
            _ => Color.White
        };
    }
}
