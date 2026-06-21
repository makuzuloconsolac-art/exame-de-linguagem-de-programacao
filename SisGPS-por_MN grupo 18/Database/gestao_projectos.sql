-- =============================================================================
-- SisGPS — Base de Dados: gestao_projectos
-- MariaDB 10.4+ | Gerado: 19-Jun-2026
--
-- ORDEM DE EXECUÇÃO:
--   1. Procedimentos armazenados (com comentários completos)
--   2. Estrutura das tabelas
--   3. Dados iniciais (seed)
--   4. Vistas (views) de relatório e Kanban
--   5. Índices, chaves primárias e estrangeiras
-- =============================================================================

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

CREATE DATABASE IF NOT EXISTS `gestao_projectos`
  DEFAULT CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE `gestao_projectos`;

-- =============================================================================
-- SECÇÃO 1 — PROCEDIMENTOS ARMAZENADOS
-- =============================================================================

DELIMITER $$

-- -----------------------------------------------------------------------------
-- sp_alterar_estado_tarefa
--
-- Propósito : Alterar o estado de uma tarefa e registar a transição no
--             histórico de forma atómica (uma única transacção).
--
-- Parâmetros:
--   p_tarefa_id   (IN)  — ID da tarefa a actualizar
--   p_novo_estado (IN)  — Novo estado: 0=Backlog, 1=EmProgresso,
--                         2=Concluida, 3=Bloqueada
--   p_observacao  (IN)  — Texto livre explicando a mudança (máx. 500 chars)
--
-- Comportamento:
--   1. Lê o estado actual da tarefa
--   2. Actualiza o campo estado em tarefas
--   3. Insere um registo em historico_tarefas com estado_anterior e estado_novo
--
-- Utilizado por: TarefaRepository.AlterarEstado() no SisGPS (Windows Forms)
-- -----------------------------------------------------------------------------
DROP PROCEDURE IF EXISTS `sp_alterar_estado_tarefa`$$
CREATE PROCEDURE `sp_alterar_estado_tarefa` (
    IN  `p_tarefa_id`   INT,
    IN  `p_novo_estado` TINYINT,
    IN  `p_observacao`  VARCHAR(500)
)
BEGIN
    DECLARE v_estado_anterior TINYINT;

    -- Guardar o estado actual antes da alteração
    SELECT estado INTO v_estado_anterior
    FROM tarefas
    WHERE id = p_tarefa_id;

    -- Actualizar o estado da tarefa
    UPDATE tarefas
    SET estado = p_novo_estado
    WHERE id = p_tarefa_id;

    -- Registar a transição no histórico (auditoria)
    INSERT INTO historico_tarefas (tarefa_id, estado_anterior, estado_novo, observacao)
    VALUES (p_tarefa_id, v_estado_anterior, p_novo_estado, p_observacao);
END$$

-- -----------------------------------------------------------------------------
-- sp_encerrar_sprint
--
-- Propósito : Encerrar formalmente um sprint, marcando-o como concluído e
--             devolvendo a contagem de tarefas ainda pendentes.
--
-- Parâmetros:
--   p_sprint_id  (IN)  — ID do sprint a encerrar
--   p_pendentes  (OUT) — Número de tarefas não concluídas (estado != 2)
--                        inclui Backlog, EmProgresso e Bloqueada
--
-- Regras de negócio:
--   - Não permite encerrar um sprint já encerrado (SIGNAL SQLSTATE '45000')
--   - Conta tarefas pendentes antes de marcar encerrado = 1
--
-- Utilizado por: SprintRepository.Encerrar() no SisGPS (Windows Forms)
-- -----------------------------------------------------------------------------
DROP PROCEDURE IF EXISTS `sp_encerrar_sprint`$$
CREATE PROCEDURE `sp_encerrar_sprint` (
    IN  `p_sprint_id` INT,
    OUT `p_pendentes` INT
)
BEGIN
    -- Impedir encerramento duplicado
    IF (SELECT encerrado FROM sprints WHERE id = p_sprint_id) = 1 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Sprint já está encerrado.';
    END IF;

    -- Contar tarefas pendentes (Backlog + EmProgresso + Bloqueada)
    SELECT COUNT(*) INTO p_pendentes
    FROM tarefas
    WHERE sprint_id = p_sprint_id
      AND estado != 2;

    -- Marcar sprint como encerrado
    UPDATE sprints
    SET encerrado = 1
    WHERE id = p_sprint_id;
