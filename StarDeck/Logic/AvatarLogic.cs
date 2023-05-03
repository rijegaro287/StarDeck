using Stardeck.Models;

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

        public Avatar newAvatar(long id, Avatar nAvatar)
        {
            var av = context.Avatars.Find(id);
            if (av != null)
            {
                av.Id = nAvatar.Id;
                av.Image = nAvatar.Image;
                av.Name = nAvatar.Name;
                context.SaveChanges();
                return av;
            }
            return null;

        }

        public Avatar deleteAvatar(long id)
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
