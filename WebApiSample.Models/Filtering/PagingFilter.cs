using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSample.Models.Filtering
{
    public abstract class PagingFilter
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

        protected PagingFilter()
        {
            _pageNo = 1;
            _pageSize = 10;
        }
    }
}
