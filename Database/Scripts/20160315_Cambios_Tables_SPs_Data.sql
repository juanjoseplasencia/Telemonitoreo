USE DB_Telemonitoreo
GO

/****** Object:  Table [dbo].[MensajeEducativo]    Script Date: 03/16/2016 22:47:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MensajeEducativo]') AND type in (N'U'))
DROP TABLE [dbo].[MensajeEducativo]
GO

/****** Object:  Index [UIX_GestanteId]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Gestante]') AND name = N'UIX_GestanteId')
DROP INDEX [UIX_GestanteId] ON [dbo].[Gestante]
GO

/****** Object:  Index [UIX_GestanteNroDocumento]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Gestante]') AND name = N'UIX_GestanteNroDocumento')
DROP INDEX [UIX_GestanteNroDocumento] ON [dbo].[Gestante]
GO

USE [DB_Telemonitoreo]
GO
/****** Object:  StoredProcedure [dbo].[sproc_AddUpdateGestante]    Script Date: 03/05/2016 21:49:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Juan Plasencia
-- Create date: 2015-07-20
-- Description:	Crear nueva gestante en el sistema. Actualizar gestante existente
-- =============================================
ALTER PROCEDURE [dbo].[sproc_AddUpdateGestante]
 @pMode smallint
,@pGestanteKey int 
,@pGestanteNroDocumento varchar(8)
,@pNombres	varchar(11)
,@pAPaterno	varchar(11)
,@pAMaterno varchar(11)
,@pFechaNacimiento datetime
,@pFechaUltimaRegla datetime
,@pFechaProbableParto datetime
,@pPresionSistolicaBase int
,@pPresionDiastolicaBase int
,@DiagnosticoIngreso int
,@pDiagnosticoIntermedio1 int
,@pDiagnosticoIntermedio2 int
,@pDiagnosticoEgreso int
,@pEstablecimientoId int
,@pEstablecimientoNotificacionId int
,@pGestanteTelefono varchar(50)
,@pGestanteDireccion varchar(100)
,@pGestanteEmail varchar(50)
,@pDistritoId varchar(6)
,@pProvinciaId varchar(6)
,@pRegionId varchar(6)
,@pUsuarioEditor int
,@pHorarioMensaje tinyint

AS

BEGIN
	SET NOCOUNT ON;

	IF (@pMode = 0)
		BEGIN
			Insert Into Gestante ( 
				GestanteId,
				GestanteNroDocumento,
				Nombres,
				APaterno,
				AMaterno,
				FechaNacimiento,
				FechaUltimaRegla,
				FechaProbableParto,
				PresionSistolicaBase,
				PresionDiastolicaBase,
				DiagnosticoIngreso,
				DiagnosticoIntermedio1,
				DiagnosticoIntermedio2,
				DiagnosticoEgreso,
				EstablecimientoId,
				EstablecimientoNotificacionId,
				GestanteTelefono,
				GestanteDireccion,
				GestanteEmail,
				DistritoId,
				ProvinciaId,
				RegionId,
				FechaCreacion,
				UsuarioEditor,
				HorarioMensaje
			 ) 
			Values ( 
				'1' + @pGestanteNroDocumento + '00', 
				@pGestanteNroDocumento,
				@pNombres,	
				@pAPaterno,	
				@pAMaterno,
				@pFechaNacimiento, 
				@pFechaUltimaRegla, 
				@pFechaProbableParto,
				@pPresionSistolicaBase,
				@pPresionDiastolicaBase,
				@DiagnosticoIngreso, 
				@pDiagnosticoIntermedio1, 
				@pDiagnosticoIntermedio2, 
				@pDiagnosticoEgreso, 
				@pEstablecimientoId,
				@pEstablecimientoNotificacionId,				 
				@pGestanteTelefono, 
				@pGestanteDireccion, 
				@pGestanteEmail, 
				@pDistritoId, 
				LEFT(@pDistritoId,4) + '00',
				LEFT(@pDistritoId,2) + '0000',
				getdate(),
				@pUsuarioEditor,
				@pHorarioMensaje
			 ) 

			Select cast(SCOPE_IDENTITY() as int) as PKValue
		END
	ELSE
		BEGIN
			Update Gestante 
			Set  
				GestanteId = '1' + @pGestanteNroDocumento + '00',
				GestanteNroDocumento = @pGestanteNroDocumento,
				Nombres = @pNombres,
				APaterno = @pAPaterno,
				AMaterno = @pAMaterno,
				FechaNacimiento = @pFechaNacimiento,
				FechaUltimaRegla = @pFechaUltimaRegla,
				FechaProbableParto = @pFechaProbableParto,
				PresionSistolicaBase = @pPresionSistolicaBase,
				PresionDiastolicaBase = @pPresionDiastolicaBase,
				DiagnosticoIngreso = @DiagnosticoIngreso, 
				DiagnosticoIntermedio1 = @pDiagnosticoIntermedio1,
				DiagnosticoIntermedio2 = @pDiagnosticoIntermedio2,
				DiagnosticoEgreso = @pDiagnosticoEgreso,
				EstablecimientoId = @pEstablecimientoId,
				EstablecimientoNotificacionId = @pEstablecimientoNotificacionId,
				GestanteTelefono = @pGestanteTelefono,
				GestanteDireccion = @pGestanteDireccion,
				GestanteEmail = @pGestanteEmail,
				DistritoId = @pDistritoId,
				ProvinciaId = LEFT(@pDistritoId,4) + '00',
				RegionId = LEFT(@pDistritoId,2) + '0000',
				UsuarioEditor = @pUsuarioEditor,
				HorarioMensaje = @pHorarioMensaje
			Where GestanteKey = @pGestanteKey
			Select @pGestanteKey as PKValue
		END
END

GO

USE DB_Telemonitoreo
GO

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Eliminado' AND Object_ID = Object_ID(N'GestanteMedicamento'))
BEGIN
    ALTER TABLE GestanteMedicamento ADD Eliminado BIT NOT NULL DEFAULT '0'
END
GO

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Eliminado' AND Object_ID = Object_ID(N'MensajeEducacional'))
BEGIN
	ALTER TABLE MensajeEducacional ADD Eliminado BIT NOT NULL DEFAULT '0'
END
GO

ALTER PROCEDURE [dbo].[sproc_ReportEvolucionGestante]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	GM.[GestanteMonitoreoId]
			,GM.[GestanteKey]
			,GM.[PresionSistolica]
			,GM.[PresionDiastolica]
			,GM.[Proteinuria]
			,GM.[MovimientosFetales]
			,GM.[SignosAlarma]
			,GM.[FechaRegistro]
			,G.[GestanteNroDocumento]
			,G.[Nombres]
			,G.[APaterno]
			,G.[AMaterno]
			,DATEDIFF(yy, G.FechaNacimiento, GETDATE()) - CASE WHEN (MONTH(G.FechaNacimiento) > MONTH(GETDATE())) OR (MONTH(G.FechaNacimiento) = MONTH(GETDATE()) AND DAY(G.FechaNacimiento) > DAY(GETDATE())) THEN 1 ELSE 0 END AS Age
	FROM	[GestanteMonitoreo] GM (NOLOCK) INNER JOIN [Gestante] G (NOLOCK) ON
			GM.[GestanteKey] = G.[GestanteKey]
	WHERE	G.[Eliminado] = 0
	ORDER BY	GM.[FechaRegistro]
END
GO

ALTER PROCEDURE [dbo].[sproc_ReportGestantesParticipantes]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	G.Gestantekey,
			G.GestanteId,
			G.GestanteNroDocumento,
			G.GestanteDireccion,
			G.GestanteTelefono,
			DATEDIFF(yy, G.FechaNacimiento, GETDATE()) - CASE WHEN (MONTH(G.FechaNacimiento) > MONTH(GETDATE())) OR (MONTH(G.FechaNacimiento) = MONTH(GETDATE()) AND DAY(G.FechaNacimiento) > DAY(GETDATE())) THEN 1 ELSE 0 END AS Age,
			CONVERT(VARCHAR(4), G.PresionSistolicaBase) + '/' + CONVERT(VARCHAR(4), G.PresionDiastolicaBase) AS Presion,
			G.FechaProbableParto,
			DXI.Descripcion AS DiagnosticoIngreso,
			DXE.Descripcion AS DiagnosticoEgreso,
			Cita = (SELECT TOP 1 'Cita: ' + CONVERT(VARCHAR(10),GC.FechaCita,103) + ' - Establecimiento: ' + ES.Descripcion FROM GestanteCita GC 
			INNER JOIN Establecimiento ES ON GC.EstablecimientoId = ES.EstablecimientoId WHERE GC.GestanteKey = G.GestanteKey AND GC.Eliminado = 0 ORDER BY GC.FechaCita DESC),
			G.FechaCreacion,
			dep.Nombre as departamento,
			(SELECT STUFF((SELECT ',' + Descripcion 
            FROM	(SELECT DISTINCT M.Descripcion FROM GestanteMedicamento GM INNER JOIN GestanteMedicamentoDetalle GMD ON GM.GestanteMedicamentoId = GMD.GestanteMedicamentoId INNER JOIN 
					Medicamento M ON GMD.MedicamentoId = M.MedicamentoId WHERE GM.GestanteKey = G.GestanteKey AND GM.Eliminado = 0) AS Medicas
            FOR XML PATH('')) ,1,1,'') AS Medicinas) AS Medicamentos
	FROM	Gestante G 
			LEFT JOIN Diagnostico DXI ON G.DiagnosticoIngreso = DXI.Id
			LEFT JOIN Diagnostico DXE ON G.DiagnosticoEgreso = DXE.Id
			INNER JOIN  Ubigeo DEP ON LEFT(G.RegionId, 2) = LEFT(DEP.CodUbigeo,2) AND DEP.CodDist = '00' AND DEP.CodProv = '00'
			WHERE G.Eliminado = 0
	ORDER BY G.GestanteNroDocumento
END
GO

ALTER PROCEDURE [dbo].[sproc_ReportListadoEvolucionGestantes]
AS
BEGIN
SET NOCOUNT ON;
	SELECT	G.Gestantekey 
			,G.GestanteId
			,G. GestanteNroDocumento
			,G.FechaCreacion
			,DATEDIFF(yy, G.FechaNacimiento, GETDATE()) - CASE WHEN (MONTH(G.FechaNacimiento) > MONTH(GETDATE())) OR (MONTH(G.FechaNacimiento) = MONTH(GETDATE()) AND DAY(G.FechaNacimiento) > DAY(GETDATE())) THEN 1 ELSE 0 END AS Age
			,E.Descripcion
			,dep.Nombre as departamento
			,GM.GestanteKey as GestanteGM_KEY
			,GM.FechaRegistro AS Fecha
			,PresionArterial = CONVERT(CHAR(3), GM.PresionSistolica) + ' / ' + CONVERT (CHAR(3),GM.PresionDiastolica)
			,GM.Proteinuria
			,GM.MovimientosFetales
			,GM.SignosAlarma
			FROM	Gestante G INNER JOIN Establecimiento E ON G.EstablecimientoId = E.EstablecimientoId
					INNER JOIN GestanteMonitoreo GM ON GM.GestanteKey = G.GestanteKey
					INNER JOIN Ubigeo DEP ON LEFT(G.RegionId, 2) = LEFT(DEP.CodUbigeo,2) AND dep.CodDist = '00' AND dep.CodProv = '00'
			WHERE	G.Eliminado = 0
END
GO

ALTER PROCEDURE [dbo].[sproc_ReportMedicacionGestante]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	G.GestanteNroDocumento
			,G.Nombres
			,G.APaterno
			,G.AMaterno
			,GM.Fecha
			,DATEDIFF(yy, G.FechaNacimiento, GETDATE()) - CASE WHEN (MONTH(G.FechaNacimiento) > MONTH(GETDATE())) OR (MONTH(G.FechaNacimiento) = MONTH(GETDATE()) AND DAY(G.FechaNacimiento) > DAY(GETDATE())) THEN 1 ELSE 0 END AS Age
			,M.Descripcion + ' ' + GMD.Dosis AS Medicamento
			,GMD.Dias
			,GMD.Cantidad
			,DATEADD(dd,	GMD.Dias, GM.Fecha) AS FechaFin
	FROM	Gestante G (NOLOCK) INNER JOIN GestanteMedicamento GM (NOLOCK) 
	ON		G.GestanteKey = GM.GestanteKey INNER JOIN GestanteMedicamentoDetalle GMD (NOLOCK) 
	ON		GM.GestanteMedicamentoId = GMD.GestanteMedicamentoId INNER JOIN Medicamento M (NOLOCK) 
	ON		GMD.MedicamentoId = M.MedicamentoId
	WHERE	G.[Eliminado] = 0 AND GM.Eliminado = 0
	ORDER BY	GM.Fecha
END
GO

ALTER PROCEDURE [dbo].[sproc_ReportProcedenciaGestantes]
AS
BEGIN
SET NOCOUNT ON;
	SELECT	G.Gestantekey 
			,G.GestanteId
			,G. GestanteNroDocumento
			,			DATEDIFF(yy, G.FechaNacimiento, GETDATE()) - CASE WHEN (MONTH(G.FechaNacimiento) > MONTH(GETDATE())) OR (MONTH(G.FechaNacimiento) = MONTH(GETDATE()) AND DAY(G.FechaNacimiento) > DAY(GETDATE())) THEN 1 ELSE 0 END AS Age 
			,E.Descripcion AS Establecimiento
			,G.FechaCreacion
			,DXI.Descripcion AS DiagnosticoIngreso
			,Cita = (SELECT TOP 1 'Cita: ' + CONVERT(VARCHAR(10),GC.FechaCita,103) + ' - Establecimiento: ' + ES.Descripcion FROM GestanteCita GC 
			INNER JOIN Establecimiento ES ON GC.EstablecimientoId = ES.EstablecimientoId WHERE GC.GestanteKey = G.GestanteKey AND GC.Eliminado = 0 ORDER BY GC.FechaCita DESC)
			,Dist.Nombre AS Distrito
			,Prov.Nombre AS Provincia
			,dep.Nombre AS departamento
	FROM	Gestante G 
			INNER JOIN Establecimiento E ON E.EstablecimientoId = G.EstablecimientoId
			LEFT JOIN Diagnostico DXI ON G.DiagnosticoIngreso = DXI.Id
			INNER JOIN Ubigeo Dist ON G.DistritoId = Dist.CodUbigeo
			INNER JOIN Ubigeo Prov	ON LEFT (g.ProvinciaId,4 ) = left(prov.CodUbigeo, 4) AND prov.CodDist = '00'
			INNER JOIN Ubigeo DEP ON LEFT(g.RegionId, 2) = left(DEP.CodUbigeo,2) AND dep.CodDist = '00' AND dep.CodProv = '00'
			WHERE G.Eliminado = 0
END
GO

ALTER PROCEDURE [dbo].[sproc_InsertRecordatorioMedicamentos]
AS
BEGIN
SET NOCOUNT ON
DECLARE @hourTime INT
SELECT @hourTime = DATEPART(HOUR, GETUTCDATE())

IF (@hourTime = 2)
	BEGIN
		SELECT	G.GestanteTelefono AS [NumeroMovil],
				'Rec. Medicamento' AS [TipoMensaje],
				'Recuerde su ' + M.Descripcion + ' a tomar: ' + GMD.Cantidad + ' al día.' AS [CuerpoMensaje],
				GETDATE() AS [FechaCreacion],
				0 AS [Procesado],
				'' AS [ResultadoProceso],
				0 AS [ErrorProceso],
				NULL AS [FechaProceso],
				GMD.GestanteMedicamentoDetalleId AS [GestanteMedicamentoDetalleId]
		INTO #RecMedicamentos
		FROM GestanteMedicamento GM (NOLOCK) INNER JOIN GestanteMedicamentoDetalle GMD (NOLOCK) ON 
		GM.GestanteMedicamentoId = GMD.GestanteMedicamentoId INNER JOIN Medicamento M (NOLOCK) ON 
		GMD.MedicamentoId = M.MedicamentoId INNER JOIN Gestante G (NOLOCK) ON GM.GestanteKey = G.GestanteKey
		WHERE G.Eliminado = 0 AND GM.Eliminado = 0 AND G.GestanteTelefono IS NOT NULL AND 
		(GMD.FechaUltimoProceso IS NULL OR REPLACE(CONVERT(VARCHAR, GMD.FechaUltimoProceso, 111), '/', '') < REPLACE(CONVERT(VARCHAR, GETDATE(), 111), '/', '')) AND
		REPLACE(CONVERT(VARCHAR, GETDATE(), 111), '/', '') <= REPLACE(CONVERT(VARCHAR, DATEADD(dd,GMD.Dias,GM.Fecha), 111), '/', '')

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
		SELECT	[NumeroMovil],
				[TipoMensaje],
				[CuerpoMensaje],
				[FechaCreacion],
				[Procesado],
				[ResultadoProceso],
				[ErrorProceso],
				[FechaProceso] FROM #RecMedicamentos
		
		UPDATE GMD SET GMD.FechaUltimoProceso = GETDATE()
		FROM GestanteMedicamentoDetalle GMD INNER JOIN #RecMedicamentos RM ON 
		GMD.GestanteMedicamentoDetalleId = RM.GestanteMedicamentoDetalleId

		IF OBJECT_ID('tempdb..#RecMedicamentos') IS NOT NULL
			DROP TABLE #RecMedicamentos
	END
SELECT CAST(1 AS INT) AS Resultado
END

GO

ALTER PROCEDURE [dbo].[sproc_InsertEnviarMensajesEducacionales]
AS
BEGIN
SET NOCOUNT ON

DECLARE @HoraActual INT, @DiaActual INT, @DiaSemana INT
DECLARE @FechaUltimaEjecucion datetime

SELECT @HoraActual = DATEPART(hh, GETDATE())
SELECT  @FechaUltimaEjecucion = MAX(RP.FechaUltimaEjecucion) 
FROM  RegistroProceso RP
WHERE RP.IdProceso = 2 -- IdProceso para sproc_InsertEnviarMensajesEducacionales

IF (@HoraActual >= 8 And @HoraActual <= 17) And (DATEDIFF(mi, @FechaUltimaEjecucion, getdate()) >= 15 Or (@FechaUltimaEjecucion IS NULL)) 
	BEGIN
		SELECT @DiaActual = DATEPART(WEEKDAY, GETDATE())
		SELECT @DiaSemana = CASE 
								WHEN @DiaActual = 1 THEN 7 
								WHEN @DiaActual = 2 THEN 1
								WHEN @DiaActual = 3 THEN 2
								WHEN @DiaActual = 4 THEN 3
								WHEN @DiaActual = 5 THEN 4
								WHEN @DiaActual = 6 THEN 5
								WHEN @DiaActual = 7 THEN 6
							END

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

USE DB_Telemonitoreo
GO

IF ((SELECT Count(*) FROM [TipoObjeto] WHERE [IdTipoObjeto] = 30 AND [Descripcion] = 'Listado de Usuarios') = 0)
 INSERT INTO TipoObjeto(Descripcion) VALUES('Listado de Usuarios')
GO 

USE [DB_Telemonitoreo]
GO
/****** Object:  StoredProcedure [dbo].[sproc_InsertRecordatorioCitas]    Script Date: 03/17/2016 00:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[sproc_InsertRecordatorioCitas]
AS
BEGIN
SET NOCOUNT ON

Declare @FechaUltimaEjecucion datetime
Select @FechaUltimaEjecucion = MAX(RP.FechaUltimaEjecucion) 
From RegistroProceso RP
Where RP.IdProceso = 1 -- IdProceso para sproc_InsertRecordatorioCitas

IF (DATEDIFF(mi, @FechaUltimaEjecucion, getdate()) >= 15 Or (@FechaUltimaEjecucion IS NULL)) 
	BEGIN
		-- Notificar citas dentro de ultimas 24 horas
		SELECT	G.GestanteTelefono AS [NumeroMovil],
				'Rec. Cita'        AS [TipoMensaje],
				'Recuerde asistir a su cita en: ' + E.Descripcion + 
				' Fecha: ' + CONVERT(varchar(10), GC.FechaCita, 103) + ' Hora: ' + GC.HoraCita  AS [CuerpoMensaje],
				GETDATE()          AS [FechaCreacion],
				0  AS [Procesado],
				'' AS [ResultadoProceso],
				0  AS [ErrorProceso],
				NULL AS [FechaProceso]
		INTO #RecCitas24
		FROM GestanteCita GC (NOLOCK) INNER JOIN Gestante G (NOLOCK) ON GC.GestanteKey = G.GestanteKey
										INNER JOIN Establecimiento E (NOLOCK) ON GC.EstablecimientoId = E.EstablecimientoId
		WHERE G.Eliminado  = 0 AND G.GestanteTelefono IS NOT NULL AND 
			  GC.Eliminado = 0 AND 
			  E.EstadoId = 1  AND
			  DATEDIFF(mi, 
						GETDATE(), 
						CONVERT(datetime,CONVERT(varchar(10), GC.FechaCita, 120) + ' ' + LEFT(GC.HoraCita,2) + ':' + RIGHT(GC.HoraCita,2) + ':00', 120)
					  ) >= 1425 AND
			  DATEDIFF(mi, 
						GETDATE(), 
						CONVERT(datetime,CONVERT(varchar(10), GC.FechaCita, 120) + ' ' + LEFT(GC.HoraCita,2) + ':' + RIGHT(GC.HoraCita,2) + ':00', 120)
					  ) < 1440

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
		SELECT	[NumeroMovil],
				[TipoMensaje],
				[CuerpoMensaje],
				[FechaCreacion],
				[Procesado],
				[ResultadoProceso],
				[ErrorProceso],
				[FechaProceso] 
		FROM #RecCitas24

		IF OBJECT_ID('tempdb..##RecCitas24') IS NOT NULL
			DROP TABLE #RecCitas24

		Insert Into RegistroProceso(IdProceso, FechaUltimaEjecucion) 
		Values (1, GETDATE())

		Select cast(1 as int) as Resultado
	END
ELSE
	Select cast(0 as int) as Resultado
END
GO

USE [DB_Telemonitoreo]
GO

/****** Object:  StoredProcedure [dbo].[sproc_ReporteGestanteCita]    Script Date: 03/18/2016 09:55:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReporteGestanteCita]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_ReporteGestanteCita]
GO

/****** Object:  StoredProcedure [dbo].[sproc_Rpt_Gestantes_Participantes]    Script Date: 03/18/2016 09:55:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_Rpt_Gestantes_Participantes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_Rpt_Gestantes_Participantes]
GO
