using System.Text.Json.Serialization;

namespace BlazorApp8Wasm.Models;


public class Commit
{
    public string Sha { get; set; }
    public string Message { get; set; }
    public int LinesChanged { get; set; }
    [JsonPropertyName("commit")]
    public CommitInfo CommitInfo { get; set; }
}

public class CommitDetails
{
    public Commit Commit { get; set; }
    public Stats Stats { get; set; }
}

public class Stats
{
    public int Total { get; set; }
}
