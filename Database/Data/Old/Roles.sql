Use [DB_Telemonitoreo]
Go

Insert Into AspNetRoles Values (newid(), 'Administrador')
Declare @AspNetRoleId nvarchar(128);
Select @AspNetRoleId = Id From AspNetRoles Where Name = 'Administrador';
Insert Into Rol(Nombre, EstadoId, AspNetRoleId) Values ('Administrador', 1, @AspNetRoleId)
Go

Insert Into AspNetRoles Values (newid(), 'Personal de salud')
Declare @AspNetRoleId nvarchar(128);
Select @AspNetRoleId = Id From AspNetRoles Where Name = 'Personal de salud';
Insert Into Rol(Nombre, EstadoId, AspNetRoleId) Values ('Personal de salud', 1, @AspNetRoleId)
Go

Insert Into AspNetRoles Values (newid(), 'Analista')
Declare @AspNetRoleId nvarchar(128);
Select @AspNetRoleId = Id From AspNetRoles Where Name = 'Analista';
Insert Into Rol(Nombre, EstadoId, AspNetRoleId) Values ('Analista', 1, @AspNetRoleId)
Go

Insert Into AspNetRoles Values (newid(), 'Gestante')
Declare @AspNetRoleId nvarchar(128);
Select @AspNetRoleId = Id From AspNetRoles Where Name = 'Gestante';
Insert Into Rol(Nombre, EstadoId, AspNetRoleId) Values ('Gestante', 1, @AspNetRoleId)
Go
