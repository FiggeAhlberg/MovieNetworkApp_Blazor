using Labb2.DataAccess.Entities;
using Labb2.DataAccess.Repository;
using Labb2.Shared.DTOs;
using Labb2.Shared.Interfaces;

namespace Labb2.API.Extensions;

public static class MovieEndpointExtension
{
	public static IEndpointRouteBuilder MapMovieEndpoint(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("movies");

		group.MapGet("/", GetAllMovies);

		group.MapGet("/{id}", GetMovieById);

		group.MapGet("title/{title}", GetMovieByTitle);

		//group.MapGet("/genre/{genreId}", GetMoviesByGenre);

		group.MapPost("/", AddMovie);

		group.MapPut("/update/{id}", UpdateMovie);
		
		group.MapDelete("/{id}", RemoveMovie);

		return app;
	}

	

	private static async Task<IResult> GetAllMovies(IMovieService<Movie> repo)
	{
		var allMovies = await repo.GetAllMovies();
		return Results.Ok(allMovies);
	}

	//ProductCategory
	

	private static async Task<IResult> GetMovieById(IMovieService<Movie> repo, int id)
	{
		var movie = await repo.GetMovieById(id);

		if (movie is null)
		{
			return Results.NotFound($"No movie found with Id: {id}");
		}

		return Results.Ok(movie);
	}

	private static async Task<IResult> GetMovieByTitle(IMovieService<Movie> repo, string title)
	{
		var movie = await repo.GetMovieByTitle(title);

		if (movie is null)
		{
			return Results.NotFound("No movie found with this id");
		}

		return Results.Ok(movie);
	}

	//private static async Task<IResult> GetMoviesByGenre(IMovieService<Movie> repo, int genreId)
	//{
	//	var movies = await repo.GetMovieByGenre(genreId);


	//	return Results.Ok(movies);
	//}

	private static async Task<IResult> AddMovie(IMovieService<Movie> mRepo ,IDirectorService<Director> dirRepo, IGenreService<Genre> genRepo, MovieDto newMovie, IProductCategoryService<ProductCategory> prRepo)
	{
		var movieToAdd = new Movie();
		movieToAdd.Title = newMovie.Title;
		movieToAdd.Description = newMovie.Description;
		movieToAdd.Duration = newMovie.Duration;

		var productCategory = await prRepo.GetProductCategoryById(newMovie.ProductCategoryId);
		movieToAdd.ProductCategory = productCategory;

		var dir = await dirRepo.GetDirectorById(newMovie.DirectorId);
		movieToAdd.Director = dir;

		foreach (var genreId in newMovie.GenreIds)
		{
			var genres = await genRepo.GetGenreById(genreId);

			movieToAdd.Genres.Add(genres);
		}

		movieToAdd.PosterImageUrl = newMovie.PosterImageUrl;
		movieToAdd.BackgroundImageUrl = newMovie.BackgroundImageUrl;
		

		await mRepo.AddMovie(movieToAdd);
		return Results.Ok();
	}

	

	private static async Task<IResult> UpdateMovie(IMovieService<Movie> repo, Movie updatedMovie, int id)
	{
		var oldMovie = await repo.GetMovieById(id);
		if (oldMovie is null)
		{
			return Results.NotFound($"Movie with Id {id}");
		}

		oldMovie.Title = updatedMovie.Title;
		oldMovie.Description = updatedMovie.Description;
		oldMovie.Duration = updatedMovie.Duration;
        oldMovie.Genres = updatedMovie.Genres;
		oldMovie.Director = updatedMovie.Director;
		oldMovie.ProductCategory = updatedMovie.ProductCategory;
		oldMovie.PosterImageUrl = updatedMovie.PosterImageUrl;
		oldMovie.BackgroundImageUrl = updatedMovie.BackgroundImageUrl;
		await repo.UpdateMovie(oldMovie, id); //ska det vara såhär??
		return Results.Ok();

	}


	private static async Task<IResult> RemoveMovie(IMovieService<Movie> repo, int id)
	{
		var existingMovie = await repo.GetMovieById(id);

		if (existingMovie is null)
		{
		   return Results.NotFound($"Movie with Id: {id} doesn't exists");
		}

		await repo.RemoveMovie(id);
		return Results.Ok();
	}

}