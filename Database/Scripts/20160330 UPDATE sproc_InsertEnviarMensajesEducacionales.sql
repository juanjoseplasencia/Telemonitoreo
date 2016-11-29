USE [DB_Telemonitoreo]
GO

ALTER PROCEDURE [dbo].[sproc_InsertEnviarMensajesEducacionales]
AS
BEGIN
SET NOCOUNT ON
--Define el primer dia de la semana / Lunes
SET DATEFIRST 1;
DECLARE @HoraActual INT, @DiaActual INT, @DiaSemana INT
DECLARE @FechaUltimaEjecucion datetime

SELECT @HoraActual = DATEPART(hh, GETDATE())
SELECT @FechaUltimaEjecucion = MAX(RP.FechaUltimaEjecucion) 
FROM  RegistroProceso RP
WHERE RP.IdProceso = 2 -- IdProceso para sproc_InsertEnviarMensajesEducacionales

IF (@HoraActual >= 8 And @HoraActual <= 17) And (DATEDIFF(mi, @FechaUltimaEjecucion, getdate()) >= 15 Or (@FechaUltimaEjecucion IS NULL)) 
	BEGIN
		SELECT @DiaActual = DATEPART(WEEKDAY, GETDATE())
		SELECT @DiaSemana = @DiaActual

		;WITH GestantesSemana(GestanteKey, GestanteTelefono, SemanaEmbarazo,DiaSemanaEmbarazo, HorarioMensaje)
		AS
		(
			SELECT GestanteKey, GestanteTelefono, DATEDIFF(dd, FechaUltimaRegla, GETDATE())/7 AS SemanaEmbarazo, 
			DATEDIFF(dd, FechaUltimaRegla, GETDATE())%7 AS DiaSemanaEmbarazo,HorarioMensaje FROM Gestante WHERE Eliminado = 0
		)
		INSERT INTO [ProcesoMensajeEducacional]([IdContenidoMensajeEducacional],[GestanteKey],[FechaEnvio],[Turno],[Procesado])
		SELECT CME.IdContenidoMensajeEducacional, GS.GestanteKey, GETDATE(), GS.HorarioMensaje, 0 FROM ContenidoMensajeEducacional CME INNER JOIN MensajeEducacional ME ON 
		CME.IdMensajeEducacional = ME.IdMensajeEducacional INNER JOIN GestantesSemana GS ON 
		ME.SemanaEmbarazo = GS.SemanaEmbarazo AND CME.DiaSemana = @DiaSemana WHERE ME.Eliminado = 0 AND  
		NOT EXISTS (SELECT * FROM ProcesoMensajeEducacional WHERE IdContenidoMensajeEducacional = CME.IdContenidoMensajeEducacional AND GestanteKey = GS.GestanteKey)

		/*Inicio proceso mensaje*/
		IF (@HoraActual) >= 14
			BEGIN
				UPDATE PME SET PME.FechaHoraProceso = GETDATE(), PME.Procesado = 1, PME.ResultadoProceso = 'Fuera de Hora' 
				FROM [ProcesoMensajeEducacional] PME WHERE PME.Turno = 1 AND PME.Procesado = 0 AND CONVERT (date, PME.FechaEnvio) = CONVERT (date, GETDATE())
		
				INSERT INTO	[SmsQueue](
						[NumeroMovil],
						[TipoMensaje],
						[CuerpoMensaje],
						[FechaCreacion],
						[Procesado],
						[ResultadoProceso],
						[ErrorProceso],
						[FechaProceso]
						)
				SELECT	G.[GestanteTelefono] AS NumeroMovil,
						'Mens. Educacional' AS TipoMensaje,
						CME.Contenido AS CuerpoMensaje,
						GETDATE() AS [FechaCreacion],
						0 AS [Procesado],
						'' AS [ResultadoProceso],
						0 AS [ErrorProceso],
						NULL AS [FechaProceso]
				FROM [ProcesoMensajeEducacional] PME INNER JOIN ContenidoMensajeEducacional CME ON PME.IdContenidoMensajeEducacional = CME.IdContenidoMensajeEducacional 
				INNER JOIN MensajeEducacional ME ON CME.IdMensajeEducacional = ME.IdMensajeEducacional INNER JOIN Gestante G ON PME.GestanteKey = G.GestanteKey WHERE 
				ME.Eliminado = 0 AND PME.Turno = 2 AND PME.Procesado = 0 AND CONVERT (date, PME.FechaEnvio) = CONVERT (date, GETDATE())

				UPDATE PME SET PME.FechaHoraProceso = GETDATE(), PME.Procesado = 1, PME.ResultadoProceso = 'Completado' 
				FROM [ProcesoMensajeEducacional] PME INNER JOIN ContenidoMensajeEducacional CME ON PME.IdContenidoMensajeEducacional = CME.IdContenidoMensajeEducacional 
				INNER JOIN MensajeEducacional ME ON CME.IdMensajeEducacional = ME.IdMensajeEducacional INNER JOIN Gestante G ON PME.GestanteKey = G.GestanteKey WHERE 
				ME.Eliminado = 0 AND PME.Turno = 2 AND PME.Procesado = 0 AND CONVERT (date, PME.FechaEnvio) = CONVERT (date, GETDATE())
			END
		ELSE
			BEGIN
				INSERT INTO	[SmsQueue](
						[NumeroMovil],
						[TipoMensaje],
						[CuerpoMensaje],
						[FechaCreacion],
						[Procesado],
						[ResultadoProceso],
						[ErrorProceso],
						[FechaProceso]
						)
				SELECT	G.[GestanteTelefono] AS NumeroMovil,
						'Mens. Educacional' AS TipoMensaje,
						CME.Contenido AS CuerpoMensaje,
						GETDATE() AS [FechaCreacion],
						0 AS [Procesado],
						'' AS [ResultadoProceso],
						0 AS [ErrorProceso],
						NULL AS [FechaProceso]
				FROM [ProcesoMensajeEducacional] PME INNER JOIN ContenidoMensajeEducacional CME ON PME.IdContenidoMensajeEducacional = CME.IdContenidoMensajeEducacional 
				INNER JOIN MensajeEducacional ME ON CME.IdMensajeEducacional = ME.IdMensajeEducacional INNER JOIN Gestante G ON PME.GestanteKey = G.GestanteKey WHERE 
				ME.Eliminado = 0 AND PME.Turno = 1 AND PME.Procesado = 0 AND CONVERT (date, PME.FechaEnvio) = CONVERT (date, GETDATE())
		
				UPDATE PME SET PME.FechaHoraProceso = GETDATE(), PME.Procesado = 1, PME.ResultadoProceso = 'Completado' 
				FROM [ProcesoMensajeEducacional] PME INNER JOIN ContenidoMensajeEducacional CME ON PME.IdContenidoMensajeEducacional = CME.IdContenidoMensajeEducacional 
				INNER JOIN MensajeEducacional ME ON CME.IdMensajeEducacional = ME.IdMensajeEducacional INNER JOIN Gestante G ON PME.GestanteKey = G.GestanteKey WHERE 
				ME.Eliminado = 0 AND PME.Turno = 1 AND PME.Procesado = 0 AND CONVERT (date, PME.FechaEnvio) = CONVERT (date, GETDATE())
			END

		Insert Into RegistroProceso(IdProceso, FechaUltimaEjecucion) 
		Values (2, GETDATE())

		SELECT CAST(1 AS INT) AS Resultado
	END
ELSE
	Select CAST(0 AS INT) as Resultado
END

GO