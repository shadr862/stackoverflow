using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.StackOverflow.Votes
{
    public interface IVoteAppService :
        ICrudAppService<
            VoteDto,
            Guid,
            ListRequestDto,
            CreateUpdateVoteDto,
            CreateUpdateVoteDto>
    {
        // Additional custom methods can go here if needed
    }
}
