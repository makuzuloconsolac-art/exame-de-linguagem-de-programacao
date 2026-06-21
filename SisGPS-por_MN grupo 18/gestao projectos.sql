-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 19-Jun-2026 às 11:30
-- Versão do servidor: 10.4.32-MariaDB
-- versão do PHP: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `gestao_projectos`
--

DELIMITER $$
--
-- Procedimentos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_alterar_estado_tarefa` (IN `p_tarefa_id` INT, IN `p_novo_estado` TINYINT, IN `p_observacao` VARCHAR(500))   BEGIN
    DECLARE v_estado_anterior TINYINT;

    SELECT estado INTO v_estado_anterior
    FROM tarefas WHERE id = p_tarefa_id;

    -- Actualizar estado
    UPDATE tarefas SET estado = p_novo_estado WHERE id = p_tarefa_id;

    -- Registar histórico
    INSERT INTO historico_tarefas (tarefa_id, estado_anterior, estado_novo, observacao)
    VALUES (p_tarefa_id, v_estado_anterior, p_novo_estado, p_observacao);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_encerrar_sprint` (IN `p_sprint_id` INT, OUT `p_pendentes` INT)   BEGIN
    -- Verificar se já está encerrado
    IF (SELECT encerrado FROM sprints WHERE id = p_sprint_id) = 1 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Sprint já está encerrado.';
    END IF;

    -- Contar pendentes (Backlog + EmProgresso + Bloqueada)
    SELECT COUNT(*) INTO p_pendentes
    FROM tarefas
    WHERE sprint_id = p_sprint_id AND estado != 2;

    -- Encerrar
    UPDATE sprints SET encerrado = 1 WHERE id = p_sprint_id;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `equipas`
--

