using AutoMapper;
using Book.Application.Contracts.Repositories;
using Book.Application.Contracts.Users;
using Book.Domain.Entities.Identity;
using Book.Shared.Constants;
using Book.Shared.Options;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Book.Application.Users;
public class UserService : IUserService
{
    private readonly UserManager<BookUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly JWTOption _jwtOption;

    public UserService(UserManager<BookUser> userManager, IMapper mapper, IConfiguration configuration, IOptions<JWTOption> jwtOption)
    {
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration;
        _jwtOption = jwtOption.Value;
    }

    public async Task<bool> ChangePasswordAsync(UserChangePasswordDto dto)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var sql = "select * from AspNetUsers";
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
        var sql = "select * from AspNetUsers";

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
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user == null)
        {
            throw new Exception("Invalid Username/Password");
        }

        bool isValidPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!isValidPassword)
        {
            throw new Exception("Invalid Username/Password.");
        }

        var authClaims = new List<Claim> { 
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
                issuer: _jwtOption.Issuer,
                audience: _jwtOption.Audience,
                expires: DateTime.Now.AddMinutes(_jwtOption.ExpiresInMins),
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Secret)), SecurityAlgorithms.HmacSha256)
            );

        return new TokenDto(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
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
}
