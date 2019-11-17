using System;
using System.Collections.Generic;
using Octokit;
using Pretty.Services.Settings;

namespace Pretty.Services.Githubs
{
    public class GithubService : IGitHubService
    {
        private IGitHubClient _client;
        private ISettingService _settingService;

        public GithubService(
            IGitHubClient client, 
            ISettingService settingService)
        {
            _client = client;
            _settingService = settingService;

            Authorize(
                _client, 
                _settingService.GetValue("github_account"),
                _settingService.GetValue("github_pwd"));
        }


        public IEnumerable<Repository> GetMyselfRepos()
        {
            return _client.Repository.GetAllForCurrent().Result;
        }

        public IEnumerable<Repository> GetRepository(string nickname)
        {
            return _client.Repository.GetAllForUser(nickname).Result;
        }

        #region Methods

        private void Authorize(IGitHubClient client, string account,string pwd)
        {
            (_client as GitHubClient).Credentials = new Credentials(account, pwd);
        }

        private bool IsAuth()
        {
            return (_client as GitHubClient).Credentials != null;
        }

        #endregion
    }
}
