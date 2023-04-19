using System;
using System.Collections.Generic;
using System.Text;

namespace Пароид.Models
{
    public class Application
    {
        public int AppId { get; set; }

        public string Name { get; set; }

        public byte[] Picture { get; set; }

        public string Description { get; set; }

        public byte[] File { get; set; }

        public int Rating { get; set; }

        public int Cost { get; set; }
    }
}
