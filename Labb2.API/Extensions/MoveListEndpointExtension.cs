using System.Net;
using Labb2.DataAccess.Entities;
using Labb2.DataAccess.Repository;
using Labb2.Shared.DTOs;
using Labb2.Shared.Interfaces;

namespace Labb2.API.Extensions;

public static class MoveListEndpointExtension
{
	public static IEndpointRouteBuilder MapMovieListEndpoint(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("movieLists");

		group.MapGet("/", GetAllMovieLists);

		group.MapGet("/{id:int}", GetMovieListById);

		group.MapGet("/{name}", GetMovieListByName);

		group.MapPost("/", AddMovieList);

		group.MapPatch("/addMovies", AddMovieToMovieList);

		group.MapPut("/update/{id}", UpdateMovieList);

		group.MapDelete("/{id}", RemoveMovieList);

		group.MapDelete("/{listId}/{movieId}", RemoveMovieFromMovieList);

		return app;
	}

	private static async Task<IResult> GetAllMovieLists(IMovieListService<MovieList> repo)
	{
	  var movieLists =	await repo.GetAllMovieLists();
	  return Results.Ok(movieLists);
	}

	private static async Task<IResult> GetMovieListById(IMovieListService<MovieList> repo, int id)
	{
		var movieList = await repo.GetMovieListById(id);

		if (movieList is null)
		{
			return Results.NotFound($"No movie list found with Id: {id}");
		}

		return Results.Ok(movieList);
	}

	private static async Task<IResult> GetMovieListByName(IMovieListService<MovieList> repo, string name)
	{
		var movieList = await repo.GetMovieListByName(name);

		if (movieList is null)
		{
		   return Results.NotFound($"No movie found with this name: {name}");
		}

		return Results.Ok(movieList);
	}

	private static async Task<IResult> AddMovieList(IMovieListService<MovieList> repo, AddMovieListDto newMovieList, IUserService<User> uRepo)
	{
		var movieListToAdd = new MovieList();
		movieListToAdd.Name = newMovieList.Name;
		var userId = await uRepo.GetUserById(newMovieList.UserId);
		movieListToAdd.User = userId;

		await repo.AddMovieList(movieListToAdd);
		return Results.Ok();
	}


	private static async Task AddMovieToMovieList(IMovieListService<MovieList> mlRepo, AddMovieToMovieListDto dto)
	{
		await mlRepo.AddMovieToMovieList(dto.MovieListId, dto.MovieId);
	}



	private static async Task<IResult> UpdateMovieList(IMovieService<Movie> movieRepo, IMovieListService<MovieList> repo, MovieList updatedMovieList, int id)
	{
		var oldMovieList = await repo.GetMovieListById(id);
		if (oldMovieList is null)
		{
			return Results.NotFound($"Movie with Id {id}");
		}

		if (!string.IsNullOrEmpty(updatedMovieList.Name))
		{
			oldMovieList.Name = updatedMovieList.Name;
		}

		oldMovieList.Movies.Clear();
		foreach (var movie in updatedMovieList.Movies)
		{
			var replacement = await movieRepo.GetMovieById(movie.Id);
			oldMovieList.Movies.Add(replacement);
		}

		await repo.UpdateMovieList(oldMovieList, id); //ska det vara såhär??

		return Results.Ok();

	}

	private static async Task<IResult> RemoveMovieList(IMovieListService<MovieList> repo, int id)
	{
		var existingMovieList = await repo.GetMovieListById(id);

		if (existingMovieList is null)
		{
		   return Results.BadRequest($"Movie with Id: {id} doesn't exists");
		}

		await repo.RemoveMovieList(id);
		return Results.Ok();
	}

	private static async Task<IResult> RemoveMovieFromMovieList(IMovieListService<MovieList> mlRepo, int movieId, int listId)
	{
		await mlRepo.RemoveMovieFromMovieList(listId, movieId);
		return Results.Ok();
	}

}