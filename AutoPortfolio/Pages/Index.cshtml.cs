using Microsoft.AspNetCore.Mvc;
using AutoPortfolio.Services;
using AutoPortfolio.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoPortfolio.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GithubService _githubService;

        public IndexModel(GithubService githubService)
        {
            _githubService = githubService;
        }

        public List<Repository> Repositories { get; set; } = new();

        public async Task OnGetAsync()
        {
            Repositories = await _githubService.GetRepositoriesFromGithubAsync("micha1j");
        }
    }
}