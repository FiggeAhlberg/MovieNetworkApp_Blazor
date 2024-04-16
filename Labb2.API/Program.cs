using Labb2.API.Extensions;
using Labb2.DataAccess;
using Labb2.DataAccess.Entities;
using Labb2.DataAccess.Repository;
using Labb2.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MovieStoreDb");

builder.Services.AddDbContext<MovieStoreDbContext>(
	options => 
	{
		options.UseSqlServer(connectionString);
	});

//TODO: Maybe change lifetime?
builder.Services.AddScoped<IMovieService<Movie>,MovieRepository>();
builder.Services.AddScoped<IUserService<User>,UserRepository>();
builder.Services.AddScoped<IMovieListService<MovieList>,MovieListRepository>();
builder.Services.AddScoped<IDirectorService<Director>,DirectorRepository>();
builder.Services.AddScoped<IGenreService<Genre>,GenreRepository>();
builder.Services.AddScoped<IProductCategoryService<ProductCategory>, ProductCategoryRepository>();


var app = builder.Build();

app.MapProductCategoryEndpoint();
app.MapMovieEndpoint();
app.MapMovieListEndpoint();
app.MapUserEndpoint();
app.MapDirectorEndpoint();
app.MapGenreEndpoint();

app.Run();
