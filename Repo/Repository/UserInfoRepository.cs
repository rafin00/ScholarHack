using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
   public class UserInfoRepository : Repository<UserInfo>
    {
        ScholarHackDBContext context = new ScholarHackDBContext();
        public UserInfo GetByEmail(string email)
        {
            
            return context.UserInfoes.ToList().Find(us=>us.Email==email);
        }
    }
}
