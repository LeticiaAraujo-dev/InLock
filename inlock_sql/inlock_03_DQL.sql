USE inlock_games_manha;

SELECT * FROM usuarios;
SELECT * FROM estudios;
SELECT * FROM jogos;

SELECT nomeJogo, descricao, dataLancamento, valor, nomeEstudio FROM jogos
INNER JOIN estudios
ON jogos.idEstudio = estudios.idEstudio

SELECT nomeEstudio, nomeJogo FROM estudios
FULL OUTER JOIN jogos
ON estudios.idEstudio = jogos.idEstudio

SELECT email, titulo 
FROM usuarios U
LEFT JOIN tiposDeUsuario TU
ON U.idTipoUsuario = TU.idTipoUsuario
WHERE email = 'admin@admin.com' AND senha = 'admin'

SELECT nomeJogo, descricao, dataLancamento, valor, nomeEstudio FROM jogos
INNER JOIN estudios
ON jogos.idEstudio = estudios.idEstudio
WHERE idJogo = 2

SELECT nomeEstudio FROM estudios
WHERE idEstudio = 3

