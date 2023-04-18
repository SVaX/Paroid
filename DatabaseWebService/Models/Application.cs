using System;
using System.Collections.Generic;

namespace DatabaseWebService;

public partial class Application
{
    public int AppId { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Picture { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte[] File { get; set; } = null!;

    public int Rating { get; set; }

    public int Cost { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Library> Libraries { get; set; } = new List<Library>();

    public virtual ICollection<Wanted> Wanteds { get; set; } = new List<Wanted>();
}
