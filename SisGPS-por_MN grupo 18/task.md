# Tarefas — SisGPS Fase 2

## 1. Inicialização do Banco de Dados
- [ ] Implementar `ConexaoBD.InicializarBD()` para criar tabelas em falta
- [ ] Chamar `ConexaoBD.InicializarBD()` em `Program.Main()`
- [ ] Configurar login inicial obrigatório em `Program.Main()`

## 2. Autenticação e Telas Auxiliares
- [ ] Atualizar `FrmLogin.Designer.cs` (adicionar links de registo e recuperação)
- [ ] Atualizar `FrmLogin.cs` (lógica de registo de acessos e links)
- [ ] Criar `FrmRegistoUtilizador.Designer.cs` e `FrmRegistoUtilizador.cs`
- [ ] Criar `FrmRecuperarSenha.Designer.cs` and `FrmRecuperarSenha.cs`

## 3. Gestão de Utilizadores
- [ ] Criar `FrmUtilizadores.Designer.cs` e `FrmUtilizadores.cs` (CRUD, filtros, listagem)
- [ ] Ligar `menuUtilizadores` ou atalho correspondente em `FrmPrincipal`

## 4. Redesenho do Dashboard Principal
- [ ] Redesenhar `pnlDashboard` em `FrmPrincipal.Designer.cs` usando controlos estáticos (9 KPI cards e atalhos rápidos)
- [ ] Atualizar lógica em `FrmPrincipal.cs` para atualizar os controlos estáticos com base no `ServicoDashboard`
- [ ] Atualizar `ServicoDashboard.cs` para calcular os 9 indicadores corretos
- [ ] Adaptar controlo de visibilidade de atalhos e menus com base nas permissões (Admin/Funcionário)

## 5. Exportação de Dados
- [ ] Adicionar botões de Exportação CSV/PDF em `FrmProjectos.Designer.cs` e implementar lógica
- [ ] Adicionar botões de Exportação CSV/PDF em `FrmTarefas.Designer.cs` e implementar lógica
- [ ] Adicionar botões de Exportação CSV/PDF em `FrmEquipa.Designer.cs` e implementar lógica
- [ ] Adicionar botões de Exportação CSV/PDF em `FrmSprints.Designer.cs` e implementar lógica

## 6. Configuração SMTP e Notificações de E-mail
- [ ] Criar `FrmConfigSMTP.Designer.cs` e `FrmConfigSMTP.cs`
- [ ] Atualizar `ServicoEmail.cs` para obter configurações da tabela `configuracao_smtp`
- [ ] Adicionar notificações por e-mail automáticas ao atribuir, alterar e concluir tarefas

## 7. Limpeza de Comentários e Revisão Geral
- [ ] Remover comentários gerados por IA e redundantes nos ficheiros novos/modificados
- [ ] Validar traduções para Português de todos os componentes
- [ ] Compilar e executar testes gerais
