using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopArt.Classes
{
    public class Item
    {
        public string name { get; set; }
        public string size { get; set; }
        public double price { get; set; }
        public string level { get; set; }
        public Item()
        {
            this.name = string.Empty;
            this.size = string.Empty;
            this.price = 0;
            this.level = string.Empty;
        }
    }
}