END$$

DELIMITER ;

-- =============================================================================
-- SECÇÃO 2 — ESTRUTURA DAS TABELAS
-- =============================================================================

-- ── equipas ──────────────────────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS `equipas` (
  `id`         int(11)      NOT NULL,
  `nome`       varchar(100) NOT NULL,
  `descricao`  varchar(255) DEFAULT NULL,
  `created_at` datetime     NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
  COMMENT='Equipas de desenvolvimento';

-- ── membros ──────────────────────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS `membros` (
  `id`         int(11)      NOT NULL,
  `nome`       varchar(100) NOT NULL,
  `email`      varchar(100) DEFAULT NULL,
  `telefone`   varchar(20)  DEFAULT NULL,
  `papel`      tinyint(4)   NOT NULL DEFAULT 0
               COMMENT '0=Developer, 1=QA, 2=ProjectManager, 3=Designer, 4=DevOps',
  `equipa_id`  int(11)      NOT NULL,
  `disponivel` tinyint(1)   NOT NULL DEFAULT 1,
  `created_at` datetime     NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
  COMMENT='Membros das equipas';

-- ── projectos ────────────────────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS `projectos` (
  `id`           int(11)        NOT NULL,
  `nome`         varchar(150)   NOT NULL,
  `descricao`    text           DEFAULT NULL,
  `data_inicio`  date           NOT NULL,
  `data_fim`     date           DEFAULT NULL,
  `orcamento`    decimal(12,2)  DEFAULT NULL,
  `estado`       tinyint(4)     NOT NULL DEFAULT 0
                 COMMENT '0=Planeamento, 1=EmDesenvolvimento, 2=Pausado, 3=Concluido, 4=Cancelado',
  `cliente_nome` varchar(100)   DEFAULT NULL,
  `equipa_id`    int(11)        NOT NULL,
  `created_at`   datetime       NOT NULL DEFAULT current_timestamp(),
  `updated_at`   datetime       NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
  COMMENT='Projectos de software';

-- ── sprints ──────────────────────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS `sprints` (
  `id`           int(11)      NOT NULL,
  `nome`         varchar(100) NOT NULL,
  `objectivo`    varchar(500) DEFAULT NULL,
  `data_inicio`  date         NOT NULL,
  `data_fim`     date         NOT NULL,
  `encerrado`    tinyint(1)   NOT NULL DEFAULT 0,
  `projecto_id`  int(11)      NOT NULL,
  `created_at`   datetime     NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
  COMMENT='Sprints de cada projecto';

-- ── tarefas ──────────────────────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS `tarefas` (
  `id`               int(11)       NOT NULL,
  `titulo`           varchar(200)  NOT NULL,
  `descricao`        text          DEFAULT NULL,
  `estado`           tinyint(4)    NOT NULL DEFAULT 0
                     COMMENT '0=Backlog, 1=EmProgresso, 2=Concluida, 3=Bloqueada',
  `prioridade`       tinyint(4)    NOT NULL DEFAULT 1
                     COMMENT '0=Baixa, 1=Media, 2=Alta, 3=Critica',
  `horas_estimadas`  decimal(6,1)  NOT NULL DEFAULT 0.0,
  `horas_registadas` decimal(6,1)  NOT NULL DEFAULT 0.0,
  `data_prazo`       date          DEFAULT NULL,
  `membro_id`        int(11)       DEFAULT NULL,
  `sprint_id`        int(11)       NOT NULL,
  `created_at`       datetime      NOT NULL DEFAULT current_timestamp(),
  `updated_at`       datetime      NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
  COMMENT='Tarefas de cada sprint';

-- ── historico_tarefas ────────────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS `historico_tarefas` (
  `id`              int(11)      NOT NULL,
  `tarefa_id`       int(11)      NOT NULL,
  `estado_anterior` tinyint(4)   NOT NULL
                    COMMENT '0=Backlog,1=EmProg,2=Concluida,3=Bloqueada',
  `estado_novo`     tinyint(4)   NOT NULL
                    COMMENT '0=Backlog,1=EmProg,2=Concluida,3=Bloqueada',
  `data_mudanca`    datetime     NOT NULL DEFAULT current_timestamp(),
  `observacao`      varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
  COMMENT='Histórico de mudanças de estado das tarefas';

-- =============================================================================
-- SECÇÃO 3 — DADOS INICIAIS (SEED)
-- =============================================================================

INSERT INTO `equipas` (`id`, `nome`, `descricao`, `created_at`) VALUES
(1, 'Equipa Alpha', 'Equipa principal de desenvolvimento', '2026-06-08 20:53:16'),
(2, 'Equipa Beta',  'Equipa de testes e garantia de qualidade', '2026-06-08 20:53:16'),
(3, 'Equipa Gamma', 'Equipa de design e UX', '2026-06-08 20:53:16'),
(4, 'Isoft Tecnol', NULL, '2026-06-14 22:33:29')
ON DUPLICATE KEY UPDATE nome = VALUES(nome);

INSERT INTO `membros` (`id`, `nome`, `email`, `telefone`, `papel`, `equipa_id`, `disponivel`, `created_at`) VALUES
(1, 'Ana Lopes',      'ana@empresa.ao',   NULL, 2, 1, 1, '2026-06-08 20:53:17'),
(2, 'Bruno Ndozi',    'bruno@empresa.ao', NULL, 0, 1, 1, '2026-06-08 20:53:17'),
(3, 'Clara Pinto',    'clara@empresa.ao', NULL, 0, 1, 1, '2026-06-08 20:53:17'),
(4, 'David Mbemba',   'david@empresa.ao', NULL, 1, 2, 0, '2026-06-08 20:53:17'),
(5, 'Eva Kassoma',    'eva@empresa.ao',   NULL, 3, 3, 1, '2026-06-08 20:53:17'),
(6, 'Filipe Luvualu', 'filipe@empresa.ao',NULL, 4, 1, 0, '2026-06-08 20:53:17'),
(7, 'Graça Teca',     'graca@empresa.ao', NULL, 0, 1, 1, '2026-06-08 20:53:17')
ON DUPLICATE KEY UPDATE nome = VALUES(nome);

INSERT INTO `projectos` (`id`, `nome`, `descricao`, `data_inicio`, `data_fim`, `orcamento`, `estado`, `cliente_nome`, `equipa_id`, `created_at`, `updated_at`) VALUES
(1, 'Portal IPUKV',           'Portal académico da Universidade Kimpa Vita', '2025-01-10', '2025-06-30', 50000.00,  1, 'IPUKV',                        1, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(2, 'App Mobile Cooperativa', 'Aplicação de gestão de cooperativa agrícola', '2025-02-01', '2025-08-31', 35000.00,  3, 'Cooperativa Agrícola do Uíge', 1, '2026-06-08 20:53:17', '2026-06-09 06:18:00'),
(3, 'Sistema RH Empresa XYZ', 'Sistema de gestão de recursos humanos',     '2025-03-01', '2025-12-31', 80000.00,  0, 'Empresa XYZ',                  2, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(4, 'Ia',                     'ia curriculo',                              '2026-06-09', '2026-06-25', 255555.00, 0, 'Afonso',                       1, '2026-06-09 06:17:40', '2026-06-09 06:17:40')
ON DUPLICATE KEY UPDATE nome = VALUES(nome);

INSERT INTO `sprints` (`id`, `nome`, `objectivo`, `data_inicio`, `data_fim`, `encerrado`, `projecto_id`, `created_at`) VALUES
(1, 'Sprint 1 — Auth',    'Módulo de autenticação e dashboard',  '2025-01-15', '2025-01-29', 1, 1, '2026-06-08 20:53:17'),
(2, 'Sprint 2 — Users',   'Módulo de gestão de utilizadores',    '2025-02-01', '2025-02-14', 0, 1, '2026-06-08 20:53:17'),
(3, 'Sprint 3 — Reports', 'Módulo de relatórios e exportação',   '2025-02-17', '2025-03-03', 0, 1, '2026-06-08 20:53:17'),
(4, 'Sprint 1 — Core',    'Módulo base da app mobile',           '2025-02-05', '2025-02-20', 0, 2, '2026-06-08 20:53:17')
ON DUPLICATE KEY UPDATE nome = VALUES(nome);

INSERT INTO `tarefas` (`id`, `titulo`, `descricao`, `estado`, `prioridade`, `horas_estimadas`, `horas_registadas`, `data_prazo`, `membro_id`, `sprint_id`, `created_at`, `updated_at`) VALUES
(1,  'Criar estrutura da base de dados',  'Script SQL completo com todas as tabelas', 2, 2,  8.0,  8.5, NULL,         2, 1, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(2,  'Implementar login Windows Forms',   'Form de login com validações',             2, 3, 16.0, 18.0, NULL,         2, 1, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(3,  'Design do ecrã de login',           'Layout e cores do form de login',          2, 1,  6.0,  5.0, NULL,         5, 1, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(4,  'Testes de autenticação',            'Casos de teste para o módulo de login',    2, 2,  6.0,  7.0, NULL,         4, 1, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(5,  'Implementar encriptação de senha',  'Hash SHA256 para passwords',               2, 3, 10.0, 12.0, NULL,         3, 1, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(6,  'Modelo de utilizadores na BD',      'Tabela users com papéis e permissões',     2, 2,  4.0,  4.0, NULL,         2, 2, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(7,  'CRUD de utilizadores',              'Create/Read/Update/Delete de users',       1, 2, 14.0,  6.0, NULL,         3, 2, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(8,  'Validações de formulário',          'Validar campos obrigatórios',              0, 1,  6.0,  0.0, NULL,         2, 2, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(9,  'Testes CRUD utilizadores',          'Testes funcionais do módulo',              0, 2,  8.0,  0.0, NULL,         4, 2, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(10, 'Interface de listagem de users',    'DataGridView com pesquisa e filtros',      1, 1, 10.0,  4.0, NULL,         5, 2, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(11, 'Relatório de progresso',            'Gráfico de barras por sprint',             0, 2, 12.0,  0.0, '2025-02-25', 2, 3, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(12, 'Relatório de horas membro',         'DataGridView horas estimadas/reais',       0, 1,  8.0,  0.0, '2025-02-27', 3, 3, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(13, 'Exportação para PDF',               'Exportar relatórios em PDF',               3, 3, 16.0,  0.0, '2025-03-01', NULL,3, '2026-06-08 20:53:17', '2026-06-08 20:53:17'),
(14, 'Dashboard principal',               'Painel com KPIs do projecto',              0, 3, 20.0,  0.0, '2025-03-01', NULL,3, '2026-06-08 20:53:17', '2026-06-08 20:53:17')
ON DUPLICATE KEY UPDATE titulo = VALUES(titulo);

INSERT INTO `historico_tarefas` (`id`, `tarefa_id`, `estado_anterior`, `estado_novo`, `data_mudanca`, `observacao`) VALUES
(1,  1, 0, 1, '2026-06-08 20:53:17', 'Início do desenvolvimento'),
(2,  1, 1, 2, '2026-06-08 20:53:17', 'Script SQL validado e executado'),
(3,  2, 0, 1, '2026-06-08 20:53:17', 'Início do desenvolvimento do form de login'),
(4,  2, 1, 2, '2026-06-08 20:53:17', 'Form testado e aprovado pelo QA'),
(5,  3, 0, 1, '2026-06-08 20:53:17', 'Design iniciado'),
(6,  3, 1, 2, '2026-06-08 20:53:17', 'Design aprovado pelo cliente'),
(7,  4, 0, 1, '2026-06-08 20:53:17', 'Início dos testes'),
(8,  4, 1, 2, '2026-06-08 20:53:17', 'Todos os testes passaram'),
(9,  5, 0, 1, '2026-06-08 20:53:17', 'Início da implementação'),
(10, 5, 1, 3, '2026-06-08 20:53:17', 'Bloqueado: dependência de biblioteca externa'),
(11, 5, 3, 1, '2026-06-08 20:53:17', 'Desbloqueado após instalação do NuGet'),
(12, 5, 1, 2, '2026-06-08 20:53:17', 'Implementação concluída e testada')
ON DUPLICATE KEY UPDATE observacao = VALUES(observacao);

-- =============================================================================
-- SECÇÃO 4 — VISTAS (VIEWS)
-- =============================================================================

-- vw_horas_membro: horas estimadas vs registadas por membro de equipa
DROP VIEW IF EXISTS `vw_horas_membro`;
CREATE VIEW `vw_horas_membro` AS
SELECT
    m.id                          AS membro_id,
    m.nome                        AS membro,
    m.papel                       AS papel,
    e.nome                        AS equipa,
    COUNT(t.id)                   AS total_tarefas,
    SUM(t.estado = 2)             AS tarefas_concluidas,
    SUM(t.horas_estimadas)        AS horas_estimadas,
    SUM(t.horas_registadas)       AS horas_registadas
FROM membros m
JOIN equipas e ON e.id = m.equipa_id
LEFT JOIN tarefas t ON t.membro_id = m.id
GROUP BY m.id, m.nome, m.papel, e.nome;

-- vw_kanban: tarefas com contexto de sprint, projecto e membro (ecrã Kanban)
DROP VIEW IF EXISTS `vw_kanban`;
CREATE VIEW `vw_kanban` AS
SELECT
    t.id               AS tarefa_id,
    t.titulo           AS titulo,
    t.estado           AS estado,
    t.prioridade       AS prioridade,
    t.horas_estimadas  AS horas_estimadas,
    t.horas_registadas AS horas_registadas,
    t.data_prazo       AS data_prazo,
    m.nome             AS membro,
    s.id               AS sprint_id,
    s.nome             AS sprint,
    p.id               AS projecto_id,
    p.nome             AS projecto
FROM tarefas t
LEFT JOIN membros  m ON m.id = t.membro_id
JOIN      sprints  s ON s.id = t.sprint_id
JOIN      projectos p ON p.id = s.projecto_id
ORDER BY t.prioridade DESC, t.data_prazo ASC;

-- vw_progresso_projecto: percentagem de conclusão por projecto
DROP VIEW IF EXISTS `vw_progresso_projecto`;
CREATE VIEW `vw_progresso_projecto` AS
SELECT
    p.id                          AS projecto_id,
    p.nome                        AS projecto,
    p.estado                      AS estado_projecto,
    p.cliente_nome                AS cliente_nome,
    e.nome                        AS equipa,
    COUNT(DISTINCT s.id)          AS total_sprints,
    COUNT(t.id)                   AS total_tarefas,
    SUM(t.estado = 2)             AS tarefas_concluidas,
    CASE
        WHEN COUNT(t.id) = 0 THEN 0
        ELSE ROUND(SUM(t.estado = 2) / COUNT(t.id) * 100, 1)
    END                           AS progresso_percent
FROM projectos p
JOIN equipas e ON e.id = p.equipa_id
LEFT JOIN sprints s ON s.projecto_id = p.id
LEFT JOIN tarefas t ON t.sprint_id = s.id
GROUP BY p.id, p.nome, p.estado, p.cliente_nome, e.nome;

-- vw_velocidade_sprint: métricas de velocidade e conclusão por sprint
DROP VIEW IF EXISTS `vw_velocidade_sprint`;
CREATE VIEW `vw_velocidade_sprint` AS
SELECT
    s.id                          AS sprint_id,
    s.nome                        AS sprint,
    p.nome                        AS projecto,
    s.data_inicio                 AS data_inicio,
    s.data_fim                    AS data_fim,
    s.encerrado                   AS encerrado,
    COUNT(t.id)                   AS total_tarefas,
    SUM(t.estado = 2)             AS tarefas_concluidas,
    SUM(t.estado = 3)             AS tarefas_bloqueadas,
    CASE
        WHEN COUNT(t.id) = 0 THEN 0
        ELSE ROUND(SUM(t.estado = 2) / COUNT(t.id) * 100, 1)
    END                           AS percentagem_conclusao,
    SUM(t.horas_estimadas)        AS total_horas_estimadas,
    SUM(CASE WHEN t.estado = 2 THEN t.horas_estimadas ELSE 0 END) AS velocidade
FROM sprints s
JOIN projectos p ON p.id = s.projecto_id
LEFT JOIN tarefas t ON t.sprint_id = s.id
GROUP BY s.id, s.nome, p.nome, s.data_inicio, s.data_fim, s.encerrado;

-- =============================================================================
-- SECÇÃO 5 — ÍNDICES, CHAVES PRIMÁRIAS E ESTRANGEIRAS
-- =============================================================================

ALTER TABLE `equipas`
  ADD PRIMARY KEY (`id`);

ALTER TABLE `membros`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_membros_equipa` (`equipa_id`);

ALTER TABLE `projectos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_projectos_equipa` (`equipa_id`);

ALTER TABLE `sprints`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_sprints_projecto` (`projecto_id`);

ALTER TABLE `tarefas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_tarefas_sprint` (`sprint_id`),
  ADD KEY `fk_tarefas_membro` (`membro_id`);

ALTER TABLE `historico_tarefas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_historico_tarefa` (`tarefa_id`);

ALTER TABLE `equipas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

ALTER TABLE `membros`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

ALTER TABLE `projectos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

ALTER TABLE `sprints`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

ALTER TABLE `tarefas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

ALTER TABLE `historico_tarefas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

ALTER TABLE `membros`
  ADD CONSTRAINT `fk_membros_equipa`
    FOREIGN KEY (`equipa_id`) REFERENCES `equipas` (`id`)
    ON DELETE RESTRICT ON UPDATE CASCADE;

ALTER TABLE `projectos`
  ADD CONSTRAINT `fk_projectos_equipa`
    FOREIGN KEY (`equipa_id`) REFERENCES `equipas` (`id`)
    ON DELETE RESTRICT ON UPDATE CASCADE;

ALTER TABLE `sprints`
  ADD CONSTRAINT `fk_sprints_projecto`
    FOREIGN KEY (`projecto_id`) REFERENCES `projectos` (`id`)
    ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `tarefas`
  ADD CONSTRAINT `fk_tarefas_sprint`
    FOREIGN KEY (`sprint_id`) REFERENCES `sprints` (`id`)
    ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_tarefas_membro`
    FOREIGN KEY (`membro_id`) REFERENCES `membros` (`id`)
    ON DELETE SET NULL ON UPDATE CASCADE;

ALTER TABLE `historico_tarefas`
  ADD CONSTRAINT `fk_historico_tarefa`
    FOREIGN KEY (`tarefa_id`) REFERENCES `tarefas` (`id`)
    ON DELETE CASCADE ON UPDATE CASCADE;

COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
