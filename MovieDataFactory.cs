public class MovieDataFactory
{
  public static List<MovieUploadObject> CreateMovieUploadObjects(string filePath = "")
  {
    var movieUploadObjects = new List<MovieUploadObject>();

    for (int i = 0; i < 10; i++)
    {
      var movieUploadObject = new MovieUploadObject
      {
        Title = $"Movie {i}",
        FilePath = filePath,
        VideoTimeStamps = new List<VideoTimeStamp>()
      };
      for (int j = 0; j < 10; j++)
      {
        movieUploadObject.VideoTimeStamps.Add(new VideoTimeStamp
        {
          Description = $"Description {j}",
          TimeStamp = $"00:0{j}:{100 % (j + 1)}"
        });
      }
      movieUploadObjects.Add(movieUploadObject);
    }

    return movieUploadObjects;
  }
}