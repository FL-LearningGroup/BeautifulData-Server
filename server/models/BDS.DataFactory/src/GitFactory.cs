namespace BDS.DataFactory
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using LibGit2Sharp;
    public class GitFactory : IGitFactory
    {
        private string _local;
        private string _remote;
        private string _userName;
        private string _password;
        private string _email;
        public GitFactory(string local, string remote, string userName, string password, string email)
        {
            _local = local;
            _remote = remote;
            _userName = userName;
            _password = password;
            _email = email;
        }
        public string Local
        {
            get { return _local; }
            set { _local = value; }
        }

        public string Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public bool PullRepository()
        {
            if (!Directory.Exists(_local))
            {
                Directory.CreateDirectory(_local);
                try
                {
                    Repository.Clone(_remote, _local);
                    return true;
                }
                catch (Exception ex)
                {
                    Directory.Delete(_local);
                    throw new GitFactoryException("Pull Repo", ex.Message);
                }
            }
            else
            {
                if (Repository.IsValid(_local))
                {
                    return true;
                }
                else
                {
                    throw new GitFactoryException(String.Format("Local Repo: {0}", _local), "Local Repository is inValid");
                }
            }

        }

        public System.Int32 AddStageChanges()
        {
            using (Repository repo = new Repository(_local))
            {
                try
                {
                    RepositoryStatus status = repo.RetrieveStatus();
                    //List<string> filePaths = status.Modified.Select(mods => mods.FilePath).ToList();
                    List<string> filePaths = status.Select(mods => mods.FilePath).ToList();
                    if (filePaths.Count == 0)
                    {
                        return 0;
                    }
                    Commands.Stage(repo, filePaths);
                    return filePaths.Count;
                }
                catch (Exception ex)
                {
                    throw new GitFactoryException(String.Format("Local Repo: {0}", _local), String.Format("Add stage changes failed, {0}", ex.Message));
                }
            }
        }
        public bool CommitChanges(string message)
        {
            using (Repository repo = new Repository(_local))
            {
                try
                {
                    repo.Commit(message, new Signature(_userName, _email, DateTimeOffset.Now),
                        new Signature(_userName, _email, DateTimeOffset.Now));
                    return true;

                }
                catch (Exception ex)
                {
                    throw new GitFactoryException(String.Format("Local Repo: {0}", _local), String.Format("Commit stage changes failed, {0}", ex.Message));
                }
            }
        }

        public bool PushRepository()
        {
            using (Repository repo = new Repository(_local))
            {
                try
                {
                    Remote remote = repo.Network.Remotes["origin"];
                    PushOptions options = new PushOptions();
                    options.CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials { Username = _userName, Password = _password };
                    var pushRefSpec = @"refs/heads/master";
                    repo.Network.Push(remote, pushRefSpec, options);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new GitFactoryException(String.Format("Local Repo: {0}, Remote Repo: {1}", _local, _remote), String.Format("Push repo failed, {0}", ex.Message));
                }
            }
        }
    }
}
