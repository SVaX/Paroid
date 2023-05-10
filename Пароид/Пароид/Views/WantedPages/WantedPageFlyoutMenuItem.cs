using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Пароид.Views
{
    public class WantedPageFlyoutMenuItem
    {
        public WantedPageFlyoutMenuItem()
        {
            TargetType = typeof(WantedPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}