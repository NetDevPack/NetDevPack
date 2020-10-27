using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace NetDevPack.SpecificationResult.Result
{
    public class SpecificationValidationResult
    {
        public SpecificationValidationResult(bool isValid, IEnumerable<ValidationFailure> errors)
        {
            IsValid = isValid;
            Errors = errors?.ToList().AsReadOnly() ?? new List<ValidationFailure>().AsReadOnly();
        }

        public bool IsValid { get; protected set; }
        public IReadOnlyList<ValidationFailure> Errors { get; protected set; }
    }
}
