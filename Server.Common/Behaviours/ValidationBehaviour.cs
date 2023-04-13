using FluentValidation;
using MediatR;
using Server.Common.Enums;
using Server.Common.Errors;
using Server.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Api.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if (failures.Count != 0)
                {
                    var errors = failures.Select(x => new RestError(x.ErrorCode, x.PropertyName, x.AttemptedValue != null ? $"{x.ErrorMessage} Attempted value: '{x.AttemptedValue}'" : x.ErrorMessage)).ToArray();
                    var restError = new RestError(RestErrorCode.ValidationError,
                        typeof(TRequest).Name, "One or many validation errors occured.", errors);
                    throw new RestException(HttpStatusCode.BadRequest, restError);
                }
            }

            return await next();
        }
    }
}
