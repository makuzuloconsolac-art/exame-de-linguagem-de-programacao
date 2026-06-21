-- =====================================================================
-- SCRIPT DE POPULARIZAÇÃO DA BASE DE DADOS - gestao_projectos
-- =====================================================================
-- Este script insere dados de exemplo para todas as tabelas do sistema
-- de gestão de projetos, com nomes de profissionais da área de TI.
-- =====================================================================

-- Limpar dados existentes (mantendo estrutura)
SET FOREIGN_KEY_CHECKS = 0;

TRUNCATE TABLE historico_tarefas;
TRUNCATE TABLE tarefas;
TRUNCATE TABLE sprints;
TRUNCATE TABLE projectos;
TRUNCATE TABLE membros;
TRUNCATE TABLE equipas;

SET FOREIGN_KEY_CHECKS = 1;

-- =====================================================================
-- 1. EQUIPAS
-- =====================================================================
INSERT INTO equipas (id, nome, descricao) VALUES
(1, 'Equipa Orion', 'Equipa de desenvolvimento backend e frontend'),
(2, 'Equipa Phoenix', 'Equipa de qualidade e testes automatizados'),
(3, 'Equipa Atlas', 'Equipa de design UX/UI e experiência do utilizador'),
(4, 'Equipa Nova', 'Equipa de DevOps e infraestrutura cloud'),
(5, 'Equipa Sigma', 'Equipa de análise de dados e inteligência artificial'),
(6, 'Equipa Nexus', 'Equipa de suporte e manutenção de sistemas');

-- =====================================================================
-- 2. MEMBROS DAS EQUIPAS (profissionais de TI)
-- =====================================================================
INSERT INTO membros (id, nome, email, telefone, papel, equipa_id, disponivel) VALUES
-- Equipa Orion (Backend/Frontend)
(1, 'Carlos Alberto Silva', 'carlos.silva@empresa.ao', '923100001', 2, 1, 1),
(2, 'Mariana Fernandes Santos', 'mariana.santos@empresa.ao', '923100002', 0, 1, 1),
(3, 'João Pedro Costa', 'joao.costa@empresa.ao', '923100003', 0, 1, 1),
(4, 'Inês Sofia Ramos', 'ines.ramos@empresa.ao', '923100004', 0, 1, 1),
(5, 'Miguel Ângelo Lopes', 'miguel.lopes@empresa.ao', '923100005', 4, 1, 1),

-- Equipa Phoenix (QA/Testes)
(6, 'Rita de Cássia Vieira', 'rita.vieira@empresa.ao', '923100006', 1, 2, 1),
(7, 'André Luís Martins', 'andre.martins@empresa.ao', '923100007', 1, 2, 1),
(8, 'Carla Marisa Nunes', 'carla.nunes@empresa.ao', '923100008', 1, 2, 0),

-- Equipa Atlas (Design/UX)
(9, 'Beatriz Helena Ferreira', 'beatriz.ferreira@empresa.ao', '923100009', 3, 3, 1),
(10, 'Tiago Daniel Rodrigues', 'tiago.rodrigues@empresa.ao', '923100010', 3, 3, 1),
(11, 'Sara Carolina Mendes', 'sara.mendes@empresa.ao', '923100011', 3, 3, 0),

-- Equipa Nova (DevOps)
(12, 'Hugo Miguel Almeida', 'hugo.almeida@empresa.ao', '923100012', 4, 4, 1),
(13, 'Vera Lúcia Barbosa', 'vera.barbosa@empresa.ao', '923100013', 4, 4, 1),

-- Equipa Sigma (Dados/IA)
(14, 'Nuno Filipe Carvalho', 'nuno.carvalho@empresa.ao', '923100014', 0, 5, 1),
(15, 'Cristina Isabel Santos', 'cristina.santos@empresa.ao', '923100015', 2, 5, 1),
(16, 'Rui Jorge Monteiro', 'rui.monteiro@empresa.ao', '923100016', 0, 5, 1),

-- Equipa Nexus (Suporte)
(17, 'Paula Cristina Oliveira', 'paula.oliveira@empresa.ao', '923100017', 0, 6, 1),
(18, 'Gonçalo José Pereira', 'goncalo.pereira@empresa.ao', '923100018', 0, 6, 1);

