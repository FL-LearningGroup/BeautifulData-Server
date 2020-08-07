using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LibGit2Sharp;
using System.Linq;
using MailKit;

namespace BDS.DotNetCoreKnowledage
{
    public class OperateGit
    {
        static int StageChanges(Repository repo)
        {
            try
            {
                RepositoryStatus status = repo.RetrieveStatus();
                //List<string> filePaths = status.Modified.Select(mods => mods.FilePath).ToList();
                List<string> filePaths = status.Select(mods => mods.FilePath).ToList();
                if(filePaths.Count == 0)
                {
                    return 0;
                }
                Commands.Stage(repo, filePaths);
                return filePaths.Count;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:RepoActions:StageChanges " + ex.Message);
                return 0;
            }
        }
        static void CommitChanges(Repository repo, string username, string email)
        {
            try
            {
                repo.Commit("updating files..", new Signature(username, email, DateTimeOffset.Now),
                    new Signature(username, email, DateTimeOffset.Now));

            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:RepoActions:CommitChanges " + ex.Message);
            }
        }
        static void PushChanges(Repository repo, string username, string password)
        {
            try
            {
                Remote remote = repo.Network.Remotes["origin"];
                PushOptions options = new PushOptions();
                options.CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials { Username = username, Password = password };
                var pushRefSpec = @"refs/heads/master";
                repo.Network.Push(remote, pushRefSpec, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:RepoActions:PushChanges " + ex.Message);
            }
        }
        static void Main_Stop(string[] args)
        {
            Console.WriteLine("Start process");
            string gitLocalPath = @"D:\OperateGitTest\LibGit2SharpTest";

            //delete directory need folder access
            if (!Directory.Exists(gitLocalPath))
            {
                Directory.CreateDirectory(gitLocalPath);
                Repository.Clone("https://github.com/LucasYao93-DataBase/BDS-Data-FuYang.git", gitLocalPath);
            }
            //Create projecr folder for everyone
            using (Repository repo = new Repository(gitLocalPath))
            {
                if (StageChanges(repo) != 0)
                {
                    CommitChanges(repo, "BDS", "BDS@outlook.com");
                }
                else
                {
                    Console.WriteLine("WARNING: No files changed.");
                }
                PushChanges(repo, "LucasYao93-DataBase", "yaodi@960903");
            }

            Console.WriteLine("Please enter key to end the process");
            Console.ReadKey();
        }
    }
}
