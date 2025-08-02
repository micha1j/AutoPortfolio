using AutoPortfolio.Data;
using AutoPortfolio.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutoPortfolio.Services
{
    public class GithubService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;

        public GithubService(HttpClient httpClient, AppDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<List<Repository>> GetRepositoriesFromGithubAsync(string githubUsername)
        {
            _httpClient.DefaultRequestHeaders.UserAgent.Clear();
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AutoPortfolio", "1.0"));

            string url = $"https://api.github.com/users/{githubUsername}/repos";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var githubRepos = JsonSerializer.Deserialize<List<GithubRepoDto>>(content, jsonOptions);

            var repositories = githubRepos.Select(repo => new Repository
            {
                Name = repo.Name,
                Description = repo.Description,
                HtmlUrl = repo.HtmlUrl,
                UpdatedAt = repo.Updated_At
            }).ToList();

            return repositories;
        }

        public async Task<List<Repository>> FetchAndSaveRepositoriesAsync(string githubUsername)
        {
            var repositories = await GetRepositoriesFromGithubAsync(githubUsername);
            _context.Repositories.AddRange(repositories);
            await _context.SaveChangesAsync();
            return repositories;
        }

        private class GithubRepoDto
        {
            public string Name { get; set; }
            public string Description { get; set; }

            [JsonPropertyName("html_url")]
            public string HtmlUrl { get; set; }

            public DateTime Updated_At { get; set; }
        }
    }
}
