USE DB_Telemonitoreo
GO

DECLARE @IdMenu INT

INSERT INTO [dbo].[Menu]
           ([Nombre], [Descripcion], [Nivel], [MenuPadre]
           ,[EsBoton], [MenuImagen], [EstadoId], [Url]
           ,[Orden], [Controlador], [Accion])
     VALUES('Resumen por Gestante', 'Resumen por Gestante', 1, 4
           ,0, NULL, 1, '/Report/ReportResumenPorGestante'
           ,6, 'Report', 'ReportResumenPorGestante')

SELECT @IdMenu = @@IDENTITY

INSERT INTO [dbo].[RolMenu]
           ([Menu_MenuId], [Rol_RolId])
     VALUES(@IdMenu, 1)
     
INSERT INTO [dbo].[RolMenu]
           ([Menu_MenuId], [Rol_RolId])
     VALUES(@IdMenu, 2)
     
INSERT INTO [dbo].[RolMenu]
           ([Menu_MenuId], [Rol_RolId])
     VALUES(@IdMenu, 3)
     
INSERT INTO [dbo].[RolMenu]
           ([Menu_MenuId], [Rol_RolId])
     VALUES(@IdMenu, 4)

GO

INSERT INTO [dbo].[TipoObjeto]
           ([Descripcion])
     VALUES('Reporte Resumen de Gestante')
GO
