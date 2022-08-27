using System.Web;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;

public class MovieToAwsConfig
{
  public string? AwsAccessKey { get; set; }
  public string? AwsSecretKey { get; set; }
  public string? AwsBucket { get; set; }

}


public class MovieToAws
{

  public string? AwsAccessKey { get; set; }
  public string? AwsSecretKey { get; set; }
  public string? AwsBucket { get; set; }

  public MovieToAws(MovieToAwsConfig config)
  {
    AwsAccessKey = config.AwsAccessKey;
    AwsSecretKey = config.AwsSecretKey;
    AwsBucket = config.AwsBucket;
  }

  public async Task<MovieUploadObject> UploadToAws(MovieUploadObject movie)
  {
    if (movie == null || movie.FilePath == null)
    {
      throw new ArgumentNullException(nameof(movie));
    }

    Console.WriteLine("sending file to AWS");
    var accessKeyID = this.AwsAccessKey;
    var secretAccessKey = this.AwsSecretKey;
    var bucketName = this.AwsBucket;
    var region = Amazon.RegionEndpoint.USEast1;
    var creds = new BasicAWSCredentials(accessKeyID, secretAccessKey);
    using (var client = new AmazonS3Client(creds, region))
    {
      Console.WriteLine("Created S3 Client");

      Console.WriteLine("sending file to AWS");
      Console.WriteLine(movie);
      using (var ms = new MemoryStream())
      {
        using (FileStream file = new FileStream(movie.FilePath, FileMode.Open, FileAccess.Read))
        {
          file.CopyTo(ms);

          var uploadRequest = new TransferUtilityUploadRequest
          {
            InputStream = ms,
            Key = movie.FileName,
            BucketName = bucketName // bucket name of S3
          };

          var fileTransferUtility = new TransferUtility(client);
          await fileTransferUtility.UploadAsync(uploadRequest);
          movie.Url = $"https://{bucketName}.s3.amazonaws.com/{HttpUtility.UrlEncode(uploadRequest.Key)}";
        }
      }
    }

    return movie;
  }


}