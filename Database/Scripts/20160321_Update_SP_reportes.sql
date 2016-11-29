USE [DB_Telemonitoreo]
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReportEvolucionGestante]    Script Date: 03/21/2016 11:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
			,G.[EstablecimientoId]
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
			G.EstablecimientoId,
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
			,G.EstablecimientoId
			FROM	Gestante G INNER JOIN Establecimiento E ON G.EstablecimientoId = E.EstablecimientoId
					INNER JOIN GestanteMonitoreo GM ON GM.GestanteKey = G.GestanteKey
					INNER JOIN Ubigeo DEP ON LEFT(G.RegionId, 2) = LEFT(DEP.CodUbigeo,2) AND dep.CodDist = '00' AND dep.CodProv = '00'
			WHERE	G.Eliminado = 0
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
			,G.EstablecimientoId
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

ALTER PROCEDURE [dbo].[sproc_ReportMedicacionGestante]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	G.GestanteNroDocumento
			,G.Nombres
			,G.APaterno
			,G.AMaterno
			,G.EstablecimientoId
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
