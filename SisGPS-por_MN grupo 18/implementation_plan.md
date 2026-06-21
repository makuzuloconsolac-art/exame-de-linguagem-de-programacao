# SisGPS — Plano de Implementação Completo (Fase 2)

Este plano detalha o desenho técnico e as alterações necessárias para implementar os novos requisitos do **SisGPS**, mantendo o aspeto académico, profissional e preservando a arquitetura original.

---

## 1. Modificações na Base de Dados e Inicialização Automática

Para garantir o funcionamento autónomo e sem erros de dependência, implementaremos uma rotina de inicialização automática na ligação de base de dados.

### [MODIFY] [ConexaoBD.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Dall/ConexaoBD.cs)
*   Adicionar o método `InicializarBD()` que executa os seguintes comandos SQL (caso as tabelas não existam):
    *   **Tabela `utilizadores`**: Incluir `ultimo_acesso` (DATETIME), `pergunta_seguranca` e `resposta_seguranca` (VARCHAR) para recuperação de senha.
    *   **Tabela `registos_acesso`**: Para manter o histórico de entradas dos utilizadores.
    *   **Tabela `configuracao_smtp`**: Para guardar as definições de e-mail localmente.
    *   Inserir o utilizador Administrador padrão (`admin` / `admin123`) caso a tabela esteja vazia.

---

## 2. Fluxo de Autenticação e Telas de Login

### [MODIFY] [Program.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Program.cs)
*   Ajustar a inicialização do programa:
    1. Chamar `ConexaoBD.InicializarBD()`.
    2. Abrir o `FrmLogin` como diálogo.
    3. Se autenticar com sucesso (`DialogResult.OK`), correr o `FrmPrincipal`. Caso contrário, fechar a aplicação de forma segura.

### [MODIFY] [FrmLogin.Designer.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmLogin.Designer.cs) & [FrmLogin.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmLogin.cs)
*   Remover a geração de atalhos e avisos estáticos do rodapé.
*   Adicionar links / botões criados via Designer:
    *   **Registar-se** (abre `FrmRegistoUtilizador`).
    *   **Recuperar Palavra-passe** (abre `FrmRecuperarSenha`).
*   Registar o acesso na tabela `registos_acesso` e atualizar o campo `ultimo_acesso` do utilizador autenticado.

### [NEW] [FrmRegistoUtilizador.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmRegistoUtilizador.cs) & [Designer](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmRegistoUtilizador.Designer.cs)
*   Formulário para cadastro de novos utilizadores com campos: Nome completo, Nome de utilizador (Username), Palavra-passe, Pergunta de segurança e Resposta de segurança.
*   Associação opcional a um Membro da equipa.

### [NEW] [FrmRecuperarSenha.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmRecuperarSenha.cs) & [Designer](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmRecuperarSenha.Designer.cs)
*   Formulário de recuperação de senha em duas etapas:
    1. Introduzir o Username e carregar a pergunta de segurança correspondente.
    2. Responder à pergunta. Se estiver correta, permitir definir uma nova palavra-passe.

---

## 3. Gestão Completa de Utilizadores

### [NEW] [FrmUtilizadores.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmUtilizadores.cs) & [Designer](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmUtilizadores.Designer.cs)
*   Ecrã acessível apenas por Administradores para controlo de acessos e CRUD de utilizadores.
*   **Funcionalidades**:
    *   Tabela (`DataGridView`) com a lista de utilizadores e data do seu último acesso.
    *   Barra de pesquisa inteligente por Nome ou Username.
    *   Filtro por Nível de Acesso (Administrador / Funcionário) e Estado (Ativo / Inativo).
    *   Botões de Ação: Novo, Editar, Eliminar e Ativar/Desativar utilizador.

---

## 4. Dashboard Estático no FrmPrincipal (Visual Designer)

### [MODIFY] [FrmPrincipal.Designer.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmPrincipal.Designer.cs) & [FrmPrincipal.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmPrincipal.cs)
*   **Designer**: Substituir a geração de KPI cards e botões via código por controlos estáticos arrumados numa grelha visual no Designer.
    *   Utilizar 9 `Panel`s com cores discretas e profissionais (Azul moderado, Cinza claro, Verde suave).
    *   Cada painel conterá um `Label` descritivo e um `Label` de valor em destaque para os 9 indicadores solicitados:
        1. Projetos Totais
        2. Projetos Ativos
        3. Projetos Concluídos
        4. Total de Equipas
        5. Total de Membros
        6. Sprints Abertos
        7. Tarefas Pendentes
        8. Tarefas Em Progresso
        9. Tarefas Concluídas
    *   Os botões de atalho rápido serão fixados em um painel lateral ou inferior criado diretamente no Designer.
