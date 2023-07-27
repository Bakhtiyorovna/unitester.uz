using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unitester_Domain.Exceptions.Contests;

public class ContestNotFoundException:NotFoundException
{
    public ContestNotFoundException()
    {
        TitleMessage = "Musobaqa topilmadi!";
    }

}
