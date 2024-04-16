namespace Labb2.Shared.Interfaces;

public interface IUserService<T> where T : class
{
	Task<IEnumerable<T>> GetAllUsers();
	Task<T?> GetUserById(int id);
	Task<T?> GetUserByEmail(string email);
	Task AddUser(T newUser);
	Task UpdateUser(T updatedUser);
	Task RemoveUser(int id);
}