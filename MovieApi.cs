using System.Text.Json;

public class MovieApi
{
  // static readonly HttpClient client = new HttpClient();

  public string MovieApiRoot { get; set; } = "https://localhost:7200/api/movies";


  public async Task UploadMetaDataToApi(MovieUploadObject movie)
  {
    Console.WriteLine("sending meta data to API");
    Console.WriteLine(movie);
    try
    {
      //TODO: remove before sending to production
      var httpClientHandler = new HttpClientHandler();
      httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
      {
        return true;
      };
      var client = new HttpClient(httpClientHandler);
      var content = new StringContent(JsonSerializer.Serialize(movie), System.Text.Encoding.UTF8, "application/json");
      var response = await client.PostAsync(this.MovieApiRoot, content);
      response.EnsureSuccessStatusCode();
      string responseBody = await response.Content.ReadAsStringAsync();
      Console.WriteLine(responseBody);
    }
    catch (HttpRequestException e)
    {
      Console.WriteLine("\nException Caught!");
      Console.WriteLine("Message :{0} ", e.Message);
      Console.WriteLine("InnerException :{0} ", e.InnerException);
      Console.WriteLine(movie);
    }
  }

  public async Task ProcessMovie(MovieUploadObject movie)
  {
    await UploadMetaDataToApi(movie);
  }
}