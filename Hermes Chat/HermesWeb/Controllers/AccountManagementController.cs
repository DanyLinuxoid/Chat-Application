using HermesLogic.Features.AccountManagement.Interfaces;
using HermesModels.MVC;
using HermesWeb.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HermesWeb.Controllers
{
    /// <summary>
    /// Controller for account managing, for user.
    /// </summary>
    [Authorize] 
    public partial class AccountManagementController : HermesApplicationController
    {
        /// <summary>
        /// Core logic for management.
        /// </summary>
        private readonly IAccountManagementLogic _accountManagementLogic;

        /// <summary>
        /// Controller for account managing, for user.
        /// </summary>
        public AccountManagementController(IAccountManagementLogic accountManagementLogic)
        {
            _accountManagementLogic = accountManagementLogic;
        }

        /// <summary>
        /// Main page of account management.
        /// </summary>
        public virtual IActionResult Index() => View(MVC.AccountManagement.Views.Account, new UserAccountManagementViewModel());

        /// <summary>
        /// Updates user account details.
        /// </summary>
        /// <param name="accountDetails">Account details model to update.</param>
        [HttpPost]
        [AjaxModelStateFilter]
        public async virtual Task<IActionResult> Account(UserProfileModel accountDetails)
        {
            accountDetails.UploadedFiles = Request.Form.Files;
            accountDetails.AspNetUserId = CurrentUserId;
            await _accountManagementLogic.UpdateUserAccountDetails(accountDetails);
            return HermesSimpleOkResult();
        }

        /// <summary>
        /// Gets current user account details for management page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<IActionResult> GetCurrentAccountDetails() 
        {
            var result = await _accountManagementLogic.GetUserProfileModelByUserIdAsync(CurrentUserId);
            return PartialView(MVC.AccountManagement.Views.Partial._AccountDetails, result);
        }
    }
}