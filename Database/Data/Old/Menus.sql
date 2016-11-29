Use [DB_Telemonitoreo]
GO

ALTER TABLE [dbo].[RolMenu] DROP CONSTRAINT [FK_RolMenu_Menu]
GO

TRUNCATE TABLE [dbo].[Menu]
GO


Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Administración','Administración',0,NULL,0,1,1,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Gestantes','Gestantes',0,NULL,0,1,2,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Mensajes Educacionales','Mensajes Educacionales',0,NULL,0,1,3,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Reportes','Reportes',0,NULL,0,1,4,NULL,NULL,NULL)
GO

Declare @MenuId int;
Select @MenuId = MenuId From Menu Where MenuPadre IS NULL And Nombre = 'Administración';

Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Administración de Establecimientos','Administración de Establecimientos',1,@MenuId,0,1,1,'/Establecimiento/Index','Establecimiento','Index')
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Administración de Roles','Administración de Roles',1,@MenuId,0,1,2,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Bitácora de Usuario Actual','Bitácora de Usuario Actual',1,@MenuId,0,1,3,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Bitácora de Sesiones','Bitácora de Sesiones',1,@MenuId,0,1,4,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Cambiar Contraseña','Cambiar Contraseña',1,@MenuId,0,1,5,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Editar Datos de Usuario','Editar Datos de Usuario',1,@MenuId,0,1,6,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Usuarios','Usuarios',1,@MenuId,0,1,7,NULL,NULL,NULL)
GO

Declare @MenuId0 int;
Declare @MenuId1 int;

Select @MenuId0 = MenuId From Menu Where MenuPadre IS NULL And Nombre = 'Administración';
Select @MenuId1 = MenuId From Menu Where MenuPadre = @MenuId0 And Nombre = 'Usuarios';

Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Crear Usuario','Crear Usuario',2,@MenuId1,0,1,1,'/Account/Register','Account','Register')
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Listar Usuarios','Listar Usuarios',2,@MenuId1,0,1,2,'/Account/Index','Account','Index')
GO

Declare @MenuId int;
Select @MenuId = MenuId From Menu Where MenuPadre IS NULL And Nombre = 'Gestantes';

Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Creación de Gestantes','Creación de Gestantes',1,@MenuId,0,1,1,'/Gestante/Crear','Gestante','Crear')
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Listado de Gestantes','Listado de Gestantes',1,@MenuId,0,1,2,'/Gestante/Index','Gestante','Index')
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Asignación de Medicamentos','Asignación de Medicamentos',1,@MenuId,0,1,3,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Listado de Medicamentos Asignados','Listado de Medicamentos Asignados',1,@MenuId,0,1,4,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Programación de Citas','Programación de Citas',1,@MenuId,0,1,5,'/GestanteCita/Crear','GestanteCita','Crear')
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Listado de Citas','Listado de Citas',1,@MenuId,0,1,6,'/GestanteCita/Index','GestanteCita','Index')
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Listado de Reportes de Monitoreo','Listado de Reportes de Monitoreo',1,@MenuId,0,1,7,'/GestanteMonitoreo/Index','GestanteMonitoreo','Index')
GO

Declare @MenuId int;
Select @MenuId = MenuId From Menu Where MenuPadre IS NULL And Nombre = 'Mensajes Educacionales';

Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Creación de Mensajes','Creación de Mensajes',1,@MenuId,0,1,1,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Listado de Mensajes','Listado de Mensajes',1,@MenuId,0,1,2,NULL,NULL,NULL)
GO

Declare @MenuId int;
Select @MenuId = MenuId From Menu Where MenuPadre IS NULL And Nombre = 'Reportes';

Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Gestantes Participantes','Gestantes Participantes',1,@MenuId,0,1,1,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Procedencia de Gestantes','Procedencia de Gestantes',1,@MenuId,0,1,2,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Evolución de Gestantes','Evolución de Gestantes',1,@MenuId,0,1,3,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Evolución por Gestante','Evolución por Gestante',1,@MenuId,0,1,4,NULL,NULL,NULL)
Insert Into Menu(Nombre,Descripcion, Nivel, MenuPadre, EsBoton, EstadoId, Orden, Url, Controlador, Accion) Values ('Medicación por Gestante','Medicación por Gestante',1,@MenuId,0,1,5,NULL,NULL,NULL)
GO


ALTER TABLE [dbo].[RolMenu]  WITH CHECK ADD  CONSTRAINT [FK_RolMenu_Menu] FOREIGN KEY([Menu_MenuId])
REFERENCES [dbo].[Menu] ([MenuId])
GO

ALTER TABLE [dbo].[RolMenu] CHECK CONSTRAINT [FK_RolMenu_Menu]
GO
