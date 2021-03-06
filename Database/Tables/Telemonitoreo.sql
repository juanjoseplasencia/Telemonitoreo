USE [DB_Telemonitoreo]
GO
/****** Object:  Trigger [TR_AFT_IU_Cambios_Gestante]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TR_AFT_IU_Cambios_Gestante]'))
DROP TRIGGER [dbo].[TR_AFT_IU_Cambios_Gestante]
GO
/****** Object:  Trigger [TR_AFT_IU_Cambios_GestanteCita]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TR_AFT_IU_Cambios_GestanteCita]'))
DROP TRIGGER [dbo].[TR_AFT_IU_Cambios_GestanteCita]
GO
/****** Object:  Trigger [TR_AFT_IU_Cambios_Usuario]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TR_AFT_IU_Cambios_Usuario]'))
DROP TRIGGER [dbo].[TR_AFT_IU_Cambios_Usuario]
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Gestante_FechaUltimaRegla]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] DROP CONSTRAINT [CK_Gestante_FechaUltimaRegla]
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Gestante_FechaProbableParto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] DROP CONSTRAINT [CK_Gestante_FechaProbableParto]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Estado]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario]'))
ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_Usuario_Estado]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_AspNetUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario]'))
ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_Usuario_AspNetUsers]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EstablecimientoUsuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario]'))
ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_EstablecimientoUsuario]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolMenu_Rol]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolMenu]'))
ALTER TABLE [dbo].[RolMenu] DROP CONSTRAINT [FK_RolMenu_Rol]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolMenu_Menu]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolMenu]'))
ALTER TABLE [dbo].[RolMenu] DROP CONSTRAINT [FK_RolMenu_Menu]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsuarioRegistroEvento]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegistroEvento]'))
ALTER TABLE [dbo].[RegistroEvento] DROP CONSTRAINT [FK_UsuarioRegistroEvento]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TipoObjetoRegistroEvento]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegistroEvento]'))
ALTER TABLE [dbo].[RegistroEvento] DROP CONSTRAINT [FK_TipoObjetoRegistroEvento]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TipoAccionRegistroEvento]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegistroEvento]'))
ALTER TABLE [dbo].[RegistroEvento] DROP CONSTRAINT [FK_TipoAccionRegistroEvento]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MensajeEducacional_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[MensajeEducacional]'))
ALTER TABLE [dbo].[MensajeEducacional] DROP CONSTRAINT [FK_MensajeEducacional_Usuario]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteGestanteMonitoreo]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMonitoreo]'))
ALTER TABLE [dbo].[GestanteMonitoreo] DROP CONSTRAINT [FK_GestanteGestanteMonitoreo]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MedicamentoGestanteMedicamentoDetalle]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMedicamentoDetalle]'))
ALTER TABLE [dbo].[GestanteMedicamentoDetalle] DROP CONSTRAINT [FK_MedicamentoGestanteMedicamentoDetalle]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteMedicamentoGestanteMedicamentoDetalle]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMedicamentoDetalle]'))
ALTER TABLE [dbo].[GestanteMedicamentoDetalle] DROP CONSTRAINT [FK_GestanteMedicamentoGestanteMedicamentoDetalle]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteMedicamentoEstablecimiento]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMedicamento]'))
ALTER TABLE [dbo].[GestanteMedicamento] DROP CONSTRAINT [FK_GestanteMedicamentoEstablecimiento]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteGestanteMedicamento]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMedicamento]'))
ALTER TABLE [dbo].[GestanteMedicamento] DROP CONSTRAINT [FK_GestanteGestanteMedicamento]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteGestanteCita]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteCita]'))
ALTER TABLE [dbo].[GestanteCita] DROP CONSTRAINT [FK_GestanteGestanteCita]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteCitaEstablecimiento]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteCita]'))
ALTER TABLE [dbo].[GestanteCita] DROP CONSTRAINT [FK_GestanteCitaEstablecimiento]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_Region]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] DROP CONSTRAINT [FK_Gestante_Region]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_Provincia]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] DROP CONSTRAINT [FK_Gestante_Provincia]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_Distrito]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] DROP CONSTRAINT [FK_Gestante_Distrito]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_DiagnosticoIntermedio2]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] DROP CONSTRAINT [FK_Gestante_DiagnosticoIntermedio2]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_DiagnosticoIntermedio1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] DROP CONSTRAINT [FK_Gestante_DiagnosticoIntermedio1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_DiagnosticoIngreso]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] DROP CONSTRAINT [FK_Gestante_DiagnosticoIngreso]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_DiagnosticoEgreso]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] DROP CONSTRAINT [FK_Gestante_DiagnosticoEgreso]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EstablecimientoNotificacionGestante]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] DROP CONSTRAINT [FK_EstablecimientoNotificacionGestante]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EstablecimientoGestante]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] DROP CONSTRAINT [FK_EstablecimientoGestante]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Establecimiento_Estado]') AND parent_object_id = OBJECT_ID(N'[dbo].[Establecimiento]'))
ALTER TABLE [dbo].[Establecimiento] DROP CONSTRAINT [FK_Establecimiento_Estado]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContenidoMensajeEducacional_MensajeEducacional]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContenidoMensajeEducacional]'))
ALTER TABLE [dbo].[ContenidoMensajeEducacional] DROP CONSTRAINT [FK_ContenidoMensajeEducacional_MensajeEducacional]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
/****** Object:  Index [IX_UbigeoNombre]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Ubigeo]') AND name = N'IX_UbigeoNombre')
DROP INDEX [IX_UbigeoNombre] ON [dbo].[Ubigeo]
GO
/****** Object:  Index [IX_Ubigeo]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Ubigeo]') AND name = N'IX_Ubigeo')
DROP INDEX [IX_Ubigeo] ON [dbo].[Ubigeo]
GO
/****** Object:  Index [IX_FK_RolMenu_Rol]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RolMenu]') AND name = N'IX_FK_RolMenu_Rol')
DROP INDEX [IX_FK_RolMenu_Rol] ON [dbo].[RolMenu]
GO
/****** Object:  Index [IX_FK_UsuarioRegistroEvento]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RegistroEvento]') AND name = N'IX_FK_UsuarioRegistroEvento')
DROP INDEX [IX_FK_UsuarioRegistroEvento] ON [dbo].[RegistroEvento]
GO
/****** Object:  Index [IX_FK_TipoObjetoRegistroEvento]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RegistroEvento]') AND name = N'IX_FK_TipoObjetoRegistroEvento')
DROP INDEX [IX_FK_TipoObjetoRegistroEvento] ON [dbo].[RegistroEvento]
GO
/****** Object:  Index [IX_FK_TipoAccionRegistroEvento]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RegistroEvento]') AND name = N'IX_FK_TipoAccionRegistroEvento')
DROP INDEX [IX_FK_TipoAccionRegistroEvento] ON [dbo].[RegistroEvento]
GO
/****** Object:  Index [IX_FK_GestanteGestanteMonitoreo]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GestanteMonitoreo]') AND name = N'IX_FK_GestanteGestanteMonitoreo')
DROP INDEX [IX_FK_GestanteGestanteMonitoreo] ON [dbo].[GestanteMonitoreo]
GO
/****** Object:  Index [IX_Establecimiento]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Establecimiento]') AND name = N'IX_Establecimiento')
DROP INDEX [IX_Establecimiento] ON [dbo].[Establecimiento]
GO
/****** Object:  Index [UserNameIndex]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND name = N'UserNameIndex')
DROP INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
GO
/****** Object:  Index [IX_UserId]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND name = N'IX_UserId')
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
GO
/****** Object:  Index [IX_RoleId]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND name = N'IX_RoleId')
DROP INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
GO
/****** Object:  Index [IX_UserId]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND name = N'IX_UserId')
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
GO
/****** Object:  Index [IX_UserId]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND name = N'IX_UserId')
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND name = N'RoleNameIndex')
DROP INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[Usuario_Historia]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario_Historia]') AND type in (N'U'))
DROP TABLE [dbo].[Usuario_Historia]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario]') AND type in (N'U'))
DROP TABLE [dbo].[Usuario]
GO
/****** Object:  Table [dbo].[Ubigeo]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ubigeo]') AND type in (N'U'))
DROP TABLE [dbo].[Ubigeo]
GO
/****** Object:  Table [dbo].[TipoObjeto]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TipoObjeto]') AND type in (N'U'))
DROP TABLE [dbo].[TipoObjeto]
GO
/****** Object:  Table [dbo].[TipoAccion]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TipoAccion]') AND type in (N'U'))
DROP TABLE [dbo].[TipoAccion]
GO
/****** Object:  Table [dbo].[SmsQueue]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmsQueue]') AND type in (N'U'))
DROP TABLE [dbo].[SmsQueue]
GO
/****** Object:  Table [dbo].[RolMenu]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolMenu]') AND type in (N'U'))
DROP TABLE [dbo].[RolMenu]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rol]') AND type in (N'U'))
DROP TABLE [dbo].[Rol]
GO
/****** Object:  Table [dbo].[RegistroProceso]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RegistroProceso]') AND type in (N'U'))
DROP TABLE [dbo].[RegistroProceso]
GO
/****** Object:  Table [dbo].[RegistroEvento]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RegistroEvento]') AND type in (N'U'))
DROP TABLE [dbo].[RegistroEvento]
GO
/****** Object:  Table [dbo].[ProcesoMensajeEducacional]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProcesoMensajeEducacional]') AND type in (N'U'))
DROP TABLE [dbo].[ProcesoMensajeEducacional]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND type in (N'U'))
DROP TABLE [dbo].[Menu]
GO
/****** Object:  Table [dbo].[MensajeEducacional]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MensajeEducacional]') AND type in (N'U'))
DROP TABLE [dbo].[MensajeEducacional]
GO
/****** Object:  Table [dbo].[Medicamento]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Medicamento]') AND type in (N'U'))
DROP TABLE [dbo].[Medicamento]
GO
/****** Object:  Table [dbo].[GestanteMonitoreo]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GestanteMonitoreo]') AND type in (N'U'))
DROP TABLE [dbo].[GestanteMonitoreo]
GO
/****** Object:  Table [dbo].[GestanteMedicamentoDetalle]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GestanteMedicamentoDetalle]') AND type in (N'U'))
DROP TABLE [dbo].[GestanteMedicamentoDetalle]
GO
/****** Object:  Table [dbo].[GestanteMedicamento]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GestanteMedicamento]') AND type in (N'U'))
DROP TABLE [dbo].[GestanteMedicamento]
GO
/****** Object:  Table [dbo].[GestanteCita_Historia]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GestanteCita_Historia]') AND type in (N'U'))
DROP TABLE [dbo].[GestanteCita_Historia]
GO
/****** Object:  Table [dbo].[GestanteCita]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GestanteCita]') AND type in (N'U'))
DROP TABLE [dbo].[GestanteCita]
GO
/****** Object:  Table [dbo].[Gestante_Historia]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Gestante_Historia]') AND type in (N'U'))
DROP TABLE [dbo].[Gestante_Historia]
GO
/****** Object:  Table [dbo].[Gestante]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Gestante]') AND type in (N'U'))
DROP TABLE [dbo].[Gestante]
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Estado]') AND type in (N'U'))
DROP TABLE [dbo].[Estado]
GO
/****** Object:  Table [dbo].[Establecimiento]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Establecimiento]') AND type in (N'U'))
DROP TABLE [dbo].[Establecimiento]
GO
/****** Object:  Table [dbo].[Diagnostico]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diagnostico]') AND type in (N'U'))
DROP TABLE [dbo].[Diagnostico]
GO
/****** Object:  Table [dbo].[ContenidoMensajeEducacional]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContenidoMensajeEducacional]') AND type in (N'U'))
DROP TABLE [dbo].[ContenidoMensajeEducacional]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
DROP TABLE [dbo].[__MigrationHistory]
GO
/****** Object:  StoredProcedure [dbo].[sproc_UpdateEstFromRenaes]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_UpdateEstFromRenaes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_UpdateEstFromRenaes]
GO
/****** Object:  StoredProcedure [dbo].[sproc_Rpt_Gestantes_Participantes]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_Rpt_Gestantes_Participantes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_Rpt_Gestantes_Participantes]
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReportProcedenciaGestantes]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReportProcedenciaGestantes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_ReportProcedenciaGestantes]
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReportMedicacionGestante]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReportMedicacionGestante]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_ReportMedicacionGestante]
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReportListadoEvolucionGestantes]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReportListadoEvolucionGestantes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_ReportListadoEvolucionGestantes]
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReportGestantesParticipantes]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReportGestantesParticipantes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_ReportGestantesParticipantes]
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReportEvolucionGestante]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReportEvolucionGestante]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_ReportEvolucionGestante]
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReporteGestanteCita]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReporteGestanteCita]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_ReporteGestanteCita]
GO
/****** Object:  StoredProcedure [dbo].[sproc_InsertRecordatorioMedicamentos]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_InsertRecordatorioMedicamentos]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_InsertRecordatorioMedicamentos]
GO
/****** Object:  StoredProcedure [dbo].[sproc_InsertRecordatorioCitas]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_InsertRecordatorioCitas]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_InsertRecordatorioCitas]
GO
/****** Object:  StoredProcedure [dbo].[sproc_InsertEnviarMensajesEducacionales]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_InsertEnviarMensajesEducacionales]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_InsertEnviarMensajesEducacionales]
GO
/****** Object:  StoredProcedure [dbo].[sproc_GetEstablecimientos]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_GetEstablecimientos]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_GetEstablecimientos]
GO
/****** Object:  StoredProcedure [dbo].[sproc_GetAllMenuOptions]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_GetAllMenuOptions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_GetAllMenuOptions]
GO
/****** Object:  StoredProcedure [dbo].[sproc_AddUpdateGestanteMonitoreo]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_AddUpdateGestanteMonitoreo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_AddUpdateGestanteMonitoreo]
GO
/****** Object:  StoredProcedure [dbo].[sproc_AddUpdateGestanteCita]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_AddUpdateGestanteCita]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_AddUpdateGestanteCita]
GO
/****** Object:  StoredProcedure [dbo].[sproc_AddUpdateGestante]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_AddUpdateGestante]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sproc_AddUpdateGestante]
GO
/****** Object:  UserDefinedTableType [dbo].[EstRenaes]    Script Date: 11/11/2015 00:44:00 ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'EstRenaes' AND ss.name = N'dbo')
DROP TYPE [dbo].[EstRenaes]
GO
/****** Object:  UserDefinedTableType [dbo].[EstRenaes]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'EstRenaes' AND ss.name = N'dbo')
CREATE TYPE [dbo].[EstRenaes] AS TABLE(
	[Nombre] [nvarchar](255) NULL,
	[Direccion] [nvarchar](255) NULL,
	[UsuarioActualizacion] [int] NOT NULL,
	[CodUbigeo] [nvarchar](25) NULL,
	[CodRenaes] [nvarchar](50) NULL
)
GO
/****** Object:  StoredProcedure [dbo].[sproc_AddUpdateGestante]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_AddUpdateGestante]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Juan Plasencia
-- Create date: 2015-07-20
-- Description:	Crear nueva gestante en el sistema. Actualizar gestante existente
-- =============================================
CREATE PROCEDURE [dbo].[sproc_AddUpdateGestante]
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
				''1'' + @pGestanteNroDocumento + ''00'', 
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
				LEFT(@pDistritoId,4) + ''00'',
				LEFT(@pDistritoId,2) + ''0000'',
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
				GestanteId = ''1'' + @pGestanteNroDocumento + ''00'',
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
				ProvinciaId = LEFT(@pDistritoId,4) + ''00'',
				RegionId = LEFT(@pDistritoId,2) + ''0000'',
				UsuarioEditor = @pUsuarioEditor,
				HorarioMensaje = @pHorarioMensaje
			Where GestanteKey = @pGestanteKey
			Select @pGestanteKey as PKValue
		END
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_AddUpdateGestanteCita]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_AddUpdateGestanteCita]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Juan Plasencia
-- Create date: 2015-08-25
-- Description:	Crear nuevo recordatorio de cita de gestante en el sistema. Actualizar recordatorio existente
-- =============================================
CREATE PROCEDURE [dbo].[sproc_AddUpdateGestanteCita]
 @pMode smallint
,@pGestanteCitaId int
,@pGestanteKey int
,@pFechaCita datetime
,@pHoraCita varchar(4)
,@NombreMedico varchar(50)
,@EstablecimientoId int
,@pUsuarioEditor int

AS

BEGIN
	SET NOCOUNT ON;
	BEGIN
	IF (@pMode = 0)
		BEGIN
			Insert Into GestanteCita( 
				GestanteKey,
				FechaCita,
				HoraCita,
				NombreMedico,
				EstablecimientoId,
				FechaCreacion,
				UsuarioEditor
				) 
			Values ( 
				@pGestanteKey, 
				@pFechaCita,
				@pHoraCita,
				@NombreMedico,
				@EstablecimientoId,
				getdate(),
				@pUsuarioEditor
				) 
			Select cast(SCOPE_IDENTITY() as int) as PKValue
		END
	ELSE
		BEGIN
			Update GestanteCita 
			Set  
				GestanteKey = @pGestanteKey,
				FechaCita = @pFechaCita,
				HoraCita = @pHoraCita,
				NombreMedico = @NombreMedico,
				EstablecimientoId = @EstablecimientoId,
				UsuarioEditor = @pUsuarioEditor
			Where GestanteCitaId = @pGestanteCitaId
			Select @pGestanteCitaId as PKValue
		END
	END
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_AddUpdateGestanteMonitoreo]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_AddUpdateGestanteMonitoreo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Juan Plasencia
-- Create date: 2015-08-23
-- Description:	Crear nuevo reporte de monitoreo de gestante en el sistema.
-- =============================================
CREATE PROCEDURE [dbo].[sproc_AddUpdateGestanteMonitoreo]
 @pGestanteKey INT
,@pPresionSistolica INT
,@pPresionDiastolica INT
,@pProteinuria INT
,@pMovimientosFetales INT
,@pSignosAlarma VARCHAR(30)

AS

BEGIN
	SET NOCOUNT ON;
	BEGIN
		INSERT INTO GestanteMonitoreo( 
			GestanteKey,
			PresionSistolica,
			PresionDiastolica,
			Proteinuria,
			MovimientosFetales,
			SignosAlarma,
			FechaRegistro
			) 
		VALUES ( 
			@pGestanteKey, 
			@pPresionSistolica,
			@pPresionDiastolica,
			@pProteinuria,	
			@pMovimientosFetales,	
			@pSignosAlarma,
			GETDATE()
			) 
		SELECT CAST(SCOPE_IDENTITY() AS INT) AS PKValue
	END
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_GetAllMenuOptions]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_GetAllMenuOptions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Carlos Rafael
-- Create date: 2015-09-04
-- Description:	Get list of all menu options
-- =============================================
CREATE PROCEDURE [dbo].[sproc_GetAllMenuOptions] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT	M.MenuId,M.Nombre,M.Nivel,M.MenuPadre,M.EsBoton,M.Orden
	    , CONVERT(BIT, CASE WHEN RM1.Rol_RolId IS NULL THEN 0 ELSE 1 END) AS AccesoAdministrador
	    , CONVERT(BIT, CASE WHEN RM2.Rol_RolId IS NULL THEN 0 ELSE 1 END) AS AccesoPersonal
	    , CONVERT(BIT, CASE WHEN RM3.Rol_RolId IS NULL THEN 0 ELSE 1 END) AS AccesoAnalista
	    , CONVERT(BIT, CASE WHEN RM4.Rol_RolId IS NULL THEN 0 ELSE 1 END) AS AccesoGestante
	FROM      Menu M
	LEFT JOIN RolMenu RM1
	ON        M.MenuId = RM1.Menu_MenuId
	AND       RM1.Rol_RolId = 1
	LEFT JOIN RolMenu RM2
	ON        M.MenuId = RM2.Menu_MenuId
	AND       RM2.Rol_RolId = 2
	LEFT JOIN RolMenu RM3
	ON        M.MenuId = RM3.Menu_MenuId
	AND       RM3.Rol_RolId = 3
	LEFT JOIN RolMenu RM4
	ON        M.MenuId = RM4.Menu_MenuId
	AND       RM4.Rol_RolId = 4
	AND M.EstadoId = 1
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_GetEstablecimientos]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_GetEstablecimientos]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sproc_GetEstablecimientos] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	E.EstablecimientoId
			,E.Descripcion
			,E.Direccion
			,E.Renaes
			,E.EstadoId
			,ES.Descripcion AS Estado
			,U.Nombre AS Distrito
			,(SELECT TOP 1 U1.Nombre FROM Ubigeo U1 (NOLOCK) WHERE U1.CodUbigeo = U.CodDpto + U.CodProv + ''00'') AS Provincia
			,(SELECT TOP 1 U2.Nombre FROM Ubigeo U2 (NOLOCK) WHERE U2.CodUbigeo = U.CodDpto + ''0000'')  AS Region
	FROM	Establecimiento E (NOLOCK) LEFT JOIN Ubigeo U (NOLOCK) ON E.Ubigeo = U.CodUbigeo
			INNER JOIN Estado ES (NOLOCK) ON E.EstadoId = ES.Id
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_InsertEnviarMensajesEducacionales]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_InsertEnviarMensajesEducacionales]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sproc_InsertEnviarMensajesEducacionales]
AS
BEGIN
SET NOCOUNT ON

DECLARE @DiaActual INT, @DiaSemana INT
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
ME.SemanaEmbarazo = GS.SemanaEmbarazo AND CME.DiaSemana = @DiaSemana WHERE  
NOT EXISTS (SELECT * FROM ProcesoMensajeEducacional WHERE IdContenidoMensajeEducacional = CME.IdContenidoMensajeEducacional AND GestanteKey = GS.GestanteKey)

/*Inicio proceso mensaje*/
DECLARE @HoraActual INT 
SELECT @HoraActual = DATEPART(hh, GETDATE())

