using BlazorApp8Wasm.Models;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

public class GitHubHttpClient
{
    private readonly HttpClient _client; 

    public GitHubHttpClient(HttpClient client)
    {
        _client = client; 
    }

    public async Task<List<Commit>> GetCommits(string repositoryOwner, string repositoryName)
    {
        var response = await _client.GetAsync($"/repos/{repositoryOwner}/{repositoryName}/commits");
        response.EnsureSuccessStatusCode();
        var commits = await response.Content.ReadFromJsonAsync<List<Commit>>();

        // Enrich commits with message and lines changed data
        foreach (var commit in commits)
        {
            var commitDetails = await _client.GetFromJsonAsync<CommitDetails>($"/repos/{repositoryOwner}/{repositoryName}/commits/{commit.Sha}");
            commit.Message = commitDetails.Commit.Message;
            commit.LinesChanged = commitDetails.Stats.Total;
        }

        return commits;
    }
}
