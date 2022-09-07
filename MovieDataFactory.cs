public class MovieDataFactory
{

  public List<MovieUploadObject> CreateListFromReadme(string FilePath)
  {
    var list = new List<MovieUploadObject>();
    MovieUploadObject? bucket = null;
    foreach (string line in System.IO.File.ReadLines(FilePath))
    {
      Console.WriteLine(line);
      if (line.Contains("##"))
      {
        if (bucket != null)
        {
          list.Add(bucket);
        }
        bucket = new MovieUploadObject { Title = line.Replace("##", "").Trim() };
      }
      else if (line.Contains(">"))
      {
        // file path 
        if (bucket != null)
        {
          bucket.FilePath = line.Replace(">", "").Trim();
        }
      }
      else if (line.Contains("~~"))
      {
        // url
        if (bucket != null)
        {
          bucket.Tags = line.Replace("~~", "").Trim().Split(',').ToList();
        }
      }
      else if (line.Contains("--"))
      {
        if (bucket != null)
        {
          var split = line.Split("--");
          if (bucket.VideoTimeStamps == null)
          {
            bucket.VideoTimeStamps = new List<VideoTimeStamp>();
          }
          bucket.VideoTimeStamps.Add(new VideoTimeStamp { Description = split[1].Trim(), TimeStamp = split[0].Trim() });
        }
      }
    }
    if (bucket != null)
    {
      list.Add(bucket);
    }
    return list;
  }

  public static List<MovieUploadObject> CreateMovieUploadObjectsDummy(string filePath = "")
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