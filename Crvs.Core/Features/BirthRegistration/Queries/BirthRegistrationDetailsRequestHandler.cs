using Crvs.Core.Contracts;
using Crvs.Core.Utils;
using Crvs.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthRegistrationEntity = Crvs.Domain.Entities.BirthRegistration;
namespace Crvs.Core.Features.BirthRegistration.Queries
{
    public class BirthRegistrationDetailsRequestHandler : IRequestHandler<BirthRegistrationDetailsRequest,Result<BirthRegistrationDto>>
    {
        private readonly IGenericRepository<BirthRegistrationEntity> _repository;

        public BirthRegistrationDetailsRequestHandler(IGenericRepository<BirthRegistrationEntity> repository)
        {
            _repository = repository;
        }

        public async Task<Result<BirthRegistrationDto>> Handle(BirthRegistrationDetailsRequest request, CancellationToken cancellationToken)
        {
           var entity = await _repository.GetByIdAsync(request.Id,cancellationToken);
            if(entity is null)
            {
                return Result<BirthRegistrationDto>.Failure(BirthRegistrationErrors.ItemNotFound);
            }
            //Result<BirthRegistrationDto>.Success((BirthRegistrationDto)entity);
            return ((BirthRegistrationDto)entity);
        }
    }
}
