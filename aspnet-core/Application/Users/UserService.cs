using AutoMapper;
using Book.Application.Contracts.Repositories;
using Book.Application.Contracts.Users;
using Book.Domain.Entities.Identity;
using Book.Shared.Constants;
using Book.Shared.Exceptions;
using Book.Shared.Options;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ExceptionServices;
using System.Security.Claims;
using System.Text;

namespace Book.Application.Users;
public class UserService : IUserService
{
    private readonly UserManager<BookUser> _userManager;
    private readonly RoleManager<BookRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserService> _logger;
    private readonly JWTOption _jwtOption;

    public UserService(
        UserManager<BookUser> userManager, 
        RoleManager<BookRole> roleManager,
        IMapper mapper, 
        IConfiguration configuration, 
        IOptions<JWTOption> jwtOption,
        ILogger<UserService> logger
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _configuration = configuration;
        _logger = logger;
        _jwtOption = jwtOption.Value;
    }

    public async Task<bool> ChangePasswordAsync(UserChangePasswordDto dto)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var sql = "select * from Users";
        using var conn = new SqlConnection(connectionString);
        var dtoUsers = (await conn.QueryAsync<UserDto>(sql)).ToList();
        return true;
    }

    public Task<bool> ForgetPasswordAsync(UserForgetPasswordDto dto)
    {
        throw new NotImplementedException();
    }

  
    public async Task<List<UserDto>> GetListAsync()
    {
        List<UserDto> dtoUsers = new();

        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var sql = "select * from Users";

        using var conn = new SqlConnection(connectionString);
        //await conn.OpenAsync();

        dtoUsers = conn.Query<UserDto>(sql).ToList();

        //using var cmd = new SqlCommand(sql, conn);
        //cmd.CommandType = System.Data.CommandType.Text;

        //using var reader = await cmd.ExecuteReaderAsync();
        //DataTable dt = new DataTable();
        //dt.Load(reader);    
        //DataSet ds = new DataSet();         
        //ds.Tables.Add(dt);

        // SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
        //sqlDataAdapter.Fill(ds);

//;        while (reader.Read())
//        {
//            var user = new UserDto();
//            user.Name = reader["Name"].ToString();
//            user.UserName = reader["UserName"].ToString();
//            user.Surname = reader["Surname"].ToString();
//            user.Email = reader["Email"].ToString();
//            user.DOB = reader.GetDateTime(reader.GetOrdinal("DOB"));
//            user.PhoneNumber = reader["PhoneNumber"].ToString();
//            dtoUsers.Add(user);
//            reader.NextResult();
//        }


        //var users = await _userManager.Users.ToListAsync();
        //var dtoUsers = _mapper.Map<List<UserDto>>(users);

        return dtoUsers;
    }

    public async Task<TokenDto> LoginAsync(UserLoginDto dto)
    {
        try
        {
            _logger.LogInformation("UserService :: LoginAsync :: Started");
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                var msg = "Invalid Username/Password";
                throw new ValidationException(msg, new List<System.ComponentModel.DataAnnotations.ValidationResult> { 
                    new System.ComponentModel.DataAnnotations.ValidationResult(msg, new []{ "username", "password"})
                });
            }

            bool isValidPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isValidPassword)
            {
                var msg = "Invalid Username/Password";
                throw new ValidationException(msg, new List<System.ComponentModel.DataAnnotations.ValidationResult> {
                    new System.ComponentModel.DataAnnotations.ValidationResult(msg, new []{ "username", "password"}),
                });
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            var authClaims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Surname, user.Surname)
        };
            authClaims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var roleName in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, roleName));
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    authClaims.AddRange(roleClaims);
                }
            }

            var token = new JwtSecurityToken(
                    issuer: _jwtOption.Issuer,
                    audience: _jwtOption.Audience,
                    expires: DateTime.Now.AddMinutes(_jwtOption.ExpiresInMins),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Secret)), SecurityAlgorithms.HmacSha256)
                );
            _logger.LogInformation("UserService :: LoginAsync :: Ended");

            return new TokenDto(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
        }
        catch (Exception ex) {
            var type = ex.GetType();
            _logger.LogInformation($"UserService :: LoginAsync :: Exception :: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> RegisterAsync(UserRegisterDto dto)
    {
        var user = _mapper.Map<BookUser>(dto);

        var userExists = await _userManager.FindByNameAsync(dto.UserName);
        if (userExists != null) {
            throw new Exception("User already exists");
        }

        await _userManager.CreateAsync(user, dto.Password);
        await _userManager.AddToRoleAsync(user, Roles.User.ToString());

        return true;
    }
  
    public async Task<UserDto> GetAsync(int id )
    {
        try
        {
            _logger.LogInformation("UserService :: GetAsync :: Started");
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                _logger.LogInformation($"UserService :: GetAsync :: Ended Not Found");
            }
            var userDto = _mapper.Map<UserDto>(user);
            _logger.LogInformation($"UserService :: GetAsync :: Ended");

            return userDto;
        }
        catch (Exception ex) {
            _logger.LogInformation($"UserService :: GetAsync :: Exception :: {ex.Message}");
            throw;
        }
    }


    public async Task<bool> UpdateAsync(int id, UserDto userDto)
    {
        try
        {
            _logger.LogInformation("UserService :: UpdateAsync :: Started");

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                var msg = "User Not Found";
                _logger.LogInformation($"UserService :: UpdateAsync ::{msg}");
                throw new Exception(msg); 
            }
             _mapper.Map(userDto, user);
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("UserService :: UpdateAsync :: User updated successfully");

            return true; 
              }
        catch (Exception ex)
        {
            _logger.LogError($"UserService :: UpdateAsync :: Exception :: {ex.Message}");
            throw; 
        }
    }


    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            _logger.LogInformation("UserService :: DeleteAsync :: Started");
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                var msg = "User Not Found";
                _logger.LogInformation($"UserService :: DeleteAsync :: {msg}");
                throw new Exception(msg);
            }
            await _userManager.DeleteAsync(user);

            _logger.LogInformation("UserService ::DeleteAsync :: Ended");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"UserService :: DeleteAsync :: Exception :: {ex.Message}");
            throw;
        }
    }
}
