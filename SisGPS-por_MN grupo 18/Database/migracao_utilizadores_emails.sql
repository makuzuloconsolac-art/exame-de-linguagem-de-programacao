USE `gestao_projectos`;

CREATE TABLE IF NOT EXISTS `utilizadores` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password_hash` varchar(64) NOT NULL,
  `nome` varchar(100) NOT NULL,
  `nivel_acesso` tinyint(4) NOT NULL DEFAULT 1,
  `membro_id` int(11) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`),
  KEY `fk_utilizador_membro` (`membro_id`),
  CONSTRAINT `fk_utilizador_membro` FOREIGN KEY (`membro_id`) REFERENCES `membros` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `emails` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `destinatario` varchar(150) NOT NULL,
  `assunto` varchar(200) NOT NULL,
  `corpo` text NOT NULL,
  `remetente` varchar(150) NOT NULL,
  `data_envio` datetime NOT NULL DEFAULT current_timestamp(),
  `enviado` tinyint(1) NOT NULL DEFAULT 0,
  `tarefa_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_email_tarefa` (`tarefa_id`),
  CONSTRAINT `fk_email_tarefa` FOREIGN KEY (`tarefa_id`) REFERENCES `tarefas` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `utilizadores` (`username`, `password_hash`, `nome`, `nivel_acesso`, `membro_id`, `activo`) VALUES
('admin', LOWER(SHA2('admin123', 256)), 'Ana Lopes', 0, 1, 1),
('bruno', LOWER(SHA2('bruno123', 256)), 'Bruno Ndozi', 1, 2, 1),
('clara', LOWER(SHA2('clara123', 256)), 'Clara Pinto', 1, 3, 1)
ON DUPLICATE KEY UPDATE nome = VALUES(nome);
