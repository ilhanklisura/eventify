namespace Eventify.Backend.Services.Default;

using Eventify.Backend.Common.Auth.Hash;
using Eventify.Backend.Mapping;
using Eventify.Backend.Models.Data;
using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Models.Request.User;
using Eventify.Backend.Models.Response.User;
using Eventify.Backend.Services.Result;
using Microsoft.EntityFrameworkCore;

public class UserService : Service, IUserService
{
    private readonly DataContext _db;
    private readonly IHashProvider _hash;
    private readonly IMapper<User, UserModel> _mapper;

    public UserService(IServiceProvider sp, DataContext db, IHashProvider hash, IMapper<User, UserModel> mapper) : base(sp)
    {
        _db = db;
        _hash = hash;
        _mapper = mapper;
    }

    public ServiceResult<UserModel> GetByEmail(string email)
    {
        var u = _db.Users.AsNoTracking().FirstOrDefault(x => x.Email == email);
        return u == null ? NotFound() : Ok(_mapper.Map(u));
    }

    public ServiceResult<UserModel> GetById(int id)
    {
        var u = _db.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
        return u == null ? NotFound() : Ok(_mapper.Map(u));
    }

    public ServiceResult<List<UserModel>> GetAll()
    {
        var list = _db.Users.AsNoTracking().OrderBy(x => x.Name).ToList().Select(_mapper.Map).ToList();
        return Ok(list);
    }

    public ServiceResult<UserModel> Create(CreateUserRequestModel model)
    {
        if (_db.Users.Any(x => x.Email == model.Email))
            return ExistingEntity("Email");
        var roleName = model.Role ?? "attendee";
        var role = _db.Roles.FirstOrDefault(r => r.Name == roleName);
        if (role == null) role = _db.Roles.First(r => r.Name == "attendee");

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Password = _hash.HashPassword(model.Password),
            Role = roleName
        };
        _db.Users.Add(user);
        _db.SaveChanges();
        _db.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
        _db.SaveChanges();
        return Ok(_mapper.Map(user));
    }

    public ServiceResult<UserModel> Update(UpdateUserRequestModel model)
    {
        var user = _db.Users.Find(model.Id);
        if (user == null) return NotFound();
        user.Name = model.Name;
        user.Email = model.Email;
        user.Role = model.Role;
        if (!string.IsNullOrEmpty(model.Password))
            user.Password = _hash.HashPassword(model.Password);
        _db.SaveChanges();
        return Ok(_mapper.Map(user));
    }

    public ServiceResult Delete(int id)
    {
        var user = _db.Users.Find(id);
        if (user == null) return NotFound();
        _db.Users.Remove(user);
        _db.SaveChanges();
        return Ok();
    }

    public ServiceResult CheckPassword(string email, string password)
    {
        var u = _db.Users.FirstOrDefault(x => x.Email == email);
        if (u == null) return NotFound();
        return _hash.VerifyPassword(password, u.Password) ? Ok() : ValidationError("Invalid password");
    }

    public ServiceResult<bool> RecordLogin(string email)
    {
        var user = _db.Users.FirstOrDefault(x => x.Email == email);
        if (user == null) return NotFound();

        var isFirstLogin = user.LastLoginAt == null;
        user.LastLoginAt = DateTime.UtcNow;
        _db.SaveChanges();

        return Ok(isFirstLogin);
    }

}
