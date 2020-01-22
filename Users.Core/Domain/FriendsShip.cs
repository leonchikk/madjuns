using Common.Core.Models;
using System;

namespace Users.Core.Domain
{
    public class FriendsShip : BaseEntity
    {
        protected FriendsShip() { }
        public FriendsShip(Guid iAmId, Guid myFriendId)
        {
            IAmId = iAmId;
            MyFriendId = myFriendId;
        }

        public virtual Guid IAmId { get; set; }
        public virtual Guid MyFriendId { get; set; }

        public virtual User IAm { get; set; }
        public virtual User MyFriend { get; set; }
    }
}
