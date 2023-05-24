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

        public Avatar GetAvatar(long id)
        {
            var avatar = context.Avatars.Find(id);

            if (avatar == null)
            {
                return null;
            }
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
