using Mybrus.Models;
using Mybrus.Extensions;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TNT.Core.Identity;
using TNT.Core.Model.Identity;
using System.Collections.Generic;
using System.Net;

namespace Mybrus.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        private IApplicationUserManager _userManager;
        private IApplicationRoleManager _roleManager;

        public ManageController(IApplicationUserManager userManager, IApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public int UserId
        {
            get
            {
                var userId = User.Identity.GetUserId();
                return userId != null ? userId.Value : 0;
            }
        }

        #region Account Management
        //
        // GET: /Account/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two factor provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "The phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(UserId),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(UserId),
                Logins = await _userManager.GetLoginsAsync(UserId),
                BrowserRemembered = await _userManager.TwoFactorBrowserRememberedAsync(UserId)
            };
            return View(model);
        }

        //
        // GET: /Account/RemoveLogin
        public ActionResult RemoveLogin()
        {
            var linkedAccounts = _userManager.GetLogins(UserId);
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return View(linkedAccounts);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await _userManager.RemoveLoginAsync(UserId, new ApplicationUserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Account/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // GET: /Manage/RememberBrowser
        public ActionResult RememberBrowser()
        {
            var rememberBrowserIdentity = _userManager.CreateTwoFactorRememberBrowserIdentity(UserId);
            _userManager.SignIn(true, rememberBrowserIdentity);
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/ForgetBrowser
        public ActionResult ForgetBrowser()
        {
            _userManager.SignOut(_userManager.TwoFactorRememberBrowserCookie);
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/EnableTFA
        public async Task<ActionResult> EnableTFA()
        {
            await _userManager.SetTwoFactorEnabledAsync(UserId, true);
            var user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/DisableTFA
        public async Task<ActionResult> DisableTFA()
        {
            await _userManager.SetTwoFactorEnabledAsync(UserId, false);
            var user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Account/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Send result of: _userManager.GetPhoneNumberCodeAsync(UserId, phoneNumber);
            // Generate the token and send it
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(UserId, model.Number);
            var message = new ApplicationMessage
            {
                Destination = model.Number,
                Body = "Your security code is: " + code
            };
            await _userManager.SendSmsAsync(message);
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // GET: /Account/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            // This code allows you exercise the flow without actually sending codes
            // For production use please register a SMS provider in IdentityConfig and generate a code here.
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(UserId, phoneNumber);
            ViewBag.Status = "For DEMO purposes only, the current code is " + code ;
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Account/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userManager.ChangePhoneNumberAsync(UserId, model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // GET: /Account/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await _userManager.SetPhoneNumberAsync(UserId, null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new {Message = ManageMessageId.Error});
            }
            var user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userManager.ChangePasswordAsync(UserId, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.AddPasswordAsync(UserId, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByIdAsync(UserId);
                    if (user != null)
                    {
                        await SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Manage
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await _userManager.GetLoginsAsync(UserId);
            var otherLogins = _userManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), UserId, _userManager);
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await _userManager.GetExternalLoginInfoAsync(XsrfKey, UserId.ToString());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await _userManager.AddLoginAsync(UserId, loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        #endregion

        #region User Management

        //
        // GET: /Users/
        public async Task<ActionResult> Users()
        {
            return View(await _userManager.GetUsersAsync());
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> UserDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _userManager.FindByIdAsync(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleNames = await _userManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> UserCreate()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await _roleManager.GetRolesAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> UserCreate(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = userViewModel.Email, Email = userViewModel.Email };
                var adminresult = await _userManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await _userManager.AddUserToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await _roleManager.GetRolesAsync(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(_roleManager.GetRoles(), "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(_roleManager.GetRoles(), "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> UserEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _userManager.FindByIdAsync(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                RolesList = _roleManager.GetRoles().Select(x => new SelectListItem
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserEdit([Bind(Include = "Email,Id")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.Email;
                user.Email = editUser.Email;

                var userRoles = await _userManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await _userManager.AddUserToRolesAsync(user.Id, selectedRole.Except(userRoles).ToList());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await _userManager.RemoveUserFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToList());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> UserDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _userManager.FindByIdAsync(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserDeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var result = await _userManager.DeleteAsync(id.Value);
                if (result.Errors.Contains("Invalid user Id"))
                {
                    return HttpNotFound();
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        #endregion

        #region Role Management
        //
        // GET: /Roles/
        public ActionResult Role()
        {
            return View(_roleManager.GetRoles());
        }

        //
        // GET: /Roles/Details/5
        public async Task<ActionResult> RoleDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await _roleManager.FindByIdAsync(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }
            // Get the list of Users in this Role
            var users = new List<AppUser>();

            // Get the list of Users in this Role
            foreach (var user in _userManager.GetUsers())
            {
                if (await _userManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }

        //
        // GET: /Roles/Create
        public ActionResult RoleCreate()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> RoleCreate(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole { Name = roleViewModel.Name };
                var roleresult = await _roleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Roles/Edit/Admin
        public async Task<ActionResult> RoleEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await _roleManager.FindByIdAsync(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }
            var roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };
            return View(roleModel);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RoleEdit([Bind(Include = "Name,Id")] RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.UpdateAsync(roleModel.Id, roleModel.Name);
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Roles/Delete/5
        public async Task<ActionResult> RoleDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await _roleManager.FindByIdAsync(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RoleDeleteConfirmed(int? id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicationIdentityResult result;
                if (deleteUser != null)
                {
                    result = await _roleManager.DeleteAsync(id.Value);
                }
                else
                {
                    result = await _roleManager.DeleteAsync(id.Value);
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private async Task SignInAsync(AppUser user, bool isPersistent)
        {
            _userManager.SignOut(_userManager.ExternalCookie, _userManager.TwoFactorCookie);
            _userManager.SignIn(isPersistent, await _userManager.CreateIdentityAsync(user, _userManager.ApplicationCookie));
        }

        private void AddErrors(ApplicationIdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = _userManager.FindById(UserId);
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = _userManager.FindById(UserId);
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
            base.Dispose(disposing);
        }
    }
}