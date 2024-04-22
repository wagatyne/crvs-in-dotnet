using Crvs.Domain.DTOs;
using Crvs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Core.Features.BirthRegistration.Commands
{
    public record CreateBirthRegistrationCommand(
        Person Child,
        Person Father,
        Person Mother,
        DateTime DateOfNotification
        ) : IRequest<Utils.Result<BirthRegistrationDto>>
    {
    }
}
