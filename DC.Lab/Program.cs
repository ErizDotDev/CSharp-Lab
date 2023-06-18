using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Octokit;
using System.Runtime.CompilerServices;

namespace DC.Lab;

public class GraphQLRequest
{
    [JsonProperty("query")]
    public string? Query { get; set; }

    [JsonProperty("variables")]
    public IDictionary<string, object> Variables { get; } = new Dictionary<string, object>();

    public string ToJsonText() =>
        JsonConvert.SerializeObject(this);
}

class Program
{
    private const string PagedIssueQuery =
@"query ($repo_name: String!, $start_cursor: String) {
    repository(owner: ""dotnet"", name: $repo_name) {
        issues(last: 25, before: $start_cursor)
        {
            totalCount
            pageInfo {
                hasPreviousPage
                startCursor
            }
            nodes {
                title
                number
                createdAt
            }
        }
    }
}";

    private class ProgressStatus : IProgress<int>
    {
        Action<int> action;

        public ProgressStatus(Action<int> progressAction) =>
            this.action = progressAction;

        public void Report(int value) => action(value);
    }

    // <SnippetStarterAppMain>
    static async Task Main(string[] args)
    {
        var key = GetEnvironmentVariable("GitHubKey",
            "You must store your GitHub key in the 'GitHubKey' environment variable",
            string.Empty);

        var client = new GitHubClient(new Octokit.ProductHeaderValue("IssueQueryDemo"))
        {
            Credentials = new Octokit.Credentials(key)
        };

        int num = 0;
        var cancellation = new CancellationTokenSource();
        await foreach (var issue in RunPagedQueryAsync(client, PagedIssueQuery, "docs")
            .WithCancellation(cancellation.Token))
        {
            Console.WriteLine(issue);
            Console.WriteLine($"Received {++num} issues in total");
        }
    }
    // </SnippetStarterAppMain>

    static string GetEnvironmentVariable(string item, string error, string defaultValue)
    {
        var value = Environment.GetEnvironmentVariable(item, EnvironmentVariableTarget.Machine);

        if (string.IsNullOrWhiteSpace(value))
        {
            if (!string.IsNullOrWhiteSpace(defaultValue))
                return defaultValue;

            if (!string.IsNullOrWhiteSpace(error))
            {
                Console.WriteLine(error);
                Environment.Exit(0);
            }
        }

        return value ?? string.Empty;
    }

    // <SnippetRunPagedQuery>
    private static async IAsyncEnumerable<JToken> RunPagedQueryAsync(GitHubClient client, string queryText,
        string repoName, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var issueAndPRQuery = new GraphQLRequest
        {
            Query = queryText
        };

        issueAndPRQuery.Variables["repo_name"] = repoName;

        bool hasMorePages = true;
        int pagesReturned = 0;
        int issuesReturned = 0;

        // Stop with 10 pages because there are larger repos:
        while (hasMorePages && (pagesReturned++ < 10))
        {
            var postBody = issueAndPRQuery.ToJsonText();
            var response = await client.Connection.Post<string>(new Uri("https://api.github.com/graphql"),
                postBody, "application/json", "application/json");

            JObject results = JObject.Parse(response.HttpResponse.Body.ToString()!);

            int totalCount = (int)issues(results)["totalCount"]!;
            hasMorePages = (bool)pageInfo(results)["hasPreviousPage"]!;
            issueAndPRQuery.Variables["start_cursor"] = pageInfo(results)["startCursor"]!.ToString();
            issuesReturned += issues(results)["nodes"]!.Count();

            foreach (JObject issue in issues(results)["nodes"]!)
                yield return issue;
        }

        JObject issues(JObject result) => (JObject)result["data"]!["repository"]!["issues"]!;
        JObject pageInfo(JObject result) => (JObject)issues(result)["pageInfo"]!;
    }
    // </SnippetRunPagedQuery>
}