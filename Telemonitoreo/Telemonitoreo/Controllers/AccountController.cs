using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Telemonitoreo.Business;
using Telemonitoreo.Models;
using Telemonitoreo.Utils;
using DataAccess;
using Telemonitoreo.Enums;


namespace Telemonitoreo.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private EstablecimientoManager _establecimientoManager = new EstablecimientoManager();
        private UtilManager _utilManager = new UtilManager();
        UsuarioManager _usuarioManager = new UsuarioManager();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ActionResult Index()
        {
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ListaUsuarios, null);
            ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion");
            ViewBag.RoleName = new SelectList(RoleManager.Roles.Where(R => !R.Name.Equals("Gestante")).ToList(), "Name", "Name");
            ViewBag.Estado = new SelectList(_utilManager.ListarEstados(), "Id", "Descripcion");
            return View();
        }

        // GET: BuscarUsuarios
        public ActionResult BuscarUsuarios(string numDni, string aPaterno, string aMaterno, string estado, string establecimiento, string rol)
        {
            var page = Convert.ToInt32(Request.Params["page"]);
            var records = Convert.ToInt32(Request.Params["rows"]);
            var sortColumn = Request.Params["sidx"] ?? "";
            var sortDirection = Request.Params["sord"] ?? "asc";
            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ListaUsuarios, null);
            var listaUsuarios = _usuarioManager.ListarUsuarios(numDni, aPaterno, aMaterno, estado, establecimiento, rol, sortColumn, sortDirection);
            var count = listaUsuarios.Count;

            var jsonDataObject = new
            {
                page = page,
                total = (int)Math.Ceiling((double)count / records),
                records = count,
                rows = listaUsuarios.Skip((page - 1) * records).Take(records)
            };
            return new JsonResult()
            {
                Data = jsonDataObject,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public ActionResult ExportarExcel(FormCollection form)
        {
            try
            {
                var numDni = form["NroDocumento"];
                var aPaterno = form["APaterno"];
                var aMaterno = form["AMaterno"];
                var estado = string.IsNullOrWhiteSpace(form["Estado"]) ? "" : form["Estado"];
                var establecimiento = string.IsNullOrWhiteSpace(form["EstablecimientoId"]) ? "" : form["EstablecimientoId"];
                var rol = string.IsNullOrWhiteSpace(form["RoleName"]) ? "" : form["RoleName"];
                var sortColumn = form["hdSortColumn"] ?? "UsuarioKey";
                var sortDirection = form["hdSortDirection"] ?? "asc";

                RegistrarAccion((byte)AccionSesion.ExportExcel, (byte)ObjetoSesion.ListaUsuarios, null);
                var listaUsuarios = _usuarioManager.ListarUsuarios(numDni, aPaterno, aMaterno, estado, establecimiento, rol, sortColumn, sortDirection);

                var listaUsuariosExcel = from o in listaUsuarios
                                        select new
                                        {
                                            ID_Registro = o.UsuarioKey,
                                            DNI = o.UserName,
                                            o.Nombres,
                                            A_Paterno = o.APaterno,
                                            A_Materno = o.AMaterno,
                                            Est_Salud = o.Establecimiento,
                                            Rol_Asignado = o.RoleName,
                                            o.Estado
                                        };

                ExcelExport.ExportToSpreadsheet(listaUsuariosExcel.CopyToDataTable(), "Usuarios");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e;
                return PartialView("Error");
            }
        }

        public ActionResult RolAcceso()
        {
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.RolesUsuario, null);
            ViewBag.MenusRoles = _usuarioManager.ListaMenuRol();
            return View();
        }

        // GET: Gestante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditRegisterViewModel usuario = _usuarioManager.MostrarUsuario((int)id);

            if (usuario == null)
            {
                return HttpNotFound();
            }
            ConfigurarMenues();
            ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion", usuario.EstablecimientoId);
            ViewBag.RoleName = new SelectList(RoleManager.Roles.Where(R => !R.Name.Equals("Gestante")).ToList(), "Name", "Name", usuario.RoleName);
            ViewBag.EstadoId = new SelectList(_utilManager.ListarEstados(), "Id", "Descripcion", usuario.EstadoId);
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.EdicionUsuario, null);
            return View(usuario);
        }

        // POST: Gestante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditRegisterViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(usuario.UserName);

                if (user.Email != usuario.Email || user.PhoneNumber != usuario.PhoneNumber)
                {
                    user.Email = usuario.Email;
                    user.PhoneNumber = usuario.PhoneNumber;
                    UserManager.Update(user);
                }

                if (!string.IsNullOrEmpty(usuario.Password))
                {
                    UserManager.RemovePassword(user.Id);
                    UserManager.AddPassword(user.Id, usuario.Password);
                }

                var rolNuevo = RoleManager.FindByName(usuario.RoleName);
                var identityUserRole = user.Roles.FirstOrDefault();
                if (identityUserRole != null && identityUserRole.RoleId != rolNuevo.Id)
                {
                    var rolActual = RoleManager.FindById(identityUserRole.RoleId);
                    UserManager.RemoveFromRole(user.Id, rolActual.Name);
                    var roleResult = UserManager.AddToRole(user.Id, usuario.RoleName);

                    if (!roleResult.Succeeded)
                    { 
                        ModelState.AddModelError("", roleResult.Errors.First());
                        ConfigurarMenues();
                        ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion", usuario.EstablecimientoId);
                        ViewBag.RoleName = new SelectList(RoleManager.Roles.Where(R => !R.Name.Equals("Gestante")).ToList(), "Name", "Name", usuario.RoleName);
                        ViewBag.EstadoId = new SelectList(_utilManager.ListarEstados(), "Id", "Descripcion", usuario.EstadoId);
                        return View(usuario);
                    }
                }
                
                var userLogId = "1";
                if (Request.IsAuthenticated)
                {
                    userLogId = User.Identity.GetUserId();
                }

                var resultGrabar = _usuarioManager.GrabarUsuario(usuario, userLogId);

                if (resultGrabar)
                {
                    RegistrarAccion((byte)AccionSesion.Actualizar, (byte)ObjetoSesion.EdicionUsuario, usuario.UsuarioKey);
                    return RedirectToAction("Index");
                }   

                ModelState.AddModelError("", new Exception("El usuario no se encontro en la base de datos."));
                ConfigurarMenues();
                ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion", usuario.EstablecimientoId);
                ViewBag.RoleName = new SelectList(RoleManager.Roles.Where(R => !R.Name.Equals("Gestante")).ToList(), "Name", "Name", usuario.RoleName);
                ViewBag.EstadoId = new SelectList(_utilManager.ListarEstados(), "Id", "Descripcion", usuario.EstadoId);
                return View(usuario);
            }
            ConfigurarMenues();
            ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion", usuario.EstablecimientoId);
            ViewBag.RoleName = new SelectList(RoleManager.Roles.Where(R => !R.Name.Equals("Gestante")).ToList(), "Name", "Name", usuario.RoleName);
            ViewBag.EstadoId = new SelectList(_utilManager.ListarEstados(), "Id", "Descripcion", usuario.EstadoId);
            return View(usuario);
        }

        public ActionResult EditPersonal()
        {
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.MensajeResult = "El usuario no esta logueado.";
                return View();
            }

            var userLogId = User.Identity.GetUserId();

            var usuario = _usuarioManager.MostrarUsuarioLogueado(userLogId);

            if (usuario == null)
            {
                return HttpNotFound();
            }
            ConfigurarMenues();
            ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion", usuario.EstablecimientoId);
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.EdicionDatos, usuario.UsuarioKey);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPersonal(EditPersonalViewModel usuario)
        {
            ConfigurarMenues();
            ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion", usuario.EstablecimientoId);
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(usuario.UserName);

                if (user.Email != usuario.Email || user.PhoneNumber != usuario.PhoneNumber)
                {
                    user.Email = usuario.Email;
                    user.PhoneNumber = usuario.PhoneNumber;
                    UserManager.Update(user);
                }

                var resultGrabar = _usuarioManager.GrabarUsuarioLogueado(usuario);

                if (resultGrabar)
                {
                    ViewBag.MensajeResult = "Usario actualizado satisfactoriamente";
                    RegistrarAccion((byte)AccionSesion.Actualizar, (byte)ObjetoSesion.EdicionDatos, usuario.UsuarioKey);
                    return View(usuario);
                }

                ModelState.AddModelError("", new Exception("El usuario no se encontro en la base de datos."));
                ViewBag.MensajeResult = "No se pudo actualizar el registro.";
                return View(usuario);
            }
            ViewBag.MensajeResult = "La información enviada es no valida.";
            return View(usuario);
        }

        [HttpPost]
        public JsonResult Eliminar(List<int> ids)
        {
            try
            {
                _usuarioManager.EliminarUsuarios(ids);
                ids.ForEach(i => RegistrarAccion((byte)AccionSesion.Eliminar, (byte)ObjetoSesion.ListaUsuarios, i));
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json(true);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var userIdentity = await UserManager.FindByName(model.UserName).GenerateUserIdentityAsync(UserManager);
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    RegistrarAccion((byte)AccionSesion.InicioSesion, (byte)ObjetoSesion.General, null);
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Usuario o clave incorrectos.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        //[AllowAnonymous]
        //public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        //{
        //    // Require that the user has already logged in via username/password or external login
        //    if (!await SignInManager.HasBeenVerifiedAsync())
        //    {
        //        return View("Error");
        //    }
        //    return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //
        // POST: /Account/VerifyCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // The following code protects for brute force attacks against the two factor codes. 
        //    // If a user enters incorrect codes for a specified amount of time then the user account 
        //    // will be locked out for a specified amount of time. 
        //    // You can configure the account lockout settings in IdentityConfig
        //    var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(model.ReturnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid code.");
        //            return View(model);
        //    }
        //}

        //
        // GET: /Account/Register
        public ActionResult Register()
        {
            ConfigurarMenues();            
            ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion");
            ViewBag.RoleName = new SelectList(RoleManager.Roles.Where(R => !R.Name.Equals("Gestante")).ToList(), "Name", "Name");
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.CrearUsuario, null);
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                };
                try
                {
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                        var roleResult = await UserManager.AddToRoleAsync(user.Id, model.RoleName);
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", roleResult.Errors.First());
                            ViewBag.RoleName = new SelectList(RoleManager.Roles.Where(R => !R.Name.Equals("Gestante")).ToList(), "Name", "Name");
                            return View();
                        }

                        var usuario = new Usuario
                        {
                            Id = user.Id,
                            Nombres = model.Nombres,
                            APaterno = model.APaterno,
                            AMaterno = model.AMaterno,
                            EstadoId = model.EstadoId,
                            RecibeAlertas = model.RecibeAlertas,
                            EstablecimientoId = model.EstablecimientoId,
                            UsuarioDireccion = model.UsuarioDireccion,
                            UsuarioEditor = 1,
                            Eliminado = false
                        };

                        var userLogId = "1";
                        if (Request.IsAuthenticated)
                        {
                            userLogId = User.Identity.GetUserId();
                        }

                        var usuarioKey = _usuarioManager.AddUsuario(usuario, userLogId);
                        RegistrarAccion((byte)AccionSesion.Crear, (byte)ObjetoSesion.CrearUsuario, usuarioKey);
                   
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index");
                    }
                    AddErrors(result);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            ConfigurarMenues(); 
            ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion");
            ViewBag.RoleName = new SelectList(RoleManager.Roles.Where(R => !R.Name.Equals("Gestante")).ToList(), "Name", "Name");
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        //[AllowAnonymous]
        //public async Task<ActionResult> ConfirmEmail(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return View("Error");
        //    }
        //    var result = await UserManager.ConfirmEmailAsync(userId, code);
        //    return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}

        //
        // GET: /Account/ForgotPassword
        //[AllowAnonymous]
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //
        // POST: /Account/ForgotPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await UserManager.FindByNameAsync(model.Email);
        //        if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        //        {
        //            // Don't reveal that the user does not exist or is not confirmed
        //            return View("ForgotPasswordConfirmation");
        //        }

        //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //        // Send an email with this link
        //        // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //        // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
        //        // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //        // return RedirectToAction("ForgotPasswordConfirmation", "Account");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/ForgotPasswordConfirmation
        //[AllowAnonymous]
        //public ActionResult ForgotPasswordConfirmation()
        //{
        //    return View();
        //}

        //
        // GET: /Account/ResetPassword
        //[AllowAnonymous]
        //public ActionResult ResetPassword(string code)
        //{
        //    return code == null ? View("Error") : View();
        //}

        //
        // POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await UserManager.FindByNameAsync(model.Email);
        //    if (user == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    AddErrors(result);
        //    return View();
        //}

        //
        // GET: /Account/ResetPasswordConfirmation
        //[AllowAnonymous]
        //public ActionResult ResetPasswordConfirmation()
        //{
        //    return View();
        //}

        //
        // POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        //
        // GET: /Account/SendCode
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        //{
        //    var userId = await SignInManager.GetVerifiedUserIdAsync();
        //    if (userId == null)
        //    {
        //        return View("Error");
        //    }
        //    var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
        //    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //
        // POST: /Account/SendCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SendCode(SendCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    // Generate the token and send it
        //    if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
        //    {
        //        return View("Error");
        //    }
        //    return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        //}

        //
        // GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //        case SignInStatus.Failure:
        //        default:
        //            // If the user does not have an account, then prompt the user to create an account
        //            ViewBag.ReturnUrl = returnUrl;
        //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //    }
        //}

        //
        // POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Login");
        }

        //
        // GET: /Account/ExternalLoginFailure
        //[AllowAnonymous]
        //public ActionResult ExternalLoginFailure()
        //{
        //    return View();
        //}

        public ActionResult GetReniecData(string dni)
        {
            return RedirectToAction(actionName: "ConsumeReniecApi",
                controllerName: "Reniec",
                routeValues: new { numDni = dni });
        }

        public ActionResult CambiarPassword()
        {
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.EdicionPassword, null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CambiarPassword(CambiarPasswordViewModel model)
        {
            ConfigurarMenues();
            if (!ModelState.IsValid)
            {
                ViewBag.MensajeResult = "La información enviada no es valida.";
                return View();
            }

            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.MensajeResult = "El usuario no esta logueado.";
                return View();
            }

            var user = await UserManager.FindAsync(User.Identity.Name, model.PasswordAnterior);
            if (user == null)
            {
                ViewBag.MensajeResult = "La contraseña es incorrecta.";
                return View();
            }

            var result = UserManager.ChangePassword(user.Id, model.PasswordAnterior, model.Password);
            if (result == null)
            {
                ViewBag.MensajeResult = "No se pudo actualizar la contraseña, intente nuevamente.";
                return View();
            }

            ViewBag.MensajeResult = "La contraseña fue actualizada.";
            RegistrarAccion((byte)AccionSesion.Actualizar, (byte)ObjetoSesion.EdicionPassword, null);
            return View();
        }

        public int ActualizarPermisos(List<Dictionary<string, string>> jsonData)
        {
            var opcionesAdmin = (from item in jsonData where item.ContainsKey(GlobalConstants.AdminMenuId) select item).ToList<Dictionary<string, string>>();
            var opcionesPersonal = (from item in jsonData where item.ContainsKey(GlobalConstants.PerMenuId) select item).ToList<Dictionary<string, string>>();
            var opcionesAnalista = (from item in jsonData where item.ContainsKey(GlobalConstants.AnMenuId) select item).ToList<Dictionary<string, string>>();
            var opcionesGestante = (from item in jsonData where item.ContainsKey(GlobalConstants.GesMenuId) select item).ToList<Dictionary<string, string>>();

            try
            {
                var db = new TelemonitoreoEntities();

                var rolAdmin = db.Roles.Find(1);
                rolAdmin.Menu.Clear();

                foreach (var menu in opcionesAdmin.Select(item => Convert.ToInt32(item[GlobalConstants.AdminMenuId])).Select(idMenu => db.Menus.Find(idMenu)))
                {
                    rolAdmin.Menu.Add(menu);
                }

                var rolPersonal = db.Roles.Find(2);
                rolPersonal.Menu.Clear();

                foreach (var menu in opcionesPersonal.Select(item => Convert.ToInt32(item[GlobalConstants.PerMenuId])).Select(idMenu => db.Menus.Find(idMenu)))
                {
                    rolPersonal.Menu.Add(menu);
                }

                var rolAnalista = db.Roles.Find(3);
                rolAnalista.Menu.Clear();

                foreach (var menu in opcionesAnalista.Select(item => Convert.ToInt32(item[GlobalConstants.AnMenuId])).Select(idMenu => db.Menus.Find(idMenu)))
                {
                    rolAnalista.Menu.Add(menu);
                }

                var rolGestante = db.Roles.Find(4);
                rolGestante.Menu.Clear();

                foreach (var menu in opcionesGestante.Select(item => Convert.ToInt32(item[GlobalConstants.GesMenuId])).Select(idMenu => db.Menus.Find(idMenu)))
                {
                    rolGestante.Menu.Add(menu);
                }

                db.SaveChanges();
                RegistrarAccion((byte)AccionSesion.Actualizar, (byte)ObjetoSesion.RolesUsuario, null);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        } 

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}