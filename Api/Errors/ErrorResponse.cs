using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Errors
{
    public class ErrorResponse
    {
        public ErrorCode Code { get; }

        public IEnumerable<ApiError> Errors { get; }


        public ErrorResponse(ErrorCode code, IEnumerable<ApiError> errors)
        {
            Code = code;
            Errors = errors;
        }

        public ErrorResponse(ErrorCode code, string errorMessage)
        {
            Code = code;
            Errors = new List<ApiError>() { new ApiError(errorMessage) };
        }

        public ErrorResponse(ModelStateDictionary modelState)
        {
            Code = ErrorCode.ValidationFailed;
            // TODO: check
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }
}
