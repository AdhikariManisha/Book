﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.UserAccounts;
public interface IUserAccountServices
{
    Task<bool> RegisterAsync(UserRegisterDto dto);
    Task<bool> LoginAsync(UserLoginDto dto);
    Task<bool> ForgetPasswordAsync(UserForgetPasswordDto dto);
    Task<bool> ChangePasswordAsync(UserChangePasswordDto dto);
}
