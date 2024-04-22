using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Core.Utils
{
    public record Error(string Code, string? Description = null)
    {
        public static Error None => new(string.Empty);
    }

    public class Result<T> where T : class
    {
        private Result(bool isSuccess, Error error, T? happyPathResult = null)
        {
            IsSuccess = isSuccess;
            ErrorDetails = error;
            Data = happyPathResult;
        }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error ErrorDetails { get; }
        public T? Data { get; }
        public static Result<T> Success(T happyPathResult) => new(true, Error.None, happyPathResult);
        public static Result<T> Failure(Error error) => new(false, error);
        //Automagically converts an object of type T into a Result<T>
        public static implicit operator Result<T>(T value)=>Success(value);      
    }
}
