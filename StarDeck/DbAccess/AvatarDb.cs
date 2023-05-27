using Stardeck.Models;

namespace Stardeck.DbAccess
{
    public class AvatarDb
    {
        private readonly StardeckContext context;

        public AvatarDb(StardeckContext context)
        {
            this.context = context;
        }

        public List<Avatar> GetAllAvatars()
        {
            List<Avatar> avatars = context.Avatars.ToList();
            if (avatars.Count == 0)
            {
                return null;
            }
            return avatars;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Avatar object or null if not found</returns>
        public Avatar? GetAvatar(long id)
        {
            var avatar = context.Avatars.Find(id);
            return avatar;

        }


        public Avatar NewAvatar(Avatar av)
        {
            context.Avatars.Add(av);
            context.SaveChanges();

            if (GetAvatar(av.Id) == null)
            {
                return null;
            }

            return av;

        }

        public Avatar DeleteAvatar(long id)
        {
            var avatar = GetAvatar(id);
            if (avatar != null)
            {
                context.Remove(avatar);
                context.SaveChanges();
                return avatar;
            }
            return null;
        }


    }
}
