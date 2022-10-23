using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSample.Models.Filtering
{
    public struct FieldFilter
    {
        public readonly string FieldName;
        public readonly FilterType FilterType;
    }
}
