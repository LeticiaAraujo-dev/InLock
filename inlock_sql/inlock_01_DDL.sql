CREATE DATABASE inlock_games_manha

USE inlock_games_manha

CREATE TABLE estudios (
	idEstudio			INT PRIMARY KEY IDENTITY
	,nomeEstudio		VARCHAR(250) NOT NULL UNIQUE 
);

CREATE TABLE jogos(
	idJogo				INT PRIMARY KEY IDENTITY
	,nomeJogo			VARCHAR(200) NOT NULL UNIQUE
	,descricao			VARCHAR(250) NOT NULL
	,dataLancamento		DATE NOT NULL
	,valor				INT NOT NULL 
	,idEstudio			INT FOREIGN KEY REFERENCES estudios (idEstudio)
);

CREATE TABLE tiposDeUsuario(
	idTipoUsuario		INT PRIMARY KEY IDENTITY
	,titulo				VARCHAR(200) NOT NULL UNIQUE
);

CREATE TABLE usuarios(
	idUsuario			INT PRIMARY KEY IDENTITY
	,email				VARCHAR(200) UNIQUE NOT NULL
	,senha				VARCHAR(200) NOT NULL
	,idTipoUsuario		INT FOREIGN KEY REFERENCES tiposDeUsuario (idTipoUsuario)
);