IF (@HoraActual) > 14
	BEGIN
		UPDATE PME SET PME.FechaHoraProceso = GETDATE(), PME.Procesado = 1, PME.ResultadoProceso = ''Fuera de Hora'' 
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
				''Mens. Educacional'' AS TipoMensaje,
				CME.Contenido AS CuerpoMensaje,
				GETDATE() AS [FechaCreacion],
				0 AS [Procesado],
				'''' AS [ResultadoProceso],
				0 AS [ErrorProceso],
				NULL AS [FechaProceso]
		FROM [ProcesoMensajeEducacional] PME INNER JOIN ContenidoMensajeEducacional CME ON PME.IdContenidoMensajeEducacional = CME.IdContenidoMensajeEducacional 
		INNER JOIN MensajeEducacional ME ON CME.IdMensajeEducacional = ME.IdMensajeEducacional INNER JOIN Gestante G ON PME.GestanteKey = G.GestanteKey WHERE 
		PME.Turno = 2 AND PME.Procesado = 0 AND CONVERT (date, PME.FechaEnvio) = CONVERT (date, GETDATE())
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
				''Mens. Educacional'' AS TipoMensaje,
				CME.Contenido AS CuerpoMensaje,
				GETDATE() AS [FechaCreacion],
				0 AS [Procesado],
				'''' AS [ResultadoProceso],
				0 AS [ErrorProceso],
				NULL AS [FechaProceso]
		FROM [ProcesoMensajeEducacional] PME INNER JOIN ContenidoMensajeEducacional CME ON PME.IdContenidoMensajeEducacional = CME.IdContenidoMensajeEducacional 
		INNER JOIN MensajeEducacional ME ON CME.IdMensajeEducacional = ME.IdMensajeEducacional INNER JOIN Gestante G ON PME.GestanteKey = G.GestanteKey WHERE 
		PME.Turno = 1 AND PME.Procesado = 0 AND CONVERT (date, PME.FechaEnvio) = CONVERT (date, GETDATE())
		
		UPDATE PME SET PME.FechaHoraProceso = GETDATE(), PME.Procesado = 1, PME.ResultadoProceso = ''Completado'' 
		FROM [ProcesoMensajeEducacional] PME INNER JOIN ContenidoMensajeEducacional CME ON PME.IdContenidoMensajeEducacional = CME.IdContenidoMensajeEducacional 
		INNER JOIN MensajeEducacional ME ON CME.IdMensajeEducacional = ME.IdMensajeEducacional INNER JOIN Gestante G ON PME.GestanteKey = G.GestanteKey WHERE 
		PME.Turno = 1 AND PME.Procesado = 0 AND CONVERT (date, PME.FechaEnvio) = CONVERT (date, GETDATE())
	END

