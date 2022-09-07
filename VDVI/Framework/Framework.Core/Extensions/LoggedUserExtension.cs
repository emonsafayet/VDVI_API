using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Core.Extensions
{
    public static class LoggedUserExtension
    {
        public static List<LoggedUser> LoggedUser = new List<LoggedUser>();

        public static void AddOrUpdateLoggedUser(this LoggedUser loggedUser) {
            var existsUser = LoggedUser.Find(x => x.UserId == loggedUser.UserId);
            if (existsUser == null)
                LoggedUser.Add(loggedUser);
            //else
            //    LoggedUser.Where(u => u.UserId == loggedUser.UserId).ToList().ForEach(x => x.LastAccessTime = loggedUser.LastAccessTime);
        }

        public static void RemoveLoggedUser(this LoggedUser loggedUser) {
            LoggedUser.RemoveAll(x => x.UserId == loggedUser.UserId);
        }

        public static void RemoveLoggedUserByUserId(int userId)
        {
            LoggedUser.RemoveAll(x => x.UserId == userId);
        }

        public static List<LoggedUser> GetLoggedUsers() {

            return LoggedUser;
        }

        public static List<LoggedUser> GetLoggedUsersByCompanyId(this LoggedUser loggedUser)
        {

            return LoggedUser.Where(x=> x.CompanyId == loggedUser.CompanyId).ToList();
        }

        public static bool IsValidSession(this LoggedUser loggedUser)
        {
            return true;

            //NEED TO WORK. NOT WORKING NOW
            //int countActiveUser = LoggedUser.Where(x => x.CompanyId == loggedUser.CompanyId).ToList().Count;
            //var user = LoggedUser.Find(x => x.UserId == loggedUser.UserId);

            //int numberOfMaxSession = loggedUser.NoOfMaxSession == 0? user?.NoOfMaxSession ?? 0 : loggedUser.NoOfMaxSession ;
            ////int numberOfMaxSession = loggedUser.NoOfMaxSession;

            //if (numberOfMaxSession > countActiveUser && user == null)
            //{
            //    loggedUser.AddOrUpdateLoggedUser();
            //}
            //else if (numberOfMaxSession >= countActiveUser && user != null)
            //{
            //    loggedUser.AddOrUpdateLoggedUser();
            //}
            //else {
            //    return false;
            //}



            //return true;
        }

    }

    public class LoggedUser {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public DateTime LoggedTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public string UserName { get; set; }
        public int NoOfMaxSession { get; set; }
    }
}
