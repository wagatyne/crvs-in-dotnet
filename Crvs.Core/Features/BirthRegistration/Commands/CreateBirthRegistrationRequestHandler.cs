using Crvs.Core.Contracts;
using Crvs.Domain.DTOs;
using Crvs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthRegistrationEntity = Crvs.Domain.Entities.BirthRegistration;
namespace Crvs.Core.Features.BirthRegistration.Commands
{
    public class CreateBirthRegistrationRequestHandler :
        IRequestHandler<CreateBirthRegistrationCommand, Utils.Result<BirthRegistrationDto>>
    {
        private readonly IGenericRepository<BirthRegistrationEntity> _repository;
        private readonly IAppLogger<CreateBirthRegistrationRequestHandler> _logger;

        public CreateBirthRegistrationRequestHandler(
            IGenericRepository<BirthRegistrationEntity> repository,
            IAppLogger<CreateBirthRegistrationRequestHandler> logger
            )
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Utils.Result<BirthRegistrationDto>> Handle(CreateBirthRegistrationCommand request, CancellationToken cancellationToken)
        {
            BirthRegistrationEntity birthRegistrationEntity = new() { 
                Applicant = request.Child,
                Father = (Father)request.Father,
                Mother = (Mother)request.Mother,
            };
            //Validation goes here
            await _repository.AddAsync(birthRegistrationEntity, cancellationToken);

            return (BirthRegistrationDto)birthRegistrationEntity;
        }
    }
}
