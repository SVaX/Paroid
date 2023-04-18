using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Пароид.Views
{
    public class AdminCommentsPageFlyoutMenuItem
    {
        public AdminCommentsPageFlyoutMenuItem()
        {
            TargetType = typeof(AdminCommentsPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}