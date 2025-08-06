using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeSureProject.Models
{
    public class InstagramProfile
    {
        public string Cid { get; set; }
        public string SocialType { get; set; }
        public string GroupID { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string ScreenName { get; set; }
        public long UsersCount { get; set; }
        public string CommunityStatus { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsClosed { get; set; }
        public bool Verified { get; set; }
    }

}