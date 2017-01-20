using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Salary.Interfaces
{
    public interface ICustomFilter
    {
        DateTime GetDate();
        DateTime GetDateBegin();
        DateTime GetDateEnd();
        Decimal? GetSubdivID();
        Decimal[] GetDegreeIDs();
    }
}
