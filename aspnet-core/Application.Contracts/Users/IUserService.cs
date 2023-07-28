using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Users;
public interface IUserService
{
    Task<bool> RegisterAsync(UserRegisterDto dto);
    Task<TokenDto> LoginAsync(UserLoginDto dto);
    Task<bool> ForgetPasswordAsync(UserForgetPasswordDto dto);
    Task<bool> ChangePasswordAsync(UserChangePasswordDto dto);
    Task<List<UserDto>> GetListAsync();
}
