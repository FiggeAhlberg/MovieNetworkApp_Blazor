using System;
using Labb2.DataAccess.Entities;
using Labb2.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labb2.DataAccess.Repository;

public class UserRepository : IUserService<User>
{
    private readonly MovieStoreDbContext _context;

	public UserRepository(MovieStoreDbContext context)
	{
		_context = context;
	}


   

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users.FindAsync(email);
    }

    public async Task AddUser(User newUser)
    {
	    await _context.Users.AddAsync(newUser);
	    await _context.SaveChangesAsync();
    }

	public async Task UpdateUser(User updatedUser)
    {
	    _context.Users.Update(updatedUser);
        await _context.SaveChangesAsync();

    }


    public async Task RemoveUser(int id)
    {
	    var removeUser = await _context.Users.FindAsync(id);
	    _context.Users.Remove(removeUser);
        await _context.SaveChangesAsync();
    }

}