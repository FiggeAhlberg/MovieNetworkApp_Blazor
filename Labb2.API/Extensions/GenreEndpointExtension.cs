using Labb2.DataAccess.Entities;
using Labb2.DataAccess.Repository;
using Labb2.Shared.Interfaces;

namespace Labb2.API.Extensions;

public static class GenreEndpointExtension
{
	public static IEndpointRouteBuilder MapGenreEndpoint(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("genres");

		group.MapGet("/", GetAllGenres);

		group.MapGet("/{id:int}", GetGenreById);

		return app;
	}

	private async static Task<IResult> GetAllGenres(IGenreService<Genre> repo)
	{
		var allGenres = await repo.GetAllGenres();
		return Results.Ok(allGenres);
	}
	private async static Task<IResult> GetGenreById(IGenreService<Genre> repo, int id)
	{
		var genre = await repo.GetGenreById(id);
		return Results.Ok(genre);
	}

	
}