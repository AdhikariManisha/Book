using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Users;
public interface IUserService
{
    public Task<bool> RegisterAsync(UserRegisterDto dto);
    public Task<TokenDto> LoginAsync(UserLoginDto dto);
    public Task<bool> ForgetPasswordAsync(UserForgetPasswordDto dto);
    public Task<bool> ChangePasswordAsync(UserChangePasswordDto dto);
    public Task<List<UserDto>> GetListAsync();
    public Task<UserDto> GetAsync(int id); 
    public Task<bool> UpdateAsync(int id, UserDto input);
    public Task<bool> DeleteAsync(int id);
}