-- =====================================================================
-- 3. PROJECTOS
-- =====================================================================
INSERT INTO projectos (id, nome, descricao, data_inicio, data_fim, orcamento, estado, cliente_nome, equipa_id) VALUES
(1, 'Sistema Bancário Digital', 'Plataforma de gestão bancária mobile e web', '2025-01-15', '2025-07-31', 85000.00, 1, 'Banco Económico', 1),
(2, 'Portal do Munícipe', 'Sistema de gestão de atendimento municipal e serviços públicos', '2025-02-01', '2025-09-30', 120000.00, 0, 'Governo Provincial de Luanda', 1),
(3, 'Plataforma E-Learning', 'Sistema de ensino à distância com videoaulas e quizzes', '2025-03-01', '2025-12-31', 65000.00, 0, 'Universidade Katyavala Bwila', 2),
(4, 'Gestão de Frota Logística', 'Sistema de rastreamento e gestão de veículos', '2025-04-01', '2025-08-31', 48000.00, 1, 'Logística Angolana S.A.', 2),
(5, 'App Saúde Integrada', 'Aplicação móvel para agendamento de consultas e telemedicina', '2025-05-01', '2025-11-30', 95000.00, 2, 'Ministério da Saúde', 1),
(6, 'Sistema de Gestão Académica', 'Plataforma integrada para gestão de estudantes, notas e horários', '2025-06-01', '2025-12-31', 75000.00, 0, 'Instituto Superior Politécnico', 3),
(7, 'Plataforma de Comércio Electrónico', 'Marketplace com integração de pagamentos e gestão de stock', '2025-07-01', '2026-01-31', 110000.00, 3, 'Comércio Digital, Lda', 1),
(8, 'Sistema de Recrutamento', 'Plataforma de gestão de candidaturas e processos seletivos', '2025-08-01', '2025-12-31', 35000.00, 0, 'RH Angolana, Lda', 2),
(9, 'Painel de Business Intelligence', 'Dashboard com indicadores de negócio e análise preditiva', '2025-09-01', '2026-02-28', 90000.00, 0, 'Grupo Sonangol', 5),
(10, 'Sistema de Irrigação Inteligente', 'Automação e monitorização de sistemas de irrigação agrícola', '2025-10-01', '2026-03-31', 55000.00, 4, 'Cooperativa Agrícola do Cunene', 4);

-- =====================================================================
-- 4. SPRINTS
-- =====================================================================
INSERT INTO sprints (id, nome, objectivo, data_inicio, data_fim, encerrado, projecto_id) VALUES
-- Projecto 1: Sistema Bancário Digital
(1, 'Sprint 1 — Autenticação e Segurança', 'Implementar login, registo e 2FA', '2025-01-20', '2025-02-03', 1, 1),
(2, 'Sprint 2 — Gestão de Contas', 'CRUD de contas bancárias e saldos', '2025-02-04', '2025-02-18', 1, 1),
(3, 'Sprint 3 — Transferências e Pagamentos', 'Módulo de transferências e pagamentos', '2025-02-19', '2025-03-05', 1, 1),
(4, 'Sprint 4 — Extratos e Relatórios', 'Geração de extratos e relatórios financeiros', '2025-03-06', '2025-03-20', 0, 1),

-- Projecto 2: Portal do Munícipe
(5, 'Sprint 1 — Estrutura Inicial', 'Arquitetura base e autenticação', '2025-02-05', '2025-02-19', 1, 2),
(6, 'Sprint 2 — Cadastro de Munícipes', 'Módulo de registo de cidadãos', '2025-02-20', '2025-03-06', 1, 2),
(7, 'Sprint 3 — Solicitações Online', 'Sistema de pedidos e atendimentos', '2025-03-07', '2025-03-21', 0, 2),

-- Projecto 3: Plataforma E-Learning
(8, 'Sprint 1 — Base de Dados e Backend', 'Modelagem de dados e API', '2025-03-05', '2025-03-19', 0, 3),

-- Projecto 4: Gestão de Frota
(9, 'Sprint 1 — Rastreamento GPS', 'Integração com API de geolocalização', '2025-04-05', '2025-04-19', 0, 4),

