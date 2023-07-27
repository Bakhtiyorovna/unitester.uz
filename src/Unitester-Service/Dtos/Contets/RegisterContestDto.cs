using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unitester_Service.Dtos.Contets
{
    public class RegisterContestDto
    {
        public long contestId { get; set; }

        public long pupilId { get; set; }

        public long basicDerictionId { get; set; }

        public long secondDerictionId { get; set; }

    }
}
