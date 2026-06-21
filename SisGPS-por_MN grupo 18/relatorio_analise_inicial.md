# Relatório de Análise Inicial — SisGPS

Este relatório apresenta o diagnóstico completo do estado atual do sistema **SisGPS — Sistema de Gestão de Projectos de Software** (C#, Windows Forms, MySQL) antes de qualquer modificação, identificando funcionalidades implementadas, em falta, erros e melhorias.

---

## 1. Funcionalidades Concluídas (Estado Atual)

*   **Arquitetura base organizada**: Divisão lógica em pastas: `Forms`, `Dall` (Data Access Layer), `Modelos`, `Enums`, `Exceptions`, `Servicos` e `Utils`.
*   **Repositórios de Dados (DAL)**: CRUD básico de Entidades (`ProjectoRepository`, `SprintRepository`, `MembroRepository`, `EquipaRepository`, `TarefaRepository`, `UtilizadorRepository`, `EmailRepository`).
*   **Lógicas de Domínio**: Lógica de atribuição de tarefas e regras de negócio (`GestorProjectos`, `ValidadorNegocio`).
*   **Menu Principal MDI (`FrmPrincipal`)**: Estrutura de menu com controle de acesso básico via permissões de utilizador.
*   **Listagens de Entidades**: Formulários como `FrmProjectos`, `FrmEquipa`, `FrmSprints` com carregamento de dados e filtros iniciais.
*   **Visualizador Kanban (`FrmKanban`)**: Distribuição básica de tarefas em ListBoxes nas 4 colunas clássicas.
*   **Relatórios Básicos (`FrmRelatorio`)**: 3 visualizações por meio de DataGridViews.
*   **Gestão de E-mails (`FrmEmails`)**: Cadastro de e-mails para fila de envio na base de dados.
*   **Classe de Encriptação (`Criptografia`)**: Implementação de Hash SHA-256 para palavras-passe.

---

## 2. Funcionalidades Incompletas e Pendentes

*   **Painel Principal (Dashboard)**: O painel inicial em `FrmPrincipal.cs` gera componentes em tempo de execução via código (`new Panel()`, `new Label()`, `new Button()`), violando o requisito de usar o Visual Studio Designer. Além disso, não possui todos os 9 indicadores solicitados (Falta: Projetos Concluídos, Sprints Abertos, Tarefas Em Progresso, Tarefas Concluídas).
*   **Sistema de Autenticação Completo**:
    *   Falta o fluxo inicial de login obrigatório antes de abrir a aplicação principal.
    *   Falta a tela de **Cadastro de Utilizadores** para novos funcionários e a tela de **Recuperação de Senha** (pergunta/resposta de segurança).
    *   Falta o **Registo de Acessos** na base de dados (histórico de quando cada utilizador entrou no sistema).
*   **Gestão de Utilizadores (CRUD)**: Não há interface gráfica (`Form`) dedicada ao controlo e listagem de utilizadores pelo administrador.
*   **Exportação de Dados**: As funções de exportação (PDF e CSV) estão definidas no serviço `ServicoExportacao`, mas não há interface com botões de exportação nos formulários (`FrmProjectos`, `FrmTarefas`, `FrmEquipa`, `FrmSprints`) para permitir ao utilizador descarregar os dados.
*   **Sistema de E-mails e SMTP**:
    *   A configuração de SMTP é definida apenas via variáveis de ambiente, sem tela de configuração interna.
    *   Falta o envio automático de e-mails em eventos críticos de tarefas (criação, atribuição, mudança de estado, conclusão).
*   **Comentários Excessivos**: Muitos ficheiros contêm comentários redundantes ou auto-gerados que indicam automação excessiva.

---

## 3. Erros de Implementação Identificados

*   **Bypass de Segurança**: O `FrmPrincipal` carrega mesmo que `Sessao.UtilizadorActual` seja nulo (sem login validado no início em `Program.cs`), deixando o menu visível para utilização de utilizadores não autenticados.
*   **Criação de Controles Dinâmicos**: O Dashboard e alguns atalhos utilizam inicialização em tempo de execução (`new Button`, `new Panel`), o que impossibilita a customização visual no Windows Forms Designer e afasta o projeto de um aspecto feito manualmente por estudantes.
*   **Erro de Referência no Projeto**: O arquivo do projeto `.csproj` contém referências órfãs à DLL `MySql.Data` que causam avisos de compilação, embora use o `MySqlConnector` moderno.

---

## 4. Oportunidades de Melhoria

*   **Interface mais Humana e Limpa**: Ajustar alinhamento, preenchimento (padding/margins) e espaçamento de botões para um aspeto académico de alta qualidade.
*   **Centralização da Ligação de Dados**: Criar um utilitário simples de migração/verificação na base de dados na inicialização do sistema para criar e povoar automaticamente tabelas em falta (`utilizadores`, `emails`, `registos_acesso`, `configuracao_smtp`).
*   **Nomenclatura Consistente**: Garantir que as tabelas, colunas, variáveis e mensagens do sistema estejam inteiramente em Português sem mistura desnecessária de termos ingleses.
