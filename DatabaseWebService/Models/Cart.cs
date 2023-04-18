using System;
using System.Collections.Generic;

namespace DatabaseWebService;

public partial class Cart
{
    public int CartId { get; set; }

    public int IdUser { get; set; }

    public int IdApp { get; set; }

    public virtual Application IdAppNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
