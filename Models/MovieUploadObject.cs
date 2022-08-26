public class VideoTimeStamp
{
  public string? Description { get; set; }
  public string? TimeStamp { get; set; }
}

public class MovieUploadObject
{
  public string? Title { get; set; }

  public string? FilePath { get; set; }
  public string? Url { get; set; }

  public List<VideoTimeStamp>? VideoTimeStamps { get; set; }

  public override string ToString()
  {
    return $"{Title} - {Url} - {FilePath}, {VideoTimeStamps?.Count}";
  }
}