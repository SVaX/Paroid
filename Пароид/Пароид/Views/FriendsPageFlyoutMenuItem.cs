﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Пароид.Views
{
    public class FriendsPageFlyoutMenuItem
    {
        public FriendsPageFlyoutMenuItem()
        {
            TargetType = typeof(FriendsPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}