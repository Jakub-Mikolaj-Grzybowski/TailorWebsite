using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;

namespace TailorWebsite.Services.ConcreteServices
{
    public class AccountSettingsService : IAccountSettingsService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountSettingsService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
            }
            return result;
        }

        public async Task<IdentityResult> SetPhoneNumberAsync(User user, string phoneNumber)
        {
            return await _userManager.SetPhoneNumberAsync(user, phoneNumber);
        }

        public async Task<IdentityResult> DeleteAccountAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
            }
            return result;
        }
    }
}
