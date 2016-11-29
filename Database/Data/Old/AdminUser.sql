Use [DB_Telemonitoreo]
Go

INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[Email]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[UserName])
     VALUES
           (
           newid(),
           'admin@telemonitoreo.pe',
           0,
           'AEMGpZXPy17V3iDJV/Ywf1/YecoweyEpDV7ayDjX5jtCJg/tkz1DVO4e4xJHG+Glnw==',
           'fb9cc874-2669-42cd-8803-d1b32288e043',
           '997641130',
           0,
           0,
           1,
           0,
           'admin'
           )
Go

Declare @UserId nvarchar(128);
Select @UserId = Id From AspNetUsers Where UserName = 'admin'

INSERT INTO [dbo].[Usuario]
           ([Id]
           ,[UsuarioDireccion]
		   ,[Nombres]
		   ,APaterno
		   ,AMaterno
		   ,EstadoId
		   )
     VALUES
           (
           @UserId
           ,'DIRECCION ADMIN'
		   ,'Administrador'
		   ,'General'
		   ,'Telemonitoreo'
		   ,1
           )
Go

Declare @UserId nvarchar(128);
Declare @RoleId nvarchar(128);

Select @UserId = Id From AspNetUsers Where UserName = 'admin'
Select @RoleId = Id From AspNetRoles where Name = 'Administrador'  

INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           ( 
           @UserId,
           @RoleId
           )
Go
