using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSample.Models.Filtering
{
    public class PagingFilter
    {
        public int PageNo
        {
            get => _pageNo ?? 1;
            set => _pageNo = value;
        }

        public int PageSize
        {
            get => _pageSize ?? 10;
            set => _pageSize = value;
        }

        private int? _pageSize;
        private int? _pageNo;

        public PagingFilter()
        {
            _pageNo = 1;
            _pageSize = 10;
        }

        public PagingFilter(int? pageNo, int? pageSize)
        {
            _pageNo = pageNo;
            _pageSize = pageSize;
        }
    }
}
