using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            if (errors!=null)
                Errors = errors.ToArray();
        }

        public bool Succeeded { get; init; }

        public string[] Errors { get; init; }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
    public class Result<T> : Result
    {
        private Result(bool succeeded, T? data, IEnumerable<string>? errors = null)
            : base(succeeded, errors)
        {
            Data = data;
        }

        public T? Data { get; init; }

        public static Result<T> Success(T data) => new(true, data);

        public static Result<T> Failure(params string[] errors) => new(false, default, errors);

        public static Result<T> Failure(IEnumerable<string> errors) => new(false, default, errors);

        public override string ToString() =>
            Succeeded
                ? $"Succeeded ({typeof(T).Name}): {Data}"
                : $"Failed: {string.Join(", ", Errors)}";
    }
}

