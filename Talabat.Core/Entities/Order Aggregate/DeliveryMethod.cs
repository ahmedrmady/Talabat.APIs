using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class DeliveryMethod:BaseEntity
   
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string description, decimal cost, string deilveryTime)
        {
            ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = deilveryTime;
        }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string DeliveryTime { get; set; }


    }
}
