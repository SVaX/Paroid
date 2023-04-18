using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Пароид.Views
{
    public class FriendsRequestPageFlyoutMenuItem
    {
        public FriendsRequestPageFlyoutMenuItem()
        {
            TargetType = typeof(FriendsRequestPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}