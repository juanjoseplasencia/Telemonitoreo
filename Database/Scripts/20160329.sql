USE [DB_Telemonitoreo]
GO
/****** Object:  StoredProcedure [dbo].[sproc_InsertRecordatorioReporteMonitoreo]    Script Date: 03/31/2016 01:22:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[sproc_InsertRecordatorioReporteMonitoreo]
AS
BEGIN
SET NOCOUNT ON

Declare @FechaUltimaEjecucion datetime
Select @FechaUltimaEjecucion = MAX(RP.FechaUltimaEjecucion) 
From RegistroProceso RP
Where RP.IdProceso = 3 -- IdProceso para sproc_InsertRecordatorioReporteMonitoreo

IF ((DatePart(hh,GETDATE()) = 12 AND DATEDIFF(mi, @FechaUltimaEjecucion, getdate()) >= 1260)  Or 
    (DatePart(hh,GETDATE()) = 15 AND DATEDIFF(mi, @FechaUltimaEjecucion, getdate()) >= 180) Or
    @FechaUltimaEjecucion IS NULL) 
	BEGIN
	-- Notificar no hay envios hasta mediodia
	IF (DatePart(hh,GETDATE()) = 12)
		BEGIN
			SELECT	G.GestanteTelefono AS [NumeroMovil],
					'Rec. Reporte Monitoreo' AS [TipoMensaje],
					'Estimada participante Ud no ha enviado su reporte clínico de la mañana, envíelo a la brevedad posible.' AS [CuerpoMensaje],
					GETDATE() AS [FechaCreacion],
					0  AS [Procesado],
					'' AS [ResultadoProceso],
					0  AS [ErrorProceso],
					NULL AS [FechaProceso]
			INTO #RecMonitoreo12			
			FROM Gestante G (NOLOCK) LEFT OUTER JOIN  GestanteMonitoreo GM (NOLOCK) ON G.GestanteKey = GM.GestanteKey
			AND  CONVERT(varchar(10),GM.FechaRegistro,120) = CONVERT(varchar(10),GETDATE(),120)
			WHERE G.Eliminado  = 0 AND G.GestanteTelefono IS NOT NULL AND GM.GestanteKey IS NULL
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
			FROM #RecMonitoreo12
		END 
	-- Notificar no hay envios hasta 3pm
	IF (DatePart(hh,GETDATE()) = 15)
		BEGIN
			SELECT	AspNetU.PhoneNumber AS [NumeroMovil],
					'Rec. Reporte Monitoreo' AS [TipoMensaje],
					'La gestante DNI: ' + G.GestanteNroDocumento +' no ha enviado su reporte clínico el día de hoy, activar protocolo.' AS [CuerpoMensaje],
					GETDATE() AS [FechaCreacion],
					0  AS [Procesado],
					'' AS [ResultadoProceso],
					0  AS [ErrorProceso],
					NULL AS [FechaProceso]
			INTO #RecMonitoreo15			
			FROM Gestante G (NOLOCK) LEFT OUTER JOIN GestanteMonitoreo GM (NOLOCK) ON G.GestanteKey = GM.GestanteKey
			AND  CONVERT(varchar(10),GM.FechaRegistro,120) = CONVERT(varchar(10),GETDATE(),120)
			INNER JOIN Usuario U ON G.EstablecimientoNotificacionId = U.EstablecimientoId AND U.RecibeAlertas = 1 AND U.Eliminado = 0 AND U.EstadoId = 1
			INNER JOIN AspNetUsers AspNetU ON U.Id = AspNetU.Id AND AspNetU.PhoneNumber IS NOT NULL AND LEN(AspNetU.PhoneNumber) > 0
			INNER JOIN AspNetUserRoles AspNetUR ON AspNetU.Id = ASpNetUR.UserId
			INNER JOIN Rol R ON AspNetUR.RoleId = R.AspNetRoleId AND R.RolId = 2
			WHERE G.Eliminado  = 0 AND G.GestanteTelefono IS NOT NULL AND GM.GestanteKey IS NULL
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
			FROM #RecMonitoreo15
		END 

	IF OBJECT_ID('tempdb..##RecMonitoreo15') IS NOT NULL
		DROP TABLE #RecMonitoreo15

	IF OBJECT_ID('tempdb..##RecMonitoreo15') IS NOT NULL
		DROP TABLE #RecMonitoreo15

	Insert Into RegistroProceso(IdProceso, FechaUltimaEjecucion) 
	Values (3, GETDATE())

	Select cast(1 as int) as Resultado
	END
ELSE
	Select cast(0 as int) as Resultado
END
