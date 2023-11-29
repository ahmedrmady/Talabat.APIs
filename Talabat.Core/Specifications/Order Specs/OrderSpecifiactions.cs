using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications.Order_Specs
{
    public class OrderSpecifiactions:BaseSpecifications<Order>
    {

        public OrderSpecifiactions(string buyerEmail) 
            :base(O=>O.BuyerEmail==buyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);

            Includes.Add(O=>O.Items);

            AddOrderByDesc(O=>O.OrderDate);

        }
        public OrderSpecifiactions(int id , string buyerEmail)
            :base(O=>O.Id ==id &&O.BuyerEmail==buyerEmail)
        {
            
        }
    }
}