CREATE TABLE `equipas` (
  `id` int(11) NOT NULL,
  `nome` varchar(100) NOT NULL,
  `descricao` varchar(255) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Equipas de desenvolvimento';

--
-- Extraindo dados da tabela `equipas`
--

INSERT INTO `equipas` (`id`, `nome`, `descricao`, `created_at`) VALUES
(1, 'Equipa Alpha', 'Equipa principal de desenvolvimento', '2026-06-08 20:53:16'),
(2, 'Equipa Beta', 'Equipa de testes e garantia de qualidade', '2026-06-08 20:53:16'),
(3, 'Equipa Gamma', 'Equipa de design e UX', '2026-06-08 20:53:16'),
(4, 'Isoft Tecnol', NULL, '2026-06-14 22:33:29');

-- --------------------------------------------------------

--
-- Estrutura da tabela `historico_tarefas`
--

CREATE TABLE `historico_tarefas` (
  `id` int(11) NOT NULL,
  `tarefa_id` int(11) NOT NULL,
  `estado_anterior` tinyint(4) NOT NULL COMMENT '0=Backlog,1=EmProg,2=Concluida,3=Bloqueada',
  `estado_novo` tinyint(4) NOT NULL COMMENT '0=Backlog,1=EmProg,2=Concluida,3=Bloqueada',
  `data_mudanca` datetime NOT NULL DEFAULT current_timestamp(),
  `observacao` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Histórico de mudanças de estado das tarefas';

--
-- Extraindo dados da tabela `historico_tarefas`
--

INSERT INTO `historico_tarefas` (`id`, `tarefa_id`, `estado_anterior`, `estado_novo`, `data_mudanca`, `observacao`) VALUES
(1, 1, 0, 1, '2026-06-08 20:53:17', 'Início do desenvolvimento'),
(2, 1, 1, 2, '2026-06-08 20:53:17', 'Script SQL validado e executado'),
(3, 2, 0, 1, '2026-06-08 20:53:17', 'Início do desenvolvimento do form de login'),
(4, 2, 1, 2, '2026-06-08 20:53:17', 'Form testado e aprovado pelo QA'),
(5, 3, 0, 1, '2026-06-08 20:53:17', 'Design iniciado'),
(6, 3, 1, 2, '2026-06-08 20:53:17', 'Design aprovado pelo cliente'),
(7, 4, 0, 1, '2026-06-08 20:53:17', 'Início dos testes'),
(8, 4, 1, 2, '2026-06-08 20:53:17', 'Todos os testes passaram'),
(9, 5, 0, 1, '2026-06-08 20:53:17', 'Início da implementação'),
(10, 5, 1, 3, '2026-06-08 20:53:17', 'Bloqueado: dependência de biblioteca externa'),
(11, 5, 3, 1, '2026-06-08 20:53:17', 'Desbloqueado após instalação do NuGet'),
(12, 5, 1, 2, '2026-06-08 20:53:17', 'Implementação concluída e testada');

-- --------------------------------------------------------

--
-- Estrutura da tabela `membros`
--

CREATE TABLE `membros` (
  `id` int(11) NOT NULL,
  `nome` varchar(100) NOT NULL,
  `email` varchar(100) DEFAULT NULL,
  `telefone` varchar(20) DEFAULT NULL,
  `papel` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=Developer, 1=QA, 2=ProjectManager, 3=Designer, 4=DevOps',
  `equipa_id` int(11) NOT NULL,
  `disponivel` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Membros das equipas';

--
-- Extraindo dados da tabela `membros`
--

INSERT INTO `membros` (`id`, `nome`, `email`, `telefone`, `papel`, `equipa_id`, `disponivel`, `created_at`) VALUES
(1, 'Ana Lopes', 'ana@empresa.ao', NULL, 2, 1, 1, '2026-06-08 20:53:17'),
(2, 'Bruno Ndozi', 'bruno@empresa.ao', NULL, 0, 1, 1, '2026-06-08 20:53:17'),
(3, 'Clara Pinto', 'clara@empresa.ao', NULL, 0, 1, 1, '2026-06-08 20:53:17'),
(4, 'David Mbemba', 'david@empresa.ao', NULL, 1, 2, 0, '2026-06-08 20:53:17'),
(5, 'Eva Kassoma', 'eva@empresa.ao', NULL, 3, 3, 1, '2026-06-08 20:53:17'),
(6, 'Filipe Luvualu', 'filipe@empresa.ao', NULL, 4, 1, 0, '2026-06-08 20:53:17'),
(7, 'Graça Teca', 'graca@empresa.ao', NULL, 0, 1, 1, '2026-06-08 20:53:17');

-- --------------------------------------------------------

--
-- Estrutura da tabela `projectos`
--

CREATE TABLE `projectos` (
  `id` int(11) NOT NULL,
  `nome` varchar(150) NOT NULL,
  `descricao` text DEFAULT NULL,
  `data_inicio` date NOT NULL,
  `data_fim` date DEFAULT NULL,
  `orcamento` decimal(12,2) DEFAULT NULL,
  `estado` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=Planeamento, 1=EmDesenvolvimento, 2=Pausado, 3=Concluido, 4=Cancelado',
  `cliente_nome` varchar(100) DEFAULT NULL,
  `equipa_id` int(11) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ;

--
-- Extraindo dados da tabela `projectos`
--

INSERT INTO `projectos` (`id`, `nome`, `descricao`, `data_inicio`, `data_fim`, `orcamento`, `estado`, `cliente_nome`, `equipa_id`, `created_at`, `updated_at`) VALUES
(1, 'Portal IPUKV', 'Portal académico da Universidade Kimpa Vita', '2025-01-10', '2025-06-30', 50000.00, 1, 'IPUKV', 1, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(2, 'App Mobile Cooperativa', 'Aplicação de gestão de cooperativa agrícola', '2025-02-01', '2025-08-31', 35000.00, 3, 'Cooperativa Agrícola do Uíge', 1, '2026-06-08 20:53:17', '2026-06-09 06:18:00'),
(3, 'Sistema RH Empresa XYZ', 'Sistema de gestão de recursos humanos', '2025-03-01', '2025-12-31', 80000.00, 0, 'Empresa XYZ', 2, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(4, 'Ia', 'ia curriculo', '2026-06-09', '2026-06-25', 255555.00, 0, 'Afonso', 1, '2026-06-09 06:17:40', '2026-06-09 06:17:40');

-- --------------------------------------------------------

--
-- Estrutura da tabela `sprints`
--

CREATE TABLE `sprints` (
  `id` int(11) NOT NULL,
  `nome` varchar(100) NOT NULL,
  `objectivo` varchar(500) DEFAULT NULL,
  `data_inicio` date NOT NULL,
  `data_fim` date NOT NULL,
  `encerrado` tinyint(1) NOT NULL DEFAULT 0,
  `projecto_id` int(11) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ;

--
-- Extraindo dados da tabela `sprints`
--

INSERT INTO `sprints` (`id`, `nome`, `objectivo`, `data_inicio`, `data_fim`, `encerrado`, `projecto_id`, `created_at`) VALUES
(1, 'Sprint 1 — Auth', 'Módulo de autenticação e dashboard', '2025-01-15', '2025-01-29', 1, 1, '2026-06-08 20:53:17'),
(2, 'Sprint 2 — Users', 'Módulo de gestão de utilizadores', '2025-02-01', '2025-02-14', 0, 1, '2026-06-08 20:53:17'),
(3, 'Sprint 3 — Reports', 'Módulo de relatórios e exportação', '2025-02-17', '2025-03-03', 0, 1, '2026-06-08 20:53:17'),
(4, 'Sprint 1 — Core', 'Módulo base da app mobile', '2025-02-05', '2025-02-20', 0, 2, '2026-06-08 20:53:17');

-- --------------------------------------------------------

--
-- Estrutura da tabela `tarefas`
--

CREATE TABLE `tarefas` (
  `id` int(11) NOT NULL,
  `titulo` varchar(200) NOT NULL,
  `descricao` text DEFAULT NULL,
  `estado` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=Backlog, 1=EmProgresso, 2=Concluida, 3=Bloqueada',
  `prioridade` tinyint(4) NOT NULL DEFAULT 1 COMMENT '0=Baixa, 1=Media, 2=Alta, 3=Critica',
  `horas_estimadas` decimal(6,1) NOT NULL DEFAULT 0.0,
  `horas_registadas` decimal(6,1) NOT NULL DEFAULT 0.0,
  `data_prazo` date DEFAULT NULL,
  `membro_id` int(11) DEFAULT NULL,
  `sprint_id` int(11) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ;

--
-- Extraindo dados da tabela `tarefas`
--

INSERT INTO `tarefas` (`id`, `titulo`, `descricao`, `estado`, `prioridade`, `horas_estimadas`, `horas_registadas`, `data_prazo`, `membro_id`, `sprint_id`, `created_at`, `updated_at`) VALUES
(1, 'Criar estrutura da base de dados', 'Script SQL completo com todas as tabelas', 2, 2, 8.0, 8.5, NULL, 2, 1, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(2, 'Implementar login Windows Forms', 'Form de login com validações', 2, 3, 16.0, 18.0, NULL, 2, 1, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(3, 'Design do ecrã de login', 'Layout e cores do form de login', 2, 1, 6.0, 5.0, NULL, 5, 1, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(4, 'Testes de autenticação', 'Casos de teste para o módulo de login', 2, 2, 6.0, 7.0, NULL, 4, 1, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(5, 'Implementar encriptação de senha', 'Hash SHA256 para passwords', 2, 3, 10.0, 12.0, NULL, 3, 1, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(6, 'Modelo de utilizadores na BD', 'Tabela users com papéis e permissões', 2, 2, 4.0, 4.0, NULL, 2, 2, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(7, 'CRUD de utilizadores', 'Create/Read/Update/Delete de users', 1, 2, 14.0, 6.0, NULL, 3, 2, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(8, 'Validações de formulário', 'Validar campos obrigatórios', 0, 1, 6.0, 0.0, NULL, 2, 2, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(9, 'Testes CRUD utilizadores', 'Testes funcionais do módulo', 0, 2, 8.0, 0.0, NULL, 4, 2, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(10, 'Interface de listagem de users', 'DataGridView com pesquisa e filtros', 1, 1, 10.0, 4.0, NULL, 5, 2, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(11, 'Relatório de progresso', 'Gráfico de barras por sprint', 0, 2, 12.0, 0.0, '2025-02-25', 2, 3, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(12, 'Relatório de horas membro', 'DataGridView horas estimadas/reais', 0, 1, 8.0, 0.0, '2025-02-27', 3, 3, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(13, 'Exportação para PDF', 'Exportar relatórios em PDF', 3, 3, 16.0, 0.0, '2025-03-01', NULL, 3, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(14, 'Dashboard principal', 'Painel com KPIs do projecto', 0, 3, 20.0, 0.0, '2025-03-01', NULL, 3, '2026-06-08 20:53:17', '2026-06-08 20:53:17');

-- --------------------------------------------------------

--
-- Estrutura stand-in para vista `vw_horas_membro`
-- (Veja abaixo para a view atual)
--
CREATE TABLE `vw_horas_membro` (
`membro_id` int(11)
,`membro` varchar(100)
,`papel` tinyint(4)
,`equipa` varchar(100)
,`total_tarefas` bigint(21)
,`tarefas_concluidas` decimal(23,0)
,`horas_estimadas` decimal(28,1)
,`horas_registadas` decimal(28,1)
);

-- --------------------------------------------------------

--
-- Estrutura stand-in para vista `vw_kanban`
-- (Veja abaixo para a view atual)
--
CREATE TABLE `vw_kanban` (
`tarefa_id` int(11)
,`titulo` varchar(200)
,`estado` tinyint(4)
,`prioridade` tinyint(4)
,`horas_estimadas` decimal(6,1)
,`horas_registadas` decimal(6,1)
,`data_prazo` date
,`membro` varchar(100)
,`sprint_id` int(11)
,`sprint` varchar(100)
,`projecto_id` int(11)
,`projecto` varchar(150)
);

-- --------------------------------------------------------

--
-- Estrutura stand-in para vista `vw_progresso_projecto`
-- (Veja abaixo para a view atual)
--
CREATE TABLE `vw_progresso_projecto` (
`projecto_id` int(11)
,`projecto` varchar(150)
,`estado_projecto` tinyint(4)
,`cliente_nome` varchar(100)
,`equipa` varchar(100)
,`total_sprints` bigint(21)
,`total_tarefas` bigint(21)
,`tarefas_concluidas` decimal(23,0)
,`progresso_percent` decimal(28,1)
);

-- --------------------------------------------------------

--
-- Estrutura stand-in para vista `vw_velocidade_sprint`
-- (Veja abaixo para a view atual)
--
CREATE TABLE `vw_velocidade_sprint` (
`sprint_id` int(11)
,`sprint` varchar(100)
,`projecto` varchar(150)
,`data_inicio` date
,`data_fim` date
,`encerrado` tinyint(1)
,`total_tarefas` bigint(21)
,`tarefas_concluidas` decimal(23,0)
,`tarefas_bloqueadas` decimal(23,0)
,`percentagem_conclusao` decimal(28,1)
,`total_horas_estimadas` decimal(28,1)
,`velocidade` decimal(28,1)
);

-- --------------------------------------------------------

--
-- Estrutura para vista `vw_horas_membro`
--
DROP TABLE IF EXISTS `vw_horas_membro`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vw_horas_membro`  AS SELECT `m`.`id` AS `membro_id`, `m`.`nome` AS `membro`, `m`.`papel` AS `papel`, `e`.`nome` AS `equipa`, count(`t`.`id`) AS `total_tarefas`, sum(`t`.`estado` = 2) AS `tarefas_concluidas`, sum(`t`.`horas_estimadas`) AS `horas_estimadas`, sum(`t`.`horas_registadas`) AS `horas_registadas` FROM ((`membros` `m` join `equipas` `e` on(`e`.`id` = `m`.`equipa_id`)) left join `tarefas` `t` on(`t`.`membro_id` = `m`.`id`)) GROUP BY `m`.`id`, `m`.`nome`, `m`.`papel`, `e`.`nome` ;

-- --------------------------------------------------------

--
-- Estrutura para vista `vw_kanban`
--
DROP TABLE IF EXISTS `vw_kanban`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vw_kanban`  AS SELECT `t`.`id` AS `tarefa_id`, `t`.`titulo` AS `titulo`, `t`.`estado` AS `estado`, `t`.`prioridade` AS `prioridade`, `t`.`horas_estimadas` AS `horas_estimadas`, `t`.`horas_registadas` AS `horas_registadas`, `t`.`data_prazo` AS `data_prazo`, `m`.`nome` AS `membro`, `s`.`id` AS `sprint_id`, `s`.`nome` AS `sprint`, `p`.`id` AS `projecto_id`, `p`.`nome` AS `projecto` FROM (((`tarefas` `t` left join `membros` `m` on(`m`.`id` = `t`.`membro_id`)) join `sprints` `s` on(`s`.`id` = `t`.`sprint_id`)) join `projectos` `p` on(`p`.`id` = `s`.`projecto_id`)) ORDER BY `t`.`prioridade` DESC, `t`.`data_prazo` ASC ;

-- --------------------------------------------------------

--
-- Estrutura para vista `vw_progresso_projecto`
--
DROP TABLE IF EXISTS `vw_progresso_projecto`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vw_progresso_projecto`  AS SELECT `p`.`id` AS `projecto_id`, `p`.`nome` AS `projecto`, `p`.`estado` AS `estado_projecto`, `p`.`cliente_nome` AS `cliente_nome`, `e`.`nome` AS `equipa`, count(distinct `s`.`id`) AS `total_sprints`, count(`t`.`id`) AS `total_tarefas`, sum(`t`.`estado` = 2) AS `tarefas_concluidas`, round(sum(`t`.`estado` = 2) / nullif(count(`t`.`id`),0) * 100,1) AS `progresso_percent` FROM (((`projectos` `p` left join `equipas` `e` on(`e`.`id` = `p`.`equipa_id`)) left join `sprints` `s` on(`s`.`projecto_id` = `p`.`id`)) left join `tarefas` `t` on(`t`.`sprint_id` = `s`.`id`)) GROUP BY `p`.`id`, `p`.`nome`, `p`.`estado`, `p`.`cliente_nome`, `e`.`nome` ;

-- --------------------------------------------------------

--
-- Estrutura para vista `vw_velocidade_sprint`
--
DROP TABLE IF EXISTS `vw_velocidade_sprint`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vw_velocidade_sprint`  AS SELECT `s`.`id` AS `sprint_id`, `s`.`nome` AS `sprint`, `p`.`nome` AS `projecto`, `s`.`data_inicio` AS `data_inicio`, `s`.`data_fim` AS `data_fim`, `s`.`encerrado` AS `encerrado`, count(`t`.`id`) AS `total_tarefas`, sum(`t`.`estado` = 2) AS `tarefas_concluidas`, sum(`t`.`estado` = 3) AS `tarefas_bloqueadas`, round(sum(`t`.`estado` = 2) / nullif(count(`t`.`id`),0) * 100,1) AS `percentagem_conclusao`, sum(`t`.`horas_estimadas`) AS `total_horas_estimadas`, sum(case when `t`.`estado` = 2 then `t`.`horas_estimadas` else 0 end) AS `velocidade` FROM ((`sprints` `s` join `projectos` `p` on(`p`.`id` = `s`.`projecto_id`)) left join `tarefas` `t` on(`t`.`sprint_id` = `s`.`id`)) GROUP BY `s`.`id`, `s`.`nome`, `p`.`nome`, `s`.`data_inicio`, `s`.`data_fim`, `s`.`encerrado` ;

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `equipas`
--
ALTER TABLE `equipas`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `nome` (`nome`);

--
-- Índices para tabela `historico_tarefas`
--
ALTER TABLE `historico_tarefas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_hist_tarefa` (`tarefa_id`);

--
-- Índices para tabela `membros`
--
ALTER TABLE `membros`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `fk_membro_equipa` (`equipa_id`);

--
-- Índices para tabela `projectos`
--
ALTER TABLE `projectos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_projecto_equipa` (`equipa_id`);

--
-- Índices para tabela `sprints`
--
ALTER TABLE `sprints`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_sprint_projecto` (`projecto_id`);

--
-- Índices para tabela `tarefas`
--
ALTER TABLE `tarefas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_tarefa_membro` (`membro_id`),
  ADD KEY `fk_tarefa_sprint` (`sprint_id`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `equipas`
--
ALTER TABLE `equipas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de tabela `historico_tarefas`
--
ALTER TABLE `historico_tarefas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de tabela `membros`
--
ALTER TABLE `membros`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de tabela `projectos`
--
ALTER TABLE `projectos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `sprints`
--
ALTER TABLE `sprints`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `tarefas`
--
ALTER TABLE `tarefas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restrições para despejos de tabelas
--

--
-- Limitadores para a tabela `historico_tarefas`
--
ALTER TABLE `historico_tarefas`
  ADD CONSTRAINT `fk_hist_tarefa` FOREIGN KEY (`tarefa_id`) REFERENCES `tarefas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limitadores para a tabela `membros`
--
ALTER TABLE `membros`
  ADD CONSTRAINT `fk_membro_equipa` FOREIGN KEY (`equipa_id`) REFERENCES `equipas` (`id`) ON UPDATE CASCADE;

--
-- Limitadores para a tabela `projectos`
--
ALTER TABLE `projectos`
  ADD CONSTRAINT `fk_projecto_equipa` FOREIGN KEY (`equipa_id`) REFERENCES `equipas` (`id`) ON UPDATE CASCADE;

--
-- Limitadores para a tabela `sprints`
--
ALTER TABLE `sprints`
  ADD CONSTRAINT `fk_sprint_projecto` FOREIGN KEY (`projecto_id`) REFERENCES `projectos` (`id`) ON UPDATE CASCADE;

--
-- Limitadores para a tabela `tarefas`
--
ALTER TABLE `tarefas`
  ADD CONSTRAINT `fk_tarefa_membro` FOREIGN KEY (`membro_id`) REFERENCES `membros` (`id`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_tarefa_sprint` FOREIGN KEY (`sprint_id`) REFERENCES `sprints` (`id`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
