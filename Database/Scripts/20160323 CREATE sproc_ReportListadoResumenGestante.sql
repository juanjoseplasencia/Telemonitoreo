USE DB_Telemonitoreo
GO

--EXEC [sproc_ReportListadoResumenGestante]
CREATE PROCEDURE [dbo].[sproc_ReportListadoResumenGestante]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	G.Gestantekey 
			,G.GestanteId
			,G. GestanteNroDocumento
			,DATEDIFF(yy, G.FechaNacimiento, GETDATE()) - CASE WHEN (MONTH(G.FechaNacimiento) > MONTH(GETDATE())) OR (MONTH(G.FechaNacimiento) = MONTH(GETDATE()) AND DAY(G.FechaNacimiento) > DAY(GETDATE())) THEN 1 ELSE 0 END AS Age 
			,G.FechaCreacion
			,E.Descripcion AS Establecimiento
			,PresionArterial = CONVERT(CHAR(3), G.PresionSistolicaBase) + ' / ' + CONVERT (CHAR(3),G.PresionDiastolicaBase)
			,G.FechaProbableParto
			,DXI.Descripcion AS DiagnosticoIngreso
			,DXE.Descripcion AS DiagnosticoEgreso
			,G.EstablecimientoId
			,G.GestanteDireccion
			,G.GestanteTelefono			
			,Cita = (SELECT TOP 1 'Cita: ' + CONVERT(VARCHAR(10),GC.FechaCita,103) + ' - Establecimiento: ' + ES.Descripcion FROM GestanteCita GC
			INNER JOIN Establecimiento ES ON GC.EstablecimientoId = ES.EstablecimientoId WHERE GC.GestanteKey = G.GestanteKey AND GC.Eliminado = 0 ORDER BY GC.FechaCita DESC)
			,GM.FechaRegistro AS FechaRegistro
			,GM.PresionSistolica
			,GM.PresionDiastolica
			,GM.Proteinuria
			,GM.MovimientosFetales
			,'' AS Fecha
			,'' AS NombreMedico
			,'' AS Medicamento
			,'' AS Dias
			,'' AS Cantidad
			,'' AS FechaFin
	FROM	Gestante G (NOLOCK)
			INNER JOIN Establecimiento E (NOLOCK) ON E.EstablecimientoId = G.EstablecimientoId
			LEFT JOIN Diagnostico DXI (NOLOCK) ON G.DiagnosticoIngreso = DXI.Id
			LEFT JOIN Diagnostico DXE (NOLOCK) ON G.DiagnosticoEgreso = DXE.Id
			INNER JOIN GestanteMonitoreo GM (NOLOCK) ON GM.GestanteKey = G.GestanteKey
			WHERE G.Eliminado = 0
	UNION
	SELECT	G.Gestantekey 
			,G.GestanteId
			,G. GestanteNroDocumento
			,DATEDIFF(yy, G.FechaNacimiento, GETDATE()) - CASE WHEN (MONTH(G.FechaNacimiento) > MONTH(GETDATE())) OR (MONTH(G.FechaNacimiento) = MONTH(GETDATE()) AND DAY(G.FechaNacimiento) > DAY(GETDATE())) THEN 1 ELSE 0 END AS Age 
			,G.FechaCreacion
			,E.Descripcion AS Establecimiento
			,PresionArterial = CONVERT(CHAR(3), G.PresionSistolicaBase) + ' / ' + CONVERT (CHAR(3),G.PresionDiastolicaBase)
			,G.FechaProbableParto
			,DXI.Descripcion AS DiagnosticoIngreso
			,DXE.Descripcion AS DiagnosticoEgreso
			,G.EstablecimientoId
			,G.GestanteDireccion
			,G.GestanteTelefono			
			,Cita = (SELECT TOP 1 'Cita: ' + CONVERT(VARCHAR(10),GC.FechaCita,103) + ' - Establecimiento: ' + ES.Descripcion FROM GestanteCita GC
			INNER JOIN Establecimiento ES ON GC.EstablecimientoId = ES.EstablecimientoId WHERE GC.GestanteKey = G.GestanteKey AND GC.Eliminado = 0 ORDER BY GC.FechaCita DESC)
			,'' AS FechaRegistro
			,'' AS PresionSistolica
			,'' AS PresionDiastolica
			,'' AS Proteinuria
			,'' AS MovimientosFetales
			,GM.Fecha
			,GM.NombreMedico
			,M.Descripcion + ' ' + GMD.Dosis AS Medicamento
			,GMD.Dias
			,GMD.Cantidad
			,DATEADD(dd, GMD.Dias, GM.Fecha) AS FechaFin
	FROM	Gestante G 
			INNER JOIN Establecimiento E (NOLOCK) ON E.EstablecimientoId = G.EstablecimientoId
			LEFT JOIN Diagnostico DXI (NOLOCK) ON G.DiagnosticoIngreso = DXI.Id
			LEFT JOIN Diagnostico DXE (NOLOCK) ON G.DiagnosticoEgreso = DXE.Id
			INNER JOIN GestanteMedicamento GM (NOLOCK) ON G.GestanteKey = GM.GestanteKey 
			INNER JOIN GestanteMedicamentoDetalle GMD (NOLOCK) ON GM.GestanteMedicamentoId = GMD.GestanteMedicamentoId 
			INNER JOIN Medicamento M (NOLOCK) ON GMD.MedicamentoId = M.MedicamentoId
			WHERE	G.[Eliminado] = 0 AND GM.Eliminado = 0
	ORDER BY Fecha
END
GO