using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.APIs.Dtos
{
    public class PeoductToReturnDto
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public int BrandId { get; set; } 

        public string Brand { get; set; } 


        public int CategoryId { get; set; }
        public string Category { get; set; }

    }
}
