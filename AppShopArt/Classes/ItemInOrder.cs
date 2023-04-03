using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopArt.Classes
{
    public class ItemInOrder : Item
    {
        public int count { get; set; }
        public double amount { get; set; }
        public ItemInOrder(Classes.Item item)
        {
            this.name = item.name;
            this.size = item.size;
            this.price = item.price;
            this.level = item.level;
            this.count = 1;
            this.amount = item.price;
        }
    }
}
