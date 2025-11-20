using System.Threading.Tasks;
using TailorWebsite.ViewModels.VM;
using Microsoft.AspNetCore.Identity;
using TailorWebsite.Model.DataModels;

namespace TailorWebsite.Services.Interface
{
    public interface IAccountSettingsService
    {
        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);
        Task<IdentityResult> SetPhoneNumberAsync(User user, string phoneNumber);
        Task<IdentityResult> DeleteAccountAsync(User user);
    }
}
