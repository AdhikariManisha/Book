using AutoMapper;
using Book.Application.Contracts.Repositories;
using Book.Application.Contracts.Users;
using Book.Domain.Entities.Identity;
using Book.Shared.Constants;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Book.Application.Users;
public class UserService : IUserService
{
    private readonly UserManager<BookUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(UserManager<BookUser> userManager, IMapper mapper, IConfiguration configuration)
    {
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<bool> ChangePasswordAsync(UserChangePasswordDto dto)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        //var sql = "select * from AspNetUsers where UserName = '" + dto.UserName + "'";
        var sql = "exec";
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

    public async Task<bool> LoginAsync(UserLoginDto dto)
    {
        var userExists = await _userManager.FindByNameAsync(dto.UserName);
        if (userExists == null)
        {
            throw new Exception("Invalid Username/Password");
        }

        bool isValidPassword = await _userManager.CheckPasswordAsync(userExists, dto.Password);

        if (!isValidPassword)
        {
            throw new Exception("Invalid Username/Password.");
        }
        return true;
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
