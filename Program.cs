// See https://aka.ms/new-console-template for more information
Console.WriteLine("Let's Go! Let's Go!");
var movies = MovieDataFactory.CreateMovieUploadObjects();

foreach (var movie in movies)
{
  await new MovieApi().ProcessMovie(movie);
}