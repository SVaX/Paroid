using System;
using System.Collections.Generic;

namespace Пароид.Models
{
    public partial class Wanted
    {
        public int WantedId { get; set; }

        public int IdUser { get; set; }

        public int IdApp { get; set; }

        public DateTime DateAdded { get; set; }

        public virtual Application IdAppNavigation { get; set; } = null!;

        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
