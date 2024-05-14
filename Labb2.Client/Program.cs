using Labb2.Client.Components;
using Labb2.Client.Services;
using Labb2.Shared.DTOs;
using Labb2.Shared.Interfaces;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddHttpClient("movieNetworkApi",
	client =>
		client.BaseAddress = new Uri("https://localhost:7229")
);

builder.Services.AddScoped<IUserService<UserDto>, UserService>();
builder.Services.AddScoped<IMovieService<MovieDto>, MovieService>();
builder.Services.AddScoped<IGenreService<GenreDto>,  GenreService>();
builder.Services.AddScoped<IDirectorService<DirectorDto>,  DirectorService>();
builder.Services.AddScoped<IMovieListService<AddMovieListDto>,  MovieListService>();
builder.Services.AddScoped<ITmdbService<TmdbMovieDto>, TmdbService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