-- Projecto 7: Comércio Electrónico
(10, 'Sprint 1 — Catálogo de Produtos', 'Gestão de produtos e categorias', '2025-07-05', '2025-07-19', 0, 7),
(11, 'Sprint 2 — Carrinho de Compras', 'Sistema de carrinho e checkout', '2025-07-20', '2025-08-03', 0, 7);

-- =====================================================================
-- 5. TAREFAS
-- =====================================================================
INSERT INTO tarefas (id, titulo, descricao, estado, prioridade, horas_estimadas, horas_registadas, data_prazo, membro_id, sprint_id) VALUES
-- Sprint 1 (Autenticação e Segurança)
(1, 'Modelar tabelas de utilizadores e roles', 'Criar estrutura de dados para autenticação', 2, 3, 8.0, 9.0, '2025-01-22', 2, 1),
(2, 'Implementar API de login', 'Endpoint de autenticação com JWT', 2, 3, 12.0, 14.0, '2025-01-24', 3, 1),
(3, 'Design do formulário de login', 'Layout da tela de login da aplicação', 2, 2, 6.0, 5.0, '2025-01-21', 9, 1),
(4, 'Implementar registo de utilizador', 'Formulário de criação de conta', 2, 3, 10.0, 11.0, '2025-01-25', 2, 1),
(5, 'Testes de autenticação', 'Casos de teste para login e registo', 2, 2, 8.0, 8.0, '2025-01-26', 6, 1),
(6, 'Implementar 2FA via SMS', 'Dupla autenticação com código OTP', 1, 3, 14.0, 8.0, '2025-01-28', 3, 1),
(7, 'Design da tela de recuperação de senha', 'Layout para recuperação de password', 2, 1, 4.0, 3.0, '2025-01-23', 9, 1),

-- Sprint 2 (Gestão de Contas)
(8, 'Modelar tabela de contas bancárias', 'Estrutura para contas e saldos', 2, 3, 6.0, 6.0, '2025-02-06', 4, 2),
(9, 'Implementar CRUD de contas', 'Criação, leitura, atualização e eliminação', 2, 3, 16.0, 18.0, '2025-02-12', 2, 2),
(10, 'Validações de saldo mínimo', 'Regras de negócio para contas', 2, 2, 8.0, 7.0, '2025-02-10', 4, 2),
(11, 'Dashboard de contas do utilizador', 'Visualização de contas e saldos', 1, 2, 12.0, 6.0, '2025-02-14', 10, 2),
(12, 'Testes de integração contas', 'Testes do módulo de contas', 0, 2, 8.0, 0.0, '2025-02-15', 6, 2),

-- Sprint 3 (Transferências e Pagamentos)
(13, 'Implementar transferências entre contas', 'Lógica de transferência de valores', 2, 3, 20.0, 22.0, '2025-02-25', 3, 3),
(14, 'Integração com gateway de pagamento', 'API de pagamentos externos', 3, 3, 16.0, 0.0, '2025-02-28', 12, 3),
(15, 'Criar histórico de transações', 'Registo e consulta de movimentos', 1, 2, 10.0, 4.0, '2025-02-26', 2, 3),
(16, 'Design da tela de transferência', 'Layout da interface de transferências', 2, 1, 6.0, 5.0, '2025-02-24', 9, 3),

-- Sprint 4 (Extratos e Relatórios)
(17, 'Gerador de extratos PDF', 'Exportação de extratos bancários em PDF', 0, 3, 14.0, 0.0, '2025-03-10', 12, 4),
(18, 'Dashboard de relatórios financeiros', 'Gráficos e indicadores de movimentos', 0, 2, 18.0, 0.0, '2025-03-15', 10, 4),

-- Sprint 5 (Estrutura Inicial - Portal Munícipe)
(19, 'Estrutura de dados do sistema', 'Modelagem de base de dados do portal', 2, 3, 10.0, 11.0, '2025-02-07', 2, 5),
(20, 'Autenticação com BI', 'Login utilizando número de Bilhete de Identidade', 2, 3, 12.0, 13.0, '2025-02-10', 3, 5),
(21, 'Design da landing page', 'Página inicial do portal', 2, 2, 8.0, 7.0, '2025-02-08', 9, 5),

