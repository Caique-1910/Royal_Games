CREATE DATABASE RoyalGames
GO

use VH_Burguer

drop database RoyalGames

USE RoyalGames
GO

CREATE TABLE Usuario(
	UsuarioID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(60) NOT NULL,
	Email VARCHAR(150) UNIQUE NOT NULL,
	Senha VARBINARY(32) NOT NULL,
	StatusUsuario BIT DEFAULT 1
)
GO

CREATE TABLE Jogo(
	JogoID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(150) UNIQUE NOT NULL,
	Preco DECIMAL(10,2) NOT NULL,
	Descricao NVARCHAR(MAX) NOT NULL,
	Imagem VARBINARY(MAX) NOT NULL,
	StatusJogo BIT DEFAULT 1,
	UsuarioID INT FOREIGN KEY REFERENCES Usuario(UsuarioID),
	ClassificacaoIndicativaID INT FOREIGN KEY REFERENCES ClassificacaoIndicativa(ClassificacaoInditicativoID)
)
GO

CREATE TABLE ClassificacaoIndicativa(
	ClassificacaoInditicativoID INT PRIMARY KEY IDENTITY,
	Classificacao VARCHAR(50) NOT NULL
)
GO



CREATE TABLE Log_AlteracaoJogo(
	Log_AlteracaoJogoID INT PRIMARY KEY IDENTITY,
	DataAlteracao DATETIME NOT NULL,
	NomeAnterior VARCHAR(100),
	PrecoAnterior DECIMAL (10,2),
	JogoID INT FOREIGN KEY REFERENCES Jogo(JogoID)
)
GO

CREATE TABLE Genero(
	GeneroID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(50) NOT NULL
)
GO

CREATE TABLE JogoGenero(
	JogoID INT NOT NULL,
	GeneroID INT NOT NULL,
	CONSTRAINT PK_JogoGenero PRIMARY KEY ( JogoID, GeneroID),
	CONSTRAINT FK_JogoGenero_Jogo FOREIGN KEY ( JogoID) REFERENCES Jogo(JogoID) ON DELETE CASCADE,
	CONSTRAINT FK_JogoGenero_Genero FOREIGN KEY ( GeneroID) REFERENCES Genero(GeneroID) ON DELETE CASCADE
)
GO

CREATE TABLE Plataforma(
	PlataformaId INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(50) NOT NULL
)
GO

CREATE TABLE JogoPlataforma(
	JogoID INT NOT NULL,
	PlataformaID INT NOT NULL,
	CONSTRAINT PK_JogoPlataforma PRIMARY KEY ( JogoID, PlataformaID),
	CONSTRAINT FK_JogoPlataforma_Jogo FOREIGN KEY ( JogoID) REFERENCES Jogo(JogoID) ON DELETE CASCADE,
	CONSTRAINT FK_JogoPlataforma_Plataforma FOREIGN KEY ( PlataformaID) REFERENCES Plataforma(PlataformaID) ON DELETE CASCADE
)
GO


-- Triggers

CREATE TRIGGER trg_ExclusaoUsuario
ON Usuario
INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE a SET StatusUsuario = 0
		FROM Usuario a
		INNER JOIN deleted d 
			ON d.UsuarioID = a.UsuarioID;
	END
	GO

CREATE TRIGGER trg_AltercaoProduto
ON Jogo
	AFTER UPDATE
	AS
	BEGIN
		INSERT INTO Log_AlteracaoJogo(DataAlteracao, JogoID, NomeAnterior, PrecoAnterior)
		SELECT GETDATE(), JogoID, Nome, Preco FROM deleted 
	END
	GO


CREATE TRIGGER trg_ExclusaoJogo
	ON Jogo
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE j SET StatusJogo = 0
		FROM Jogo j
		INNER JOIN deleted d 
			ON d.JogoID = j.JogoID;
	END
	GO

--- DML

INSERT INTO ClassificacaoIndicativa (Classificacao)
VALUES 
('Livre'),
('10 anos'),
('12 anos'),
('14 anos'),
('16 anos'),
('18 anos');
GO

INSERT INTO Usuario (Nome, Email, Senha)
VALUES
('Caique Lima', 'caique@email.com', HASHBYTES('SHA2_256','123456')),
('Ana Souza', 'ana@email.com', HASHBYTES('SHA2_256','senha123')),
('João Pereira', 'joao@email.com', HASHBYTES('SHA2_256','abc123'));
GO

INSERT INTO Genero (Nome)
VALUES
('Ação'),
('Aventura'),
('RPG'),
('Esporte'),
('Corrida');
GO

INSERT INTO Plataforma (Nome)
VALUES
('PC'),
('PlayStation'),
('Xbox'),
('Nintendo Switch');
GO

INSERT INTO Jogo
(Nome, Preco, Descricao, Imagem, UsuarioID, ClassificacaoIndicativaID)
VALUES
(
'Ea FC 26',199.90,'Jogo de futebol realista com times licenciados.',0x,1,3),
('Elder Ring',149.99,'Explore masmorras e enfrente criaturas épicas.',0x,2,4);
GO

INSERT INTO JogoGenero (JogoID, GeneroID)
VALUES
(1, 4), -- Futebol Pro 2025 -> Esporte
(2, 1), -- Aventura Sombria -> Ação
(2, 3); -- Aventura Sombria -> Aventura
GO

INSERT INTO JogoPlataforma (JogoID, PlataformaID)
VALUES
(1, 1), -- PC
(1, 2), -- PlayStation
(2, 1), -- PC
(2, 3); -- Xbox
GO