using FluentValidation.Results;

namespace NetDevPack.Messaging
{
    public class ResponseMessage : Message
    {
        public ValidationResult ValidationResult { get; set; }

        public ResponseMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public void AddError(string message)
        {
            AddError(string.Empty, message);
        }

        public void AddError(string propertyName, string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(propertyName, message));
        }
    }
}