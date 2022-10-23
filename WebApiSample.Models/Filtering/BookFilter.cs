using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSample.Models.Filtering
{
    public class BookFilter : PagingFilter
    {
        public string Title { get; set; }
    }
}
