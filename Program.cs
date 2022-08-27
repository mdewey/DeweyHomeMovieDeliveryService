﻿// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;

Console.WriteLine("Let's Go! Let's Go!");

var config = new ConfigurationBuilder()
      .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
      .AddUserSecrets<Program>()
      .Build();

var sourceFile = @"C:\Users\markt\Desktop\Movie Project\test\README.md";
var movies = new MovieDataFactory()
    .CreateListFromReadme(sourceFile).ToList();

foreach (var movie in movies)
{
  Console.WriteLine(movie);
  await new MovieToAws(new MovieToAwsConfig
  {
    AwsAccessKey = config["HomeMovies:aws:accessKeyId"],
    AwsSecretKey = config["HomeMovies:aws:secret"],
    AwsBucket = config["HomeMovies:aws:bucket"]
  }).UploadToAws(movie);
  Console.WriteLine(movie);
}