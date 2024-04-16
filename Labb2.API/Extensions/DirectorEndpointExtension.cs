using Labb2.DataAccess.Entities;
using Labb2.DataAccess.Repository;
using Labb2.Shared.Interfaces;

namespace Labb2.API.Extensions;

public static class DirectorEndpointExtension
{
	public static IEndpointRouteBuilder MapDirectorEndpoint(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("directors");

		group.MapGet("/", GetAllDirectors);

		group.MapGet("/{id:int}", GetDirectorById);

		group.MapGet("/{firstname}", GetDirectorByFirstName);

		group.MapGet("/{lastname}", GetDirectorByLastName);

		group.MapPost("/", AddDirector);

		group.MapPut("/update/{id}", UpdateDirector);

		group.MapDelete("/{id}", RemoveDirector);

		return app;
	}

	private static async Task<IResult> GetAllDirectors(IDirectorService<Director> repo)
	{
		var allDirectors = await repo.GetAllDirectors();
		return Results.Ok(allDirectors);
	}

	private static async Task<IResult> GetDirectorById(IDirectorService<Director> repo, int id)
	{
		var director = await repo.GetDirectorById(id);
		return Results.Ok(director);
	}

	private static async Task<IResult> GetDirectorByFirstName(IDirectorService<Director> repo, string firstname)
	{
		var director = await repo.GetDirectorByFirstName(firstname);
		return Results.Ok(director);
	}

	private static async Task<IResult> GetDirectorByLastName(IDirectorService<Director> repo, string lastname)
	{
		var director = await repo.GetDirectorByLastName(lastname);
		return Results.Ok(director);
	}

	private static async Task<IResult> AddDirector(IDirectorService<Director> repo, Director newDirector)
	{
		var existingDirector = await repo.GetDirectorById(newDirector.Id);
		if (existingDirector is not null)
		{
			return Results.BadRequest($"Director with Id: {newDirector.Id} already exists");
		}

		await repo.AddDirector(newDirector);
		return Results.Ok();
	}

	private static async Task<IResult> UpdateDirector(IDirectorService<Director> repo, IMovieService<Movie> movieRepo, Director updateDirector ,int id)
	{
		var oldDirector = await repo.GetDirectorById(id);
		if (oldDirector is null)
		{
			return Results.BadRequest($"Director with Id: {id} does not exist");
		}


		if (!string.IsNullOrEmpty(updateDirector.FirstName))
		{
			oldDirector.FirstName = updateDirector.FirstName;
		}

		if (!string.IsNullOrEmpty(updateDirector.LastName))
		{
			oldDirector.LastName = updateDirector.LastName;
		}

		oldDirector.Movies.Clear();
		foreach (var movie in updateDirector.Movies)
		{
			var replacement = await movieRepo.GetMovieById(movie.Id);
			oldDirector.Movies.Add(replacement);
		}
		await repo.UpdateDirector(updateDirector, id);
		return Results.Ok();
	}

	private static async Task<IResult> RemoveDirector(IDirectorService<Director> repo, int id)
	{
		var existingDirector = await repo.GetDirectorById(id);
		if (existingDirector is null)
		{
			return Results.BadRequest($"Director with Id: {id} doesn't exists");
		}

		await repo.RemoveDirector(id);
		return Results.Ok();
	}
}