*   **Lógica (`FrmPrincipal.cs`)**:
    *   Remover métodos `AdicionarKpi` e `AdicionarAtalho` que instanciavam controlos dinamicamente.
    *   Consultar `ServicoDashboard.cs` no carregamento (`Load`) e atualizar o texto dos `Labels` estáticos correspondentes.
    *   Ajustar a visibilidade dos botões de atalho e dos menus superiores conforme o nível de acesso (Administrador visualiza todas as opções; Funcionário visualiza apenas Tarefas e Kanban).

### [MODIFY] [ServicoDashboard.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Servicos/ServicoDashboard.cs)
*   Atualizar a classe `ResumoDashboard` e o cálculo de indicadores no método `ObterResumo` para incluir:
    *   `ProjectosConcluidos` (`SELECT COUNT(*) FROM projectos WHERE estado=3`)
    *   `TarefasEmProgresso` (`SELECT COUNT(*) FROM tarefas WHERE estado=1`)
    *   `TarefasConcluidas` (`SELECT COUNT(*) FROM tarefas WHERE estado=2`)

---

## 5. Exportação de Dados em CSV e PDF

Disponibilizaremos botões de exportação diretamente nos cabeçalhos das tabelas de listagem principais.

### [MODIFY]
Adicionar botões **Exportar CSV** e **Exportar PDF** criados no Designer aos seguintes formulários, integrando com o `ServicoExportacao`:
*   [FrmProjectos.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmProjectos.cs) / [Designer](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmProjectos.Designer.cs) (Exportação de Projetos)
*   [FrmTarefas.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmTarefas.cs) / [Designer](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmTarefas.Designer.cs) (Exportação de Tarefas)
*   [FrmEquipa.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmEquipa.cs) / [Designer](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmEquipa.Designer.cs) (Exportação de Equipas e Membros)
*   [FrmSprints.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmSprints.cs) / [Designer](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmSprints.Designer.cs) (Exportação de Sprints)

---

## 6. Configuração SMTP e Notificações de E-mail

### [NEW] [FrmConfigSMTP.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmConfigSMTP.cs) & [Designer](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Forms/FrmConfigSMTP.Designer.cs)
*   Formulário de configuração com campos: Servidor SMTP, Porta, E-mail do remetente, Palavra-passe do remetente e opção Usar SSL.
*   Gravação dos parâmetros de forma encriptada na base de dados (tabela `configuracao_smtp`).

### [MODIFY] [ServicoEmail.cs](file:///c:/Users/msima/3D%20Objects/SisGPS-por_MN/SisGPS-por_MN/Servicos/ServicoEmail.cs)
*   Ajustar os métodos de envio para obter dinamicamente as configurações da tabela `configuracao_smtp` antes do envio. Caso não existam configurações gravadas, utilizar as variáveis de ambiente como plano de contingência (*fallback*).

### [MODIFY] Lógicas de Transição de Tarefas para Enviar E-mails
Disparar notificações de e-mail automaticamente em ações fundamentais:
*   **Atribuição de Tarefa** (no `FrmTarefas.cs` ao selecionar um membro).
*   **Mudança de Estado** (no `FrmAlterarEstado.cs` e no Kanban ao trocar o estado de uma tarefa).
*   **Conclusão de Tarefa** (quando o estado passa a Concluída).

---

## 7. Limpeza e Aparência Académica

*   **Remover Comentários Redundantes**: Eliminar todos os comentários excessivos e cabeçalhos redundantes de comentários nos ficheiros novos e modificados para garantir uma aparência natural de escrita manual.
*   **Português Nativo**: Rever strings de erros, labels, enums e caixas de diálogo para garantir uma tradução portuguesa sem mistura de inglês.

---

## Plano de Verificação

### Testes de Compilação
- Correr `dotnet build` e assegurar que não existem avisos ou erros.

### Testes Funcionais
1. Apagar as tabelas `utilizadores` e `emails` locais para testar a migração automática ao abrir a aplicação.
2. Efetuar o login inicial com o utilizador `admin` padrão.
3. Testar a criação de utilizadores funcionais.
4. Entrar com uma conta de funcionário e validar que a interface oculta corretamente as opções administrativas.
5. Fazer alteração de estado no Kanban e validar o envio/registo de e-mail de notificação.
6. Exportar relatórios e listas para PDF e CSV.
