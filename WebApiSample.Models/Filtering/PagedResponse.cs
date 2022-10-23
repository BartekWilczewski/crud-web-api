using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSample.Models.Filtering
{
    public class PagedResponse<T> where T : class
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public Uri NextPage { get; set; }
        public Uri PrevPage { get; set; }

        public PagedResponse(T data, int pageNo, int pageSize)
        {
            Data = data;
            Succeeded = true;
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
