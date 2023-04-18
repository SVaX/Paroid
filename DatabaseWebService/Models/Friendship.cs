using System;
using System.Collections.Generic;

namespace DatabaseWebService;

public partial class Friendship
{
    public int FriendshipId { get; set; }

    public int IdUser1 { get; set; }

    public int IdUser2 { get; set; }

    public bool Confirmed { get; set; }

    public virtual User IdUser1Navigation { get; set; } = null!;

    public virtual User IdUser2Navigation { get; set; } = null!;
}
