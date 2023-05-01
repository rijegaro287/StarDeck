using Stardeck.Models;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Stardeck.Logic
{
    public class AvatarLogic
    {
        private readonly StardeckContext context;


        public AvatarLogic(StardeckContext context)
        {
            this.context = context;
        }


        public List<Avatar> GetAll()
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
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var avatar = context.Avatars.Find(id);

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
                Id=avatar.Id,
                Name=avatar.Name,
                Image=avatar.Image,

            };
            context.Avatars.Add(avatarAux);
            return avatarAux;

        }

        public Avatar UpdateAvatar(long id, Avatar nAvatar)
        {
            var avatar = context.Avatars.Find(id);
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
            var avatar = context.Avatars.Find(id);
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
