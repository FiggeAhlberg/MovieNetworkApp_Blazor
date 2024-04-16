using Labb2.DataAccess.Entities;
using Labb2.DataAccess.Repository;
using Labb2.Shared.DTOs;
using Labb2.Shared.Interfaces;

namespace Labb2.API.Extensions;

public static class UserEndpointExtension
{
	public static IEndpointRouteBuilder MapUserEndpoint(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("users");

		group.MapGet("/", GetAllUsers);

		group.MapGet("/{id}", GetUserById);

		group.MapGet("/{email}", GetUserByEmail);

		group.MapPost("/", AddUser);

		group.MapPut("/update/{id}", UpdateUser);

		group.MapDelete("/{id}", RemoveUser);

		return app;
	}

	

	private static async Task<IResult> GetAllUsers(IUserService<User> repo)
	{
		var users = await repo.GetAllUsers();
		return Results.Ok(users);
	}

	private static async Task<IResult> GetUserById(IUserService<User> repo, int id)
	{
		var user = await repo.GetUserById(id);

		if (user is null)
		{
		   return Results.NotFound($"No user found with Id: {id}");
		}

		return Results.Ok(user);
	}

	private static async Task<IResult> GetUserByEmail(IUserService<User> repo, string email)
	{
		var user = await repo.GetUserByEmail(email);

		if (user is null)
		{
			return Results.NotFound("No user found with this id");
		}

		return Results.Ok(user);
	}

	private static async Task<IResult> AddUser(IUserService<User> repo, UserDto newUser)
	{
		var userToAdd = new User();
		userToAdd.UserName = newUser.UserName;
		userToAdd.FirstName = newUser.FirstName;
		userToAdd.LastName = newUser.LastName;
		userToAdd.Email = newUser.Email;
		userToAdd.Password = newUser.Password;

		await repo.AddUser(userToAdd);
		return Results.Ok();
	}

	private static async Task<IResult> UpdateUser(IUserService<User> repo, User updatedUser, int id)
	{
		var oldUser = await repo.GetUserById(id);
		if (oldUser is null)
		{
			return Results.NotFound($"User with Id {id} doesn't exist");
		}

		oldUser.UserName = updatedUser.UserName;
		oldUser.FirstName = updatedUser.FirstName;
		oldUser.LastName = updatedUser.LastName;
		oldUser.Email = updatedUser.Email;
		oldUser.MovieLists = updatedUser.MovieLists;
		oldUser.Password = updatedUser.Password;
		await repo.UpdateUser(oldUser);
		return Results.Ok();
	}

	private static async Task<IResult> RemoveUser(IUserService<User> repo, int id)
	{
		var existingUser = await repo.GetUserById(id);

		if (existingUser is null)
		{
			return Results.BadRequest($"User with Id: {id} doesn't exists");
		}

		await repo.RemoveUser(id);
		return Results.Ok();
	}

}