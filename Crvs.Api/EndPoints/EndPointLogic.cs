using Crvs.Core.Contracts;
using Crvs.Core.Features.BirthRegistration.Commands;
using Crvs.Core.Features.BirthRegistration.Queries;
using Crvs.Core.Utils;
using Crvs.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Crvs.Api.EndPoints
{
    public static class BirthRegistrationEndPoints
    {

        private static async Task<Results<Ok<BirthRegistrationDto>, NotFound<Error>>> GetBirthRegistrationDetails(
            Guid id, 
            [FromServices] ISender mediatR,
            [AsParameters] BirthRegistrationServices services
            )
        {
            services.Logger.LogInfo("Get birth registration details for {BirthRegistrationId}", id);
            BirthRegistrationDetailsRequest request = new(id);
            var result = await mediatR.Send(request);
            return result.IsSuccess == true ? TypedResults.Ok(result.Data) : TypedResults.NotFound(result.ErrorDetails);
        }

        private static async Task<Results<Created<BirthRegistrationDto>,ProblemHttpResult>> AddBirthRegistration(
            CreateBirthRegistrationCommand request,
            [AsParameters] BirthRegistrationServices services
            )
        {
            services.Logger.LogInfo("New birth registration request {@Payload}", request);
            var result = await services.Mediator.Send(request);
            return result.IsSuccess? TypedResults.Created($"/birth-registrations/{result.Data!.Id}",result.Data):
                                     TypedResults.Problem(new() { Detail = result.ErrorDetails.Description, Title=result.ErrorDetails.Code});
        }
        public static IEndpointRouteBuilder RegisterBirthRegistrationEndPoints(this IEndpointRouteBuilder app)
        {
            var endpointsGroup = app.MapGroup("/birth-registrations");

            endpointsGroup.MapGet("/{id:guid}", GetBirthRegistrationDetails)
                        .WithOpenApi(config => new(config)
                        {
                            Description = "Get details of a birth registration given a valid record Id."
                        });

            endpointsGroup.MapPost("/", AddBirthRegistration)
                        .WithOpenApi(openapi => new(openapi)
                        {
                            Description = "Creates a new birth regisration", 
                        });
            return app;
        }
    }

    public class BirthRegistrationServices
    {
        private readonly IAppLogger<BirthRegistrationServices> _logger;
        private readonly IMediator _mediator;
        public BirthRegistrationServices(
            IAppLogger<BirthRegistrationServices> logger, 
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public IAppLogger<BirthRegistrationServices> Logger => _logger;

        public IMediator Mediator => _mediator;
    }

}
