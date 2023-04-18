using System;
using System.Collections.Generic;

namespace DatabaseWebService;

public partial class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Balance { get; set; }

    public string PermissionLevel { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Friendship> FriendshipIdUser1Navigations { get; set; } = new List<Friendship>();

    public virtual ICollection<Friendship> FriendshipIdUser2Navigations { get; set; } = new List<Friendship>();

    public virtual ICollection<Library> Libraries { get; set; } = new List<Library>();

    public virtual ICollection<Wanted> Wanteds { get; set; } = new List<Wanted>();
}
