using Stardeck.DbAccess;
using Stardeck.Models;

namespace Stardeck.Logic
{
    public class AvatarLogic
    {
        private readonly StardeckContext context;
        private readonly AvatarDb avatarDB;

        public AvatarLogic(StardeckContext context)
        {
            this.context = context;
            this.avatarDB = new AvatarDb(context);
        }


        public List<Avatar> GetAll()
        {
            List<Avatar> avatars = avatarDB.GetAllAvatars();
            if (avatars.Count == 0)
            {
                return null;
            }
            return avatars;
        }

        public Avatar GetAvatar(long id)
        {
            var avatar = avatarDB.GetAvatar(id);

            if (avatar == null)
            {
                return null;
            }
            return avatar;

        }

        public Avatar NewAvatar(Avatar avatar)
        {
            var avatarAux = new Avatar()
            {
                Id = avatar.Id,
                Name = avatar.Name,
                Image = avatar.Image,

            };
            avatarDB.NewAvatar(avatarAux);
            return avatarAux;

        }

        public Avatar UpdateAvatar(long id, Avatar nAvatar)
        {
            var avatar = avatarDB.GetAvatar(id);
            if (avatar != null)
            {
                avatar.Name = nAvatar.Name;
                avatar.Image = nAvatar.Image;
                avatar.Name = nAvatar.Name;
                avatar.Image = nAvatar.Image;

                context.SaveChanges();
                return avatar;
            }
            return null;

        }


        public Avatar DeleteAvatar(long id)
        {
            var avatar = avatarDB.DeleteAvatar(id);
            if (avatar != null)
            {
                return avatar;
            }
            return null;
        }





    }
}