SELECT CAST(1 AS INT) AS Resultado
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_InsertRecordatorioCitas]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_InsertRecordatorioCitas]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE  PROCEDURE [dbo].[sproc_InsertRecordatorioCitas]
AS
BEGIN
SET NOCOUNT ON

Declare @FechaUltimaEjecucion datetime
Select @FechaUltimaEjecucion = MAX(RP.FechaUltimaEjecucion) 
From RegistroProceso RP
Where RP.IdProceso = 1

IF (DATEDIFF(mi, @FechaUltimaEjecucion, getdate()) >= 15 Or (@FechaUltimaEjecucion IS NULL)) 
	BEGIN
		-- Notificar citas dentro de ultimas 24 horas
		SELECT	G.GestanteTelefono AS [NumeroMovil],
				''Rec. Cita''        AS [TipoMensaje],
				''Recuerde asistir a su cita en: '' + E.Descripcion + 
				'' Fecha: '' + CONVERT(varchar(10), GC.FechaCita, 103) + '' Hora: '' + GC.HoraCita  AS [CuerpoMensaje],
				GETDATE()          AS [FechaCreacion],
				0  AS [Procesado],
				'''' AS [ResultadoProceso],
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
						CONVERT(datetime,CONVERT(varchar(10), GC.FechaCita, 120) + '' '' + LEFT(GC.HoraCita,2) + '':'' + RIGHT(GC.HoraCita,2) + '':00'', 120)
					  ) >= 1425 AND
			  DATEDIFF(mi, 
						GETDATE(), 
						CONVERT(datetime,CONVERT(varchar(10), GC.FechaCita, 120) + '' '' + LEFT(GC.HoraCita,2) + '':'' + RIGHT(GC.HoraCita,2) + '':00'', 120)
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

		IF OBJECT_ID(''tempdb..##RecCitas24'') IS NOT NULL
			DROP TABLE #RecCitas24

		Insert Into RegistroProceso(IdProceso, FechaUltimaEjecucion) 
		Values (1, GETDATE())

		Select cast(1 as int) as Resultado
	END
ELSE
	Select cast(0 as int) as Resultado
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_InsertRecordatorioMedicamentos]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_InsertRecordatorioMedicamentos]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[sproc_InsertRecordatorioMedicamentos]
AS
BEGIN
SET NOCOUNT ON
DECLARE @hourTime INT
SELECT @hourTime = DATEPART(HOUR, GETUTCDATE())

IF (@hourTime = 2)
	BEGIN
		SELECT	G.GestanteTelefono AS [NumeroMovil],
				''Rec. Medicamento'' AS [TipoMensaje],
				''Recuerde su '' + M.Descripcion + '' a tomar: '' + GMD.Cantidad + '' al día.'' AS [CuerpoMensaje],
				GETDATE() AS [FechaCreacion],
				0 AS [Procesado],
				'''' AS [ResultadoProceso],
				0 AS [ErrorProceso],
				NULL AS [FechaProceso],
				GMD.GestanteMedicamentoDetalleId AS [GestanteMedicamentoDetalleId]
		INTO #RecMedicamentos
		FROM GestanteMedicamento GM (NOLOCK) INNER JOIN GestanteMedicamentoDetalle GMD (NOLOCK) ON 
		GM.GestanteMedicamentoId = GMD.GestanteMedicamentoId INNER JOIN Medicamento M (NOLOCK) ON 
		GMD.MedicamentoId = M.MedicamentoId INNER JOIN Gestante G (NOLOCK) ON GM.GestanteKey = G.GestanteKey
		WHERE G.Eliminado = 0 AND G.GestanteTelefono IS NOT NULL AND 
		(GMD.FechaUltimoProceso IS NULL OR REPLACE(CONVERT(VARCHAR, GMD.FechaUltimoProceso, 111), ''/'', '''') < REPLACE(CONVERT(VARCHAR, GETDATE(), 111), ''/'', '''')) AND
		REPLACE(CONVERT(VARCHAR, GETDATE(), 111), ''/'', '''') <= REPLACE(CONVERT(VARCHAR, DATEADD(dd,GMD.Dias,GM.Fecha), 111), ''/'', '''')

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

		IF OBJECT_ID(''tempdb..#RecMedicamentos'') IS NOT NULL
			DROP TABLE #RecMedicamentos
	END
SELECT CAST(1 AS INT) AS Resultado
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReporteGestanteCita]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReporteGestanteCita]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sproc_ReporteGestanteCita]
AS
BEGIN
	SELECT G.GestanteNroDocumento, G.FechaCreacion, GC.FechaCita, GC.EstablecimientoId  FROM 
	Gestante G INNER JOIN GestanteCita GC ON G.GestanteKey = GC.GestanteKey WHERE G.Eliminado = 0 AND GC.Eliminado = 0
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReportEvolucionGestante]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReportEvolucionGestante]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sproc_ReportEvolucionGestante]
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
	FROM	[GestanteMonitoreo] GM (NOLOCK) INNER JOIN [Gestante] G (NOLOCK) ON
			GM.[GestanteKey] = G.[GestanteKey]
	WHERE	G.[Eliminado] = 0
	ORDER BY	GM.[FechaRegistro]
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReportGestantesParticipantes]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReportGestantesParticipantes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sproc_ReportGestantesParticipantes]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	G.Gestantekey,
			G.GestanteId,
			G.GestanteNroDocumento,
			NroCitas = (SELECT COUNT( GestanteKey) FROM GestanteCita WHERE GestanteKey = g.GestanteKey),
			FechaCita = ''Cita: '' + CONVERT(VARCHAR(10),GC.FechaCita,103),
			Establecimiento = ''Establecimiento: '' + E.Descripcion  ,
			G.FechaCreacion,
			Dist.Nombre as Distrito,
			Prov.Nombre as Provincia,
			dep.Nombre as departamento,
			AsigMedic = (SELECT  COUNT(GestanteKey) FROM GestanteMedicamento where GestanteKey = g.GestanteKey),
			NULL AS FechaMed,
			'''' AS NombreMedico
	FROM	Gestante G INNER JOIN GestanteCita GC ON G.GestanteKey = GC.GestanteKey
			INNER JOIN Establecimiento E ON E.EstablecimientoId = G.EstablecimientoId  
			INNER JOIN Ubigeo Dist ON G.DistritoId = Dist.CodUbigeo
			INNER JOIN Ubigeo Prov ON LEFT (G.ProvinciaId,4 ) = LEFT(Prov.CodUbigeo, 4) AND Prov.CodDist = ''00''
			INNER JOIN Ubigeo DEP ON LEFT(G.RegionId, 2) = LEFT(DEP.CodUbigeo,2) AND DEP.CodDist = ''00'' AND DEP.CodProv = ''00''
			WHERE G.Eliminado = 0 AND GC.Eliminado = 0
	UNION
	SELECT	G.Gestantekey,
			G.GestanteId,
			G.GestanteNroDocumento,
			NroCitas = (SELECT COUNT( GestanteKey) FROM GestanteCita WHERE GestanteKey = g.GestanteKey),
			'''' AS FechaCita,
			'''' AS Establecimiento,
			G.FechaCreacion,
			Dist.Nombre as Distrito,
			Prov.Nombre as Provincia,
			dep.Nombre as departamento,
			AsigMedic = (SELECT  COUNT(GestanteKey) FROM GestanteMedicamento where GestanteKey = g.GestanteKey),
			''Fecha: '' + CONVERT(VARCHAR(10),GM.Fecha,103) AS FechaMed,
			''Médico: '' + GM.NombreMedico AS NombreMedico
	FROM	Gestante G
			INNER JOIN Establecimiento E ON E.EstablecimientoId = G.EstablecimientoId  
			INNER JOIN GestanteMedicamento GM ON G.GestanteKey = GM.GestanteKey
			INNER JOIN Ubigeo Dist ON G.DistritoId = Dist.CodUbigeo
			INNER JOIN Ubigeo Prov ON LEFT (G.ProvinciaId,4 ) = LEFT(Prov.CodUbigeo, 4) AND Prov.CodDist = ''00''
			INNER JOIN Ubigeo DEP ON LEFT(G.RegionId, 2) = LEFT(DEP.CodUbigeo,2) AND DEP.CodDist = ''00'' AND DEP.CodProv = ''00''
	WHERE G.Eliminado = 0
	ORDER BY G.GestanteNroDocumento, FechaCita
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReportListadoEvolucionGestantes]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReportListadoEvolucionGestantes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE  Procedure [dbo].[sproc_ReportListadoEvolucionGestantes]
AS
BEGIN
SET NOCOUNT ON;
	SELECT	G.Gestantekey 
			,G.GestanteId
			,G. GestanteNroDocumento
			,G.FechaCreacion
			,Dist.Nombre as Distrito
			,Prov.Nombre as Provincia
			,dep.Nombre as departamento
			,GM.GestanteKey as GestanteGM_KEY
			,GM.FechaRegistro AS Fecha
			,PresionArterial = CONVERT(CHAR(3), GM.PresionSistolica) + '' / '' + CONVERT (CHAR(3),GM.PresionDiastolica)
			,GM.Proteinuria
			,GM.MovimientosFetales
			,GM.SignosAlarma
			FROM	Gestante G INNER JOIN GestanteMonitoreo GM ON GM.GestanteKey = G.GestanteKey
					INNER JOIN Ubigeo Dist ON G.DistritoId = Dist.CodUbigeo
					INNER JOIN Ubigeo Prov ON LEFT(G.ProvinciaId,4 ) = LEFT(prov.CodUbigeo, 4) AND prov.CodDist = ''00''
					INNER JOIN Ubigeo DEP ON LEFT(G.RegionId, 2) = LEFT(DEP.CodUbigeo,2) AND dep.CodDist = ''00'' AND dep.CodProv = ''00''
			WHERE	G.Eliminado = 0
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReportMedicacionGestante]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReportMedicacionGestante]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sproc_ReportMedicacionGestante]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	G.GestanteNroDocumento
			,G.Nombres
			,G.APaterno
			,G.AMaterno
			,GM.Fecha
			,M.Descripcion + '' '' + GMD.Dosis AS Medicamento
			,GMD.Dias
			,GMD.Cantidad
			,DATEADD(dd,	GMD.Dias, GM.Fecha) AS FechaFin
	FROM	Gestante G (NOLOCK) INNER JOIN GestanteMedicamento GM (NOLOCK) 
	ON		G.GestanteKey = GM.GestanteKey INNER JOIN GestanteMedicamentoDetalle GMD (NOLOCK) 
	ON		GM.GestanteMedicamentoId = GMD.GestanteMedicamentoId INNER JOIN Medicamento M (NOLOCK) 
	ON		GMD.MedicamentoId = M.MedicamentoId
	WHERE	G.[Eliminado] = 0
	ORDER BY	GM.Fecha
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_ReportProcedenciaGestantes]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_ReportProcedenciaGestantes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE  PROCEDURE [dbo].[sproc_ReportProcedenciaGestantes]
AS
BEGIN
SET NOCOUNT ON;
	SELECT	G.Gestantekey 
			,G.GestanteId
			,G. GestanteNroDocumento
			,NroCitas = (SELECT COUNT(GestanteKey) FROM GestanteCita WHERE GestanteKey = G.GestanteKey)
			,FechaCita = ''Cita: '' + CONVERT(VARCHAR(10),GC.FechaCita,103) 
			,Establecimiento = ''Establecimiento: '' + e.Descripcion
			,G.FechaCreacion
			,Dist.Nombre AS Distrito
			,Prov.Nombre AS Provincia
			,dep.Nombre AS departamento
	FROM	gestante G INNER JOIN GestanteCita GC ON G.GestanteKey = GC.GestanteKey
			INNER JOIN Establecimiento E ON E.EstablecimientoId = G.EstablecimientoId
			INNER JOIN Ubigeo Dist ON G.DistritoId = Dist.CodUbigeo
			INNER JOIN Ubigeo Prov	ON LEFT (g.ProvinciaId,4 ) = left(prov.CodUbigeo, 4) AND prov.CodDist = ''00''
			INNER JOIN Ubigeo DEP ON LEFT(g.RegionId, 2) = left(DEP.CodUbigeo,2) AND dep.CodDist = ''00'' AND dep.CodProv = ''00''
			WHERE G.Eliminado = 0 AND GC.Eliminado = 0
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_Rpt_Gestantes_Participantes]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_Rpt_Gestantes_Participantes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sproc_Rpt_Gestantes_Participantes]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	G.Gestantekey,
			G.GestanteId,
			G.GestanteNroDocumento,
			NroCitas = (SELECT COUNT( GestanteKey) FROM GestanteCita WHERE GestanteKey = g.GestanteKey),
			FechaCita = ''Cita: '' + CONVERT(VARCHAR(10),GC.FechaCita,103),
			G.FechaCreacion,
			Dist.Nombre as Distrito,
			Prov.Nombre as Provincia,
			dep.Nombre as departamento,
			AsigMedic = (SELECT  COUNT(GestanteKey) FROM GestanteMedicamento where GestanteKey = g.GestanteKey),
			GM.Fecha,
			GM.NombreMedico
	FROM Gestante G
	INNER JOIN GestanteCita GC
	ON G.GestanteKey = GC.GestanteKey
	INNER JOIN GestanteMedicamento GM
	ON G.GestanteKey = GM.GestanteKey
	INNER JOIN Ubigeo Dist
	ON G.RegionId = Dist.CodUbigeo
	INNER JOIN Ubigeo Prov
	ON LEFT (G.ProvinciaId,4 ) = LEFT(Prov.CodUbigeo, 4)
	AND Prov.CodDist = ''00''
	INNER JOIN  Ubigeo DEP
	ON LEFT(G.RegionId, 2) = LEFT(DEP.CodUbigeo,2) 
	AND DEP.CodDist = ''00'' AND DEP.CodProv = ''00''
	WHERE G.Eliminado = 0 AND GC.Eliminado = 0
	--Aca faltarian los filtros de los parametros
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sproc_UpdateEstFromRenaes]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sproc_UpdateEstFromRenaes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sproc_UpdateEstFromRenaes]
    @TableRenaes EstRenaes readonly
AS
BEGIN
	DECLARE @MaxIdEst INT
	
	IF  EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N''[dbo].[TmpRecordsEst]'') AND TYPE in (N''U''))
	DROP TABLE [dbo].[TmpRecordsEst]

	SELECT * INTO [TmpRecordsEst] FROM @TableRenaes
    
    --Update Establecimientos Existentes
    UPDATE E
	SET E.[Descripcion] = ER.[Nombre],
		E.[Direccion] = ER.[Direccion],
		E.[FechaActualizacion] = GETDATE(),
		E.[UsuarioActualizacionId] = ER.[UsuarioActualizacion],
		E.[Ubigeo] = ER.[CodUbigeo]
	FROM Establecimiento E INNER JOIN TmpRecordsEst ER ON
    E.Renaes = ER.CodRenaes;
    
    --Insercion de nuevos establecimientos    
    WITH Establecimientos_nuevos (ID, Nombre, Direccion, FechaActualizacion, UsuarioActualizacion,
								  CodUbigeo, CodRenaes)
	AS
	(
		SELECT ROW_NUMBER() OVER(ORDER BY ER.CodRenaes) AS ID, ER.Nombre, ER.Direccion, GETDATE(),
		1, ER.CodUbigeo, ER.CodRenaes
		FROM TmpRecordsEst ER WHERE NOT EXISTS(SELECT E.Renaes FROM Establecimiento E WHERE E.Renaes = ER.CodRenaes)
	)
	
	INSERT INTO [Establecimiento]
	([Descripcion],[Direccion],[Renaes],[Ubigeo], [FechaCreacion],
	[FechaActualizacion],[UsuarioCreacionId],[UsuarioActualizacionId],[EstadoId])
	SELECT Nombre, Direccion, CodRenaes, CodUbigeo, FechaActualizacion,
	FechaActualizacion,UsuarioActualizacion,UsuarioActualizacion, 1 FROM Establecimientos_nuevos
	
	DROP TABLE [dbo].[TmpRecordsEst]
END' 
END
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ContenidoMensajeEducacional]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContenidoMensajeEducacional]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContenidoMensajeEducacional](
	[IdContenidoMensajeEducacional] [int] IDENTITY(1,1) NOT NULL,
	[IdMensajeEducacional] [int] NOT NULL,
	[DiaSemana] [tinyint] NOT NULL,
	[Contenido] [varchar](250) NOT NULL,
	[FechaUltimoProceso] [date] NULL,
 CONSTRAINT [PK_ContenidoMensajeEducacional] PRIMARY KEY CLUSTERED 
(
	[IdContenidoMensajeEducacional] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Diagnostico]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diagnostico]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Diagnostico](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](400) NOT NULL,
	[Id10] [varchar](10) NOT NULL,
	[Grupo] [varchar](200) NULL,
 CONSTRAINT [PK_Diagnostico] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Establecimiento]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Establecimiento]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Establecimiento](
	[EstablecimientoId] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[Direccion] [varchar](300) NULL,
	[Renaes] [int] NULL,
	[Ubigeo] [int] NULL,
	[EstablecimientoEmail] [varchar](50) NULL,
	[EstablecimientoTelefono] [varchar](50) NULL,
	[EstablecimientoRUC] [varchar](15) NULL,
	[FechaCreacion] [datetime] NULL,
	[FechaActualizacion] [datetime] NULL,
	[UsuarioCreacionId] [int] NULL,
	[UsuarioActualizacionId] [int] NULL,
	[EstadoId] [tinyint] NOT NULL,
 CONSTRAINT [PK_Establecimiento] PRIMARY KEY CLUSTERED 
(
	[EstablecimientoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Estado]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Estado](
	[Id] [tinyint] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gestante]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Gestante]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Gestante](
	[GestanteKey] [int] IDENTITY(1,1) NOT NULL,
	[GestanteId] [varchar](11) NOT NULL,
	[GestanteNroDocumento] [varchar](8) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[APaterno] [varchar](50) NOT NULL,
	[AMaterno] [varchar](50) NOT NULL,
	[FechaNacimiento] [datetime] NULL,
	[FechaUltimaRegla] [datetime] NULL,
	[FechaProbableParto] [datetime] NULL,
	[PresionSistolicaBase] [int] NOT NULL,
	[PresionDiastolicaBase] [int] NOT NULL,
	[DiagnosticoIngreso] [int] NULL,
	[DiagnosticoIntermedio1] [int] NULL,
	[DiagnosticoIntermedio2] [int] NULL,
	[DiagnosticoEgreso] [int] NULL,
	[EstablecimientoId] [int] NOT NULL,
	[EstablecimientoNotificacionId] [int] NOT NULL,
	[GestanteTelefono] [varchar](50) NULL,
	[GestanteDireccion] [varchar](100) NULL,
	[GestanteEmail] [varchar](50) NULL,
	[DistritoId] [varchar](6) NOT NULL,
	[ProvinciaId] [varchar](6) NOT NULL,
	[RegionId] [varchar](6) NOT NULL,
	[HorarioMensaje] [tinyint] NOT NULL,
	[FechaCreacion] [datetime] NULL,
	[Eliminado] [bit] NULL CONSTRAINT [DF_Gestante_Eliminado]  DEFAULT ((0)),
	[UsuarioEditor] [int] NOT NULL,
 CONSTRAINT [PK_Gestante] PRIMARY KEY CLUSTERED 
(
	[GestanteKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gestante_Historia]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Gestante_Historia]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Gestante_Historia](
	[GestanteKey] [int] NOT NULL,
	[GestanteId] [varchar](11) NOT NULL,
	[GestanteNroDocumento] [varchar](8) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[APaterno] [varchar](50) NOT NULL,
	[AMaterno] [varchar](50) NOT NULL,
	[FechaNacimiento] [datetime] NULL,
	[FechaUltimaRegla] [datetime] NULL,
	[FechaProbableParto] [datetime] NULL,
	[PresionSistolicaBase] [int] NOT NULL,
	[PresionDiastolicaBase] [int] NOT NULL,
	[DiagnosticoIngreso] [int] NULL,
	[DiagnosticoIntermedio1] [int] NULL,
	[DiagnosticoIntermedio2] [int] NULL,
	[DiagnosticoEgreso] [int] NULL,
	[EstablecimientoId] [int] NOT NULL,
	[EstablecimientoNotificacionId] [int] NOT NULL,
	[GestanteTelefono] [varchar](50) NULL,
	[GestanteDireccion] [varchar](100) NULL,
	[GestanteEmail] [varchar](50) NULL,
	[DistritoId] [varchar](6) NOT NULL,
	[ProvinciaId] [varchar](6) NOT NULL,
	[RegionId] [varchar](6) NOT NULL,
	[HorarioMensaje] [tinyint] NOT NULL,
	[FechaCreacion] [datetime] NULL,
	[Eliminado] [bit] NOT NULL,
	[UsuarioModificacion] [int] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
	[Accion] [varchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GestanteCita]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GestanteCita]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GestanteCita](
	[GestanteCitaId] [int] IDENTITY(1,1) NOT NULL,
	[GestanteKey] [int] NOT NULL,
	[FechaCita] [datetime] NOT NULL,
	[HoraCita] [varchar](4) NOT NULL,
	[NombreMedico] [varchar](50) NULL,
	[EstablecimientoId] [int] NOT NULL,
	[FechaCreacion] [datetime] NULL,
	[Eliminado] [bit] NULL CONSTRAINT [DF_GestanteCita_Eliminado]  DEFAULT ((0)),
	[UsuarioEditor] [int] NOT NULL,
 CONSTRAINT [PK_GestanteCita] PRIMARY KEY CLUSTERED 
(
	[GestanteCitaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GestanteCita_Historia]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GestanteCita_Historia]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GestanteCita_Historia](
	[GestanteCitaId] [int] NOT NULL,
	[GestanteKey] [int] NOT NULL,
	[FechaCita] [datetime] NOT NULL,
	[HoraCita] [varchar](4) NOT NULL,
	[NombreMedico] [varchar](50) NULL,
	[EstablecimientoId] [int] NOT NULL,
	[FechaCreacion] [datetime] NULL,
	[Eliminado] [bit] NOT NULL,
	[UsuarioModificacion] [int] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
	[Accion] [varchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GestanteMedicamento]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GestanteMedicamento]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GestanteMedicamento](
	[GestanteMedicamentoId] [int] IDENTITY(1,1) NOT NULL,
	[GestanteKey] [int] NOT NULL,
	[NombreMedico] [varchar](50) NULL,
	[Fecha] [date] NULL,
	[EstablecimientoId] [int] NOT NULL,
 CONSTRAINT [PK_GestanteMedicamento] PRIMARY KEY CLUSTERED 
(
	[GestanteMedicamentoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GestanteMedicamentoDetalle]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GestanteMedicamentoDetalle]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GestanteMedicamentoDetalle](
	[GestanteMedicamentoDetalleId] [int] IDENTITY(1,1) NOT NULL,
	[GestanteMedicamentoId] [int] NOT NULL,
	[MedicamentoId] [int] NOT NULL,
	[Dosis] [varchar](50) NULL,
	[Dias] [int] NULL,
	[Cantidad] [varchar](50) NULL,
	[Instrucciones] [varchar](250) NULL,
	[FechaUltimoProceso] [date] NULL,
 CONSTRAINT [PK_GestanteMedicamentoDetalle] PRIMARY KEY CLUSTERED 
(
	[GestanteMedicamentoDetalleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GestanteMonitoreo]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GestanteMonitoreo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GestanteMonitoreo](
	[GestanteMonitoreoId] [int] IDENTITY(1,1) NOT NULL,
	[GestanteKey] [int] NOT NULL,
	[PresionSistolica] [int] NULL,
	[PresionDiastolica] [int] NULL,
	[Proteinuria] [int] NULL,
	[MovimientosFetales] [int] NULL,
	[SignosAlarma] [varchar](30) NULL,
	[FechaRegistro] [datetime] NULL,
 CONSTRAINT [PK_GestanteMonitoreo] PRIMARY KEY CLUSTERED 
(
	[GestanteMonitoreoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Medicamento]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Medicamento]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Medicamento](
	[MedicamentoId] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[Concentracion] [varchar](50) NULL,
	[Formato] [varchar](50) NULL,
	[Presentacion] [varchar](80) NULL,
	[FechaRegistro] [datetime] NULL,
	[NumeroRegistro] [varchar](20) NULL,
	[Titular] [varchar](50) NULL,
	[IdDIGEMID] [int] NULL,
 CONSTRAINT [PK_Medicamento] PRIMARY KEY CLUSTERED 
(
	[MedicamentoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MensajeEducacional]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MensajeEducacional]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MensajeEducacional](
	[IdMensajeEducacional] [int] IDENTITY(1,1) NOT NULL,
	[SemanaEmbarazo] [varchar](20) NOT NULL,
	[EstadoId] [tinyint] NOT NULL,
	[UsuarioEditor] [int] NOT NULL,
	[FechaCreacion] [datetime] NULL,
 CONSTRAINT [PK_MensajeEducacional] PRIMARY KEY CLUSTERED 
(
	[IdMensajeEducacional] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Menu](
	[MenuId] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Descripcion] [varchar](200) NULL,
	[Nivel] [tinyint] NOT NULL,
	[MenuPadre] [smallint] NULL,
	[EsBoton] [bit] NOT NULL,
	[MenuImagen] [varchar](100) NULL,
	[EstadoId] [tinyint] NOT NULL,
	[Url] [varchar](500) NULL,
	[Orden] [tinyint] NULL,
	[Controlador] [varchar](50) NULL,
	[Accion] [varchar](50) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProcesoMensajeEducacional]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProcesoMensajeEducacional]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProcesoMensajeEducacional](
	[IdContenidoMensajeEducacional] [int] NOT NULL,
	[GestanteKey] [int] NOT NULL,
	[FechaEnvio] [date] NOT NULL,
	[Turno] [int] NOT NULL,
	[Procesado] [bit] NOT NULL,
	[FechaHoraProceso] [datetime] NULL,
	[ResultadoProceso] [varchar](50) NULL,
 CONSTRAINT [PK_ProcesoMensajeEducacional] PRIMARY KEY CLUSTERED 
(
	[IdContenidoMensajeEducacional] ASC,
	[GestanteKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegistroEvento]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RegistroEvento]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RegistroEvento](
	[RegistroEventoKey] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioKey] [int] NOT NULL,
	[EventoFecha] [datetime] NULL,
	[IdTipoAccion] [tinyint] NOT NULL,
	[IdTipoObjeto] [tinyint] NOT NULL,
	[IdRegistro] [int] NULL,
	[Origen] [varchar](20) NULL,
 CONSTRAINT [PK_RegistroEvento] PRIMARY KEY CLUSTERED 
(
	[RegistroEventoKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegistroProceso]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RegistroProceso]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RegistroProceso](
	[RegistroProcesoKey] [int] IDENTITY(1,1) NOT NULL,
	[IdProceso] [int] NOT NULL,
	[FechaUltimaEjecucion] [datetime] NULL,
 CONSTRAINT [PK_RegistroProceso] PRIMARY KEY CLUSTERED 
(
	[RegistroProcesoKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rol]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Rol](
	[RolId] [tinyint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[EstadoId] [tinyint] NOT NULL,
	[AspNetRoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[RolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RolMenu]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolMenu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RolMenu](
	[Menu_MenuId] [smallint] NOT NULL,
	[Rol_RolId] [tinyint] NOT NULL,
 CONSTRAINT [PK_RolMenu] PRIMARY KEY CLUSTERED 
(
	[Menu_MenuId] ASC,
	[Rol_RolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SmsQueue]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmsQueue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SmsQueue](
	[SmsQueueId] [int] IDENTITY(1,1) NOT NULL,
	[NumeroMovil] [varchar](50) NOT NULL,
	[TipoMensaje] [varchar](50) NOT NULL,
	[CuerpoMensaje] [varchar](500) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Procesado] [bit] NOT NULL,
	[ResultadoProceso] [varchar](250) NOT NULL,
	[ErrorProceso] [bit] NOT NULL,
	[FechaProceso] [datetime] NULL,
 CONSTRAINT [PK_SmsQueue] PRIMARY KEY CLUSTERED 
(
	[SmsQueueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoAccion]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TipoAccion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TipoAccion](
	[IdTipoAccion] [tinyint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](30) NOT NULL,
 CONSTRAINT [PK_TipoAccion] PRIMARY KEY CLUSTERED 
(
	[IdTipoAccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoObjeto]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TipoObjeto]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TipoObjeto](
	[IdTipoObjeto] [tinyint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TipoObjeto] PRIMARY KEY CLUSTERED 
(
	[IdTipoObjeto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ubigeo]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ubigeo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ubigeo](
	[IDUbigeo] [int] IDENTITY(1,1) NOT NULL,
	[CodUbigeo] [varchar](6) NOT NULL,
	[CodDpto] [varchar](2) NOT NULL,
	[CodProv] [varchar](2) NOT NULL,
	[CodDist] [varchar](2) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[EsDpto] [bit] NOT NULL,
	[EsProv] [bit] NOT NULL,
	[EsDist] [bit] NOT NULL,
 CONSTRAINT [PK_Ubigeo] PRIMARY KEY CLUSTERED 
(
	[IDUbigeo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Usuario](
	[UsuarioKey] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[APaterno] [varchar](50) NOT NULL,
	[AMaterno] [varchar](50) NOT NULL,
	[EstadoId] [tinyint] NOT NULL,
	[RecibeAlertas] [tinyint] NOT NULL,
	[EstablecimientoId] [int] NOT NULL,
	[UsuarioDireccion] [varchar](100) NULL,
	[UsuarioEditor] [int] NOT NULL,
	[Id] [nvarchar](128) NOT NULL,
	[Eliminado] [bit] NULL CONSTRAINT [DF_Usuario_Eliminado]  DEFAULT ((0)),
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[UsuarioKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario_Historia]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario_Historia]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Usuario_Historia](
	[UsuarioKey] [int] NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[APaterno] [varchar](50) NOT NULL,
	[AMaterno] [varchar](50) NOT NULL,
	[EstadoId] [tinyint] NOT NULL,
	[RecibeAlertas] [tinyint] NOT NULL,
	[EstablecimientoId] [int] NOT NULL,
	[UsuarioDireccion] [varchar](100) NULL,
	[Id] [nvarchar](128) NOT NULL,
	[UsuarioModificacion] [int] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
	[Accion] [varchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND name = N'RoleNameIndex')
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND name = N'IX_UserId')
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND name = N'IX_UserId')
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND name = N'IX_RoleId')
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND name = N'IX_UserId')
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND name = N'UserNameIndex')
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Establecimiento]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Establecimiento]') AND name = N'IX_Establecimiento')
CREATE NONCLUSTERED INDEX [IX_Establecimiento] ON [dbo].[Establecimiento]
(
	[Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FK_GestanteGestanteMonitoreo]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GestanteMonitoreo]') AND name = N'IX_FK_GestanteGestanteMonitoreo')
CREATE NONCLUSTERED INDEX [IX_FK_GestanteGestanteMonitoreo] ON [dbo].[GestanteMonitoreo]
(
	[GestanteKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TipoAccionRegistroEvento]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RegistroEvento]') AND name = N'IX_FK_TipoAccionRegistroEvento')
CREATE NONCLUSTERED INDEX [IX_FK_TipoAccionRegistroEvento] ON [dbo].[RegistroEvento]
(
	[IdTipoAccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TipoObjetoRegistroEvento]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RegistroEvento]') AND name = N'IX_FK_TipoObjetoRegistroEvento')
CREATE NONCLUSTERED INDEX [IX_FK_TipoObjetoRegistroEvento] ON [dbo].[RegistroEvento]
(
	[IdTipoObjeto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_UsuarioRegistroEvento]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RegistroEvento]') AND name = N'IX_FK_UsuarioRegistroEvento')
CREATE NONCLUSTERED INDEX [IX_FK_UsuarioRegistroEvento] ON [dbo].[RegistroEvento]
(
	[UsuarioKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_RolMenu_Rol]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RolMenu]') AND name = N'IX_FK_RolMenu_Rol')
CREATE NONCLUSTERED INDEX [IX_FK_RolMenu_Rol] ON [dbo].[RolMenu]
(
	[Rol_RolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Ubigeo]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Ubigeo]') AND name = N'IX_Ubigeo')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Ubigeo] ON [dbo].[Ubigeo]
(
	[CodUbigeo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UbigeoNombre]    Script Date: 11/11/2015 00:44:00 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Ubigeo]') AND name = N'IX_UbigeoNombre')
CREATE NONCLUSTERED INDEX [IX_UbigeoNombre] ON [dbo].[Ubigeo]
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContenidoMensajeEducacional_MensajeEducacional]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContenidoMensajeEducacional]'))
ALTER TABLE [dbo].[ContenidoMensajeEducacional]  WITH CHECK ADD  CONSTRAINT [FK_ContenidoMensajeEducacional_MensajeEducacional] FOREIGN KEY([IdMensajeEducacional])
REFERENCES [dbo].[MensajeEducacional] ([IdMensajeEducacional])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContenidoMensajeEducacional_MensajeEducacional]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContenidoMensajeEducacional]'))
ALTER TABLE [dbo].[ContenidoMensajeEducacional] CHECK CONSTRAINT [FK_ContenidoMensajeEducacional_MensajeEducacional]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Establecimiento_Estado]') AND parent_object_id = OBJECT_ID(N'[dbo].[Establecimiento]'))
ALTER TABLE [dbo].[Establecimiento]  WITH CHECK ADD  CONSTRAINT [FK_Establecimiento_Estado] FOREIGN KEY([EstadoId])
REFERENCES [dbo].[Estado] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Establecimiento_Estado]') AND parent_object_id = OBJECT_ID(N'[dbo].[Establecimiento]'))
ALTER TABLE [dbo].[Establecimiento] CHECK CONSTRAINT [FK_Establecimiento_Estado]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EstablecimientoGestante]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante]  WITH CHECK ADD  CONSTRAINT [FK_EstablecimientoGestante] FOREIGN KEY([EstablecimientoId])
REFERENCES [dbo].[Establecimiento] ([EstablecimientoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EstablecimientoGestante]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] CHECK CONSTRAINT [FK_EstablecimientoGestante]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EstablecimientoNotificacionGestante]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante]  WITH CHECK ADD  CONSTRAINT [FK_EstablecimientoNotificacionGestante] FOREIGN KEY([EstablecimientoNotificacionId])
REFERENCES [dbo].[Establecimiento] ([EstablecimientoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EstablecimientoNotificacionGestante]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] CHECK CONSTRAINT [FK_EstablecimientoNotificacionGestante]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_DiagnosticoEgreso]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante]  WITH CHECK ADD  CONSTRAINT [FK_Gestante_DiagnosticoEgreso] FOREIGN KEY([DiagnosticoEgreso])
REFERENCES [dbo].[Diagnostico] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_DiagnosticoEgreso]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] CHECK CONSTRAINT [FK_Gestante_DiagnosticoEgreso]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_DiagnosticoIngreso]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante]  WITH CHECK ADD  CONSTRAINT [FK_Gestante_DiagnosticoIngreso] FOREIGN KEY([DiagnosticoIngreso])
REFERENCES [dbo].[Diagnostico] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_DiagnosticoIngreso]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] CHECK CONSTRAINT [FK_Gestante_DiagnosticoIngreso]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_DiagnosticoIntermedio1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante]  WITH CHECK ADD  CONSTRAINT [FK_Gestante_DiagnosticoIntermedio1] FOREIGN KEY([DiagnosticoIntermedio1])
REFERENCES [dbo].[Diagnostico] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_DiagnosticoIntermedio1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] CHECK CONSTRAINT [FK_Gestante_DiagnosticoIntermedio1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_DiagnosticoIntermedio2]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante]  WITH CHECK ADD  CONSTRAINT [FK_Gestante_DiagnosticoIntermedio2] FOREIGN KEY([DiagnosticoIntermedio2])
REFERENCES [dbo].[Diagnostico] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_DiagnosticoIntermedio2]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] CHECK CONSTRAINT [FK_Gestante_DiagnosticoIntermedio2]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_Distrito]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante]  WITH CHECK ADD  CONSTRAINT [FK_Gestante_Distrito] FOREIGN KEY([DistritoId])
REFERENCES [dbo].[Ubigeo] ([CodUbigeo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_Distrito]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] CHECK CONSTRAINT [FK_Gestante_Distrito]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_Provincia]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante]  WITH CHECK ADD  CONSTRAINT [FK_Gestante_Provincia] FOREIGN KEY([ProvinciaId])
REFERENCES [dbo].[Ubigeo] ([CodUbigeo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_Provincia]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] CHECK CONSTRAINT [FK_Gestante_Provincia]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_Region]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante]  WITH CHECK ADD  CONSTRAINT [FK_Gestante_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Ubigeo] ([CodUbigeo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gestante_Region]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] CHECK CONSTRAINT [FK_Gestante_Region]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteCitaEstablecimiento]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteCita]'))
ALTER TABLE [dbo].[GestanteCita]  WITH CHECK ADD  CONSTRAINT [FK_GestanteCitaEstablecimiento] FOREIGN KEY([EstablecimientoId])
REFERENCES [dbo].[Establecimiento] ([EstablecimientoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteCitaEstablecimiento]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteCita]'))
ALTER TABLE [dbo].[GestanteCita] CHECK CONSTRAINT [FK_GestanteCitaEstablecimiento]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteGestanteCita]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteCita]'))
ALTER TABLE [dbo].[GestanteCita]  WITH CHECK ADD  CONSTRAINT [FK_GestanteGestanteCita] FOREIGN KEY([GestanteKey])
REFERENCES [dbo].[Gestante] ([GestanteKey])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteGestanteCita]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteCita]'))
ALTER TABLE [dbo].[GestanteCita] CHECK CONSTRAINT [FK_GestanteGestanteCita]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteGestanteMedicamento]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMedicamento]'))
ALTER TABLE [dbo].[GestanteMedicamento]  WITH CHECK ADD  CONSTRAINT [FK_GestanteGestanteMedicamento] FOREIGN KEY([GestanteKey])
REFERENCES [dbo].[Gestante] ([GestanteKey])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteGestanteMedicamento]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMedicamento]'))
ALTER TABLE [dbo].[GestanteMedicamento] CHECK CONSTRAINT [FK_GestanteGestanteMedicamento]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteMedicamentoEstablecimiento]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMedicamento]'))
ALTER TABLE [dbo].[GestanteMedicamento]  WITH CHECK ADD  CONSTRAINT [FK_GestanteMedicamentoEstablecimiento] FOREIGN KEY([EstablecimientoId])
REFERENCES [dbo].[Establecimiento] ([EstablecimientoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteMedicamentoEstablecimiento]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMedicamento]'))
ALTER TABLE [dbo].[GestanteMedicamento] CHECK CONSTRAINT [FK_GestanteMedicamentoEstablecimiento]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteMedicamentoGestanteMedicamentoDetalle]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMedicamentoDetalle]'))
ALTER TABLE [dbo].[GestanteMedicamentoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_GestanteMedicamentoGestanteMedicamentoDetalle] FOREIGN KEY([GestanteMedicamentoId])
REFERENCES [dbo].[GestanteMedicamento] ([GestanteMedicamentoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteMedicamentoGestanteMedicamentoDetalle]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMedicamentoDetalle]'))
ALTER TABLE [dbo].[GestanteMedicamentoDetalle] CHECK CONSTRAINT [FK_GestanteMedicamentoGestanteMedicamentoDetalle]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MedicamentoGestanteMedicamentoDetalle]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMedicamentoDetalle]'))
ALTER TABLE [dbo].[GestanteMedicamentoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_MedicamentoGestanteMedicamentoDetalle] FOREIGN KEY([MedicamentoId])
REFERENCES [dbo].[Medicamento] ([MedicamentoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MedicamentoGestanteMedicamentoDetalle]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMedicamentoDetalle]'))
ALTER TABLE [dbo].[GestanteMedicamentoDetalle] CHECK CONSTRAINT [FK_MedicamentoGestanteMedicamentoDetalle]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteGestanteMonitoreo]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMonitoreo]'))
ALTER TABLE [dbo].[GestanteMonitoreo]  WITH CHECK ADD  CONSTRAINT [FK_GestanteGestanteMonitoreo] FOREIGN KEY([GestanteKey])
REFERENCES [dbo].[Gestante] ([GestanteKey])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GestanteGestanteMonitoreo]') AND parent_object_id = OBJECT_ID(N'[dbo].[GestanteMonitoreo]'))
ALTER TABLE [dbo].[GestanteMonitoreo] CHECK CONSTRAINT [FK_GestanteGestanteMonitoreo]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MensajeEducacional_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[MensajeEducacional]'))
ALTER TABLE [dbo].[MensajeEducacional]  WITH CHECK ADD  CONSTRAINT [FK_MensajeEducacional_Usuario] FOREIGN KEY([UsuarioEditor])
REFERENCES [dbo].[Usuario] ([UsuarioKey])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MensajeEducacional_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[MensajeEducacional]'))
ALTER TABLE [dbo].[MensajeEducacional] CHECK CONSTRAINT [FK_MensajeEducacional_Usuario]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TipoAccionRegistroEvento]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegistroEvento]'))
ALTER TABLE [dbo].[RegistroEvento]  WITH CHECK ADD  CONSTRAINT [FK_TipoAccionRegistroEvento] FOREIGN KEY([IdTipoAccion])
REFERENCES [dbo].[TipoAccion] ([IdTipoAccion])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TipoAccionRegistroEvento]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegistroEvento]'))
ALTER TABLE [dbo].[RegistroEvento] CHECK CONSTRAINT [FK_TipoAccionRegistroEvento]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TipoObjetoRegistroEvento]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegistroEvento]'))
ALTER TABLE [dbo].[RegistroEvento]  WITH CHECK ADD  CONSTRAINT [FK_TipoObjetoRegistroEvento] FOREIGN KEY([IdTipoObjeto])
REFERENCES [dbo].[TipoObjeto] ([IdTipoObjeto])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TipoObjetoRegistroEvento]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegistroEvento]'))
ALTER TABLE [dbo].[RegistroEvento] CHECK CONSTRAINT [FK_TipoObjetoRegistroEvento]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsuarioRegistroEvento]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegistroEvento]'))
ALTER TABLE [dbo].[RegistroEvento]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioRegistroEvento] FOREIGN KEY([UsuarioKey])
REFERENCES [dbo].[Usuario] ([UsuarioKey])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsuarioRegistroEvento]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegistroEvento]'))
ALTER TABLE [dbo].[RegistroEvento] CHECK CONSTRAINT [FK_UsuarioRegistroEvento]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolMenu_Menu]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolMenu]'))
ALTER TABLE [dbo].[RolMenu]  WITH CHECK ADD  CONSTRAINT [FK_RolMenu_Menu] FOREIGN KEY([Menu_MenuId])
REFERENCES [dbo].[Menu] ([MenuId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolMenu_Menu]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolMenu]'))
ALTER TABLE [dbo].[RolMenu] CHECK CONSTRAINT [FK_RolMenu_Menu]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolMenu_Rol]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolMenu]'))
ALTER TABLE [dbo].[RolMenu]  WITH CHECK ADD  CONSTRAINT [FK_RolMenu_Rol] FOREIGN KEY([Rol_RolId])
REFERENCES [dbo].[Rol] ([RolId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolMenu_Rol]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolMenu]'))
ALTER TABLE [dbo].[RolMenu] CHECK CONSTRAINT [FK_RolMenu_Rol]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EstablecimientoUsuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario]'))
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_EstablecimientoUsuario] FOREIGN KEY([EstablecimientoId])
REFERENCES [dbo].[Establecimiento] ([EstablecimientoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EstablecimientoUsuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario]'))
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_EstablecimientoUsuario]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_AspNetUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario]'))
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_AspNetUsers] FOREIGN KEY([Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_AspNetUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario]'))
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_AspNetUsers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Estado]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario]'))
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Estado] FOREIGN KEY([EstadoId])
REFERENCES [dbo].[Estado] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Estado]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario]'))
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Estado]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Gestante_FechaProbableParto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante]  WITH CHECK ADD  CONSTRAINT [CK_Gestante_FechaProbableParto] CHECK  (([FechaProbableParto]>[FechaUltimaRegla]))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Gestante_FechaProbableParto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] CHECK CONSTRAINT [CK_Gestante_FechaProbableParto]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Gestante_FechaUltimaRegla]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante]  WITH CHECK ADD  CONSTRAINT [CK_Gestante_FechaUltimaRegla] CHECK  (([FechaUltimaRegla]>[FechaNacimiento]))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Gestante_FechaUltimaRegla]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gestante]'))
ALTER TABLE [dbo].[Gestante] CHECK CONSTRAINT [CK_Gestante_FechaUltimaRegla]
GO
/****** Object:  Trigger [dbo].[TR_AFT_IU_Cambios_Usuario]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TR_AFT_IU_Cambios_Usuario]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [dbo].[TR_AFT_IU_Cambios_Usuario]
ON [dbo].[Usuario]
AFTER INSERT, UPDATE
AS 
SET NOCOUNT ON
	DECLARE @INS INT, @DEL INT

	SELECT @INS = COUNT(*) FROM INSERTED
	SELECT @DEL = COUNT(*) FROM DELETED

	IF @INS > 0 AND @DEL > 0 
		BEGIN
			-- Un registro fue actualizado.
			INSERT INTO Usuario_Historia
			SELECT [UsuarioKey],[Nombres],[APaterno],[AMaterno],[EstadoId],[RecibeAlertas],[EstablecimientoId],[UsuarioDireccion],
				   [Id],[UsuarioEditor],getdate(),''ACTUALIZACION''  FROM INSERTED
		END
	ELSE 
		BEGIN
			-- Un registro fue insertado.
			INSERT INTO Usuario_Historia
			SELECT [UsuarioKey],[Nombres],[APaterno],[AMaterno],[EstadoId],[RecibeAlertas],[EstablecimientoId],[UsuarioDireccion],
				   [Id],[UsuarioEditor],getdate(),''INSERCION''  FROM INSERTED
		END
' 
GO
/****** Object:  Trigger [dbo].[TR_AFT_IU_Cambios_GestanteCita]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TR_AFT_IU_Cambios_GestanteCita]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [dbo].[TR_AFT_IU_Cambios_GestanteCita]
ON [dbo].[GestanteCita]
AFTER INSERT, UPDATE
AS 
SET NOCOUNT ON
	DECLARE @INS INT, @DEL INT

	SELECT @INS = COUNT(*) FROM INSERTED
	SELECT @DEL = COUNT(*) FROM DELETED

	IF @INS > 0 AND @DEL > 0 
		BEGIN
			-- Un registro fue actualizado.
			INSERT INTO GestanteCita_Historia
			SELECT [GestanteCitaId]
				  ,[GestanteKey]
				  ,[FechaCita]
				  ,[HoraCita]
				  ,[NombreMedico]
				  ,[EstablecimientoId]
				  ,[FechaCreacion]
				  ,[Eliminado]
				  ,[UsuarioEditor]
				  ,getdate()
				  ,''ACTUALIZACION''  
			FROM INSERTED
		END
	ELSE 
		BEGIN
			-- Un registro fue insertado.
			INSERT INTO GestanteCita_Historia
			SELECT [GestanteCitaId]
				  ,[GestanteKey]
				  ,[FechaCita]
				  ,[HoraCita]
				  ,[NombreMedico]
				  ,[EstablecimientoId]
				  ,[FechaCreacion]
				  ,[Eliminado]
				  ,[UsuarioEditor]
				  ,getdate()
				  ,''INSERCION''  
			FROM INSERTED
		END
' 
GO
/****** Object:  Trigger [dbo].[TR_AFT_IU_Cambios_Gestante]    Script Date: 11/11/2015 00:44:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TR_AFT_IU_Cambios_Gestante]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [dbo].[TR_AFT_IU_Cambios_Gestante]
ON [dbo].[Gestante]
AFTER INSERT, UPDATE
AS 
SET NOCOUNT ON
	DECLARE @INS INT, @DEL INT

	SELECT @INS = COUNT(*) FROM INSERTED
	SELECT @DEL = COUNT(*) FROM DELETED

	IF @INS > 0 AND @DEL > 0 
		BEGIN
			-- Un registro fue actualizado.
			INSERT INTO Gestante_Historia
			SELECT [GestanteKey]
				  ,[GestanteId]
				  ,[GestanteNroDocumento]
				  ,[Nombres]
				  ,[APaterno]
				  ,[AMaterno]
				  ,[FechaNacimiento]
				  ,[FechaUltimaRegla]
				  ,[FechaProbableParto]
				  ,[PresionSistolicaBase]
				  ,[PresionDiastolicaBase] 
				  ,[DiagnosticoIngreso]
				  ,[DiagnosticoIntermedio1]
				  ,[DiagnosticoIntermedio2]
				  ,[DiagnosticoEgreso]
				  ,[EstablecimientoId]
				  ,[EstablecimientoNotificacionId]
				  ,[GestanteTelefono]
				  ,[GestanteDireccion]
				  ,[GestanteEmail]
				  ,[DistritoId]
				  ,[ProvinciaId]
				  ,[RegionId]
				  ,[HorarioMensaje]
				  ,[FechaCreacion]
				  ,[Eliminado]
				  ,[UsuarioEditor]
				  ,getdate()
				  ,''ACTUALIZACION''  
			FROM INSERTED
		END
	ELSE 
		BEGIN
			-- Un registro fue insertado.
			INSERT INTO Gestante_Historia
			SELECT [GestanteKey]
				  ,[GestanteId]
				  ,[GestanteNroDocumento]
				  ,[Nombres]
				  ,[APaterno]
				  ,[AMaterno]
				  ,[FechaNacimiento]
				  ,[FechaUltimaRegla]
				  ,[FechaProbableParto]
				  ,[PresionSistolicaBase]
				  ,[PresionDiastolicaBase] 
				  ,[DiagnosticoIngreso]
				  ,[DiagnosticoIntermedio1]
				  ,[DiagnosticoIntermedio2]
				  ,[DiagnosticoEgreso]
				  ,[EstablecimientoId]
				  ,[EstablecimientoNotificacionId]
				  ,[GestanteTelefono]
				  ,[GestanteDireccion]
				  ,[GestanteEmail]
				  ,[DistritoId]
				  ,[ProvinciaId]
				  ,[RegionId]
				  ,[HorarioMensaje]          
				  ,[FechaCreacion]
				  ,[Eliminado]
				  ,[UsuarioEditor]
				  ,getdate()
				  ,''INSERCION''  
			FROM INSERTED
		END
' 
GO
