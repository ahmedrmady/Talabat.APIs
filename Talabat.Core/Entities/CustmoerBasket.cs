using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class CustmoerBasket
    {

        public string Id {  get; set; }

        public List<BasketItem> Items { get; set; }
        public CustmoerBasket(string id)
        {
            Id = id;
        }
    }
}