-- Sprint 6 (Cadastro de Munícipes)
(22, 'Registo de munícipe com BI', 'Formulário completo de cadastro', 2, 3, 14.0, 15.0, '2025-02-25', 2, 6),
(23, 'Validação de dados biométricos', 'Integração com sistema de biometria', 1, 3, 18.0, 10.0, '2025-02-28', 3, 6),
(24, 'Pesquisa e listagem de munícipes', 'Tabela com filtros e busca', 1, 2, 10.0, 5.0, '2025-02-27', 4, 6),

-- Sprint 7 (Solicitações Online)
(25, 'Sistema de pedidos de atendimento', 'Fluxo de solicitação de serviços', 0, 3, 16.0, 0.0, '2025-03-15', 2, 7),
(26, 'Acompanhamento de solicitações', 'Rastreamento de status dos pedidos', 0, 2, 12.0, 0.0, '2025-03-18', 10, 7),

-- Sprint 8 (Base de Dados e Backend - E-Learning)
(27, 'Modelagem de dados do E-Learning', 'Alunos, cursos, aulas e matrículas', 1, 3, 14.0, 8.0, '2025-03-07', 4, 8),
(28, 'API de cursos e aulas', 'Endpoints para gestão de conteúdos', 0, 3, 16.0, 0.0, '2025-03-12', 3, 8),

-- Sprint 9 (Rastreamento GPS - Frota)
(29, 'Integração com API de mapas', 'Geolocalização de veículos', 1, 3, 14.0, 6.0, '2025-04-08', 12, 9),
(30, 'Dashboard de rastreamento', 'Mapa com posição das viaturas', 0, 3, 16.0, 0.0, '2025-04-12', 10, 9),

-- Sprint 10 (Catálogo de Produtos - E-Commerce)
(31, 'Estrutura de categorias e produtos', 'Modelagem e CRUD de produtos', 2, 3, 12.0, 13.0, '2025-07-08', 2, 10),
(32, 'Upload de imagens de produtos', 'Sistema de armazenamento de imagens', 1, 2, 8.0, 4.0, '2025-07-10', 12, 10),
(33, 'Vitrine e busca de produtos', 'Página de produtos com filtros', 1, 3, 14.0, 6.0, '2025-07-12', 3, 10),

-- Sprint 11 (Carrinho de Compras)
(34, 'Sistema de carrinho de compras', 'Adicionar/remover itens do carrinho', 0, 3, 16.0, 0.0, '2025-07-25', 3, 11),
(35, 'Checkout e processamento de pagamento', 'Finalização de compra com integração financeira', 0, 3, 18.0, 0.0, '2025-07-30', 2, 11);

-- =====================================================================
-- 6. HISTÓRICO DE TAREFAS
-- =====================================================================
INSERT INTO historico_tarefas (tarefa_id, estado_anterior, estado_novo, observacao) VALUES
-- Tarefa 1
(1, 0, 1, 'Início do desenvolvimento da modelagem'),
(1, 1, 2, 'Modelo de dados aprovado pela equipa'),

-- Tarefa 2
(2, 0, 1, 'Implementação do endpoint de login iniciada'),
(2, 1, 2, 'API de login testada e funcionando'),

-- Tarefa 3
(3, 0, 1, 'Criação do design do login'),
(3, 1, 2, 'Design aprovado pelo cliente'),

-- Tarefa 4
(4, 0, 1, 'Desenvolvimento do registo de utilizadores'),
(4, 1, 2, 'Registo funcionando com validações'),

-- Tarefa 5
(5, 0, 1, 'Elaboração dos casos de teste'),
(5, 1, 2, 'Testes de autenticação 100% aprovados'),

-- Tarefa 6
(6, 0, 1, 'Implementação do 2FA em progresso'),

-- Tarefa 7
(7, 0, 1, 'Design da recuperação de senha iniciado'),
(7, 1, 2, 'Design aprovado pelo gestor de projeto'),

-- Tarefa 8
(8, 0, 1, 'Modelagem das contas iniciada'),
(8, 1, 2, 'Estrutura de dados finalizada'),

-- Tarefa 9
(9, 0, 1, 'CRUD de contas em implementação'),
(9, 1, 2, 'CRUD completo e testado'),

