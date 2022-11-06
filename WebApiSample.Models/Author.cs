using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSample.Models
{
    public class Author : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