-- Tarefa 13 (teve bloqueio)
(13, 0, 1, 'Implementação de transferências iniciada'),
(13, 1, 2, 'Transferências funcionando com validações'),

-- Tarefa 14 (bloqueada)
(14, 0, 1, 'Integração com gateway de pagamento iniciada'),
(14, 1, 3, 'Bloqueado: API do parceiro inativa'),

-- Tarefa 19
(19, 0, 1, 'Modelagem do portal iniciada'),
(19, 1, 2, 'Estrutura de dados finalizada'),

-- Tarefa 20
(20, 0, 1, 'Autenticação com BI em desenvolvimento'),
(20, 1, 2, 'Login com BI validado'),

-- Tarefa 22
(22, 0, 1, 'Cadastro de munícipe iniciado'),
(22, 1, 2, 'Cadastro completo e testado'),

-- Tarefa 31
(31, 0, 1, 'CRUD de produtos implementado'),
(31, 1, 2, 'Gestão de produtos finalizada');

-- =====================================================================
-- 7. VERIFICAR DADOS INSERIDOS
-- =====================================================================
SELECT '=== EQUIPAS ===' AS '';
SELECT COUNT(*) AS total_equipas FROM equipas;

SELECT '=== MEMBROS ===' AS '';
SELECT COUNT(*) AS total_membros FROM membros;

SELECT '=== PROJECTOS ===' AS '';
SELECT COUNT(*) AS total_projectos FROM projectos;

SELECT '=== SPRINTS ===' AS '';
SELECT COUNT(*) AS total_sprints FROM sprints;

SELECT '=== TAREFAS ===' AS '';
SELECT COUNT(*) AS total_tarefas FROM tarefas;

SELECT '=== HISTÓRICO DE TAREFAS ===' AS '';
SELECT COUNT(*) AS total_historicos FROM historico_tarefas;

-- =====================================================================
-- 8. RELATÓRIOS RESUMO
-- =====================================================================
-- Distribuição de membros por equipa
SELECT 
    e.nome AS equipa,
    COUNT(m.id) AS total_membros,
    SUM(CASE WHEN m.disponivel = 1 THEN 1 ELSE 0 END) AS disponiveis
FROM equipas e
LEFT JOIN membros m ON m.equipa_id = e.id
GROUP BY e.id, e.nome
ORDER BY total_membros DESC;

-- Progresso dos projectos
SELECT 
    p.nome AS projecto,
    p.estado AS estado,
    COUNT(DISTINCT s.id) AS sprints,
    COUNT(t.id) AS tarefas,
    SUM(CASE WHEN t.estado = 2 THEN 1 ELSE 0 END) AS concluidas,
    ROUND(SUM(CASE WHEN t.estado = 2 THEN 1 ELSE 0 END) / NULLIF(COUNT(t.id), 0) * 100, 1) AS progresso
FROM projectos p
LEFT JOIN sprints s ON s.projecto_id = p.id
LEFT JOIN tarefas t ON t.sprint_id = s.id
GROUP BY p.id, p.nome, p.estado
ORDER BY p.id;

-- Tarefas por estado
SELECT 
    CASE estado
        WHEN 0 THEN 'Backlog'
        WHEN 1 THEN 'Em Progresso'
        WHEN 2 THEN 'Concluída'
        WHEN 3 THEN 'Bloqueada'
    END AS estado,
    COUNT(*) AS total
FROM tarefas
GROUP BY estado
ORDER BY estado;

-- Membros com mais tarefas atribuídas
SELECT 
    m.nome AS membro,
    COUNT(t.id) AS tarefas,
    SUM(CASE WHEN t.estado = 2 THEN 1 ELSE 0 END) AS concluidas,
    ROUND(SUM(CASE WHEN t.estado = 2 THEN 1 ELSE 0 END) / NULLIF(COUNT(t.id), 0) * 100, 1) AS eficiencia
FROM membros m
LEFT JOIN tarefas t ON t.membro_id = m.id
GROUP BY m.id, m.nome
HAVING COUNT(t.id) > 0
ORDER BY eficiencia DESC;

SELECT '=== POPULAÇÃO CONCLUÍDA COM SUCESSO ===' AS '';

COMMIT;