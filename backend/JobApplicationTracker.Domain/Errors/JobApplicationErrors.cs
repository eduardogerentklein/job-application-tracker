using Common;

namespace Domain.Errors
{
    public static class JobApplicationErrors
    {
        public static Error CreateValidationFailed(string reason = "One or more validations failed.") => Error.Failure(
            "JobApplication.CreateValidationFailed",
            $"Failed to create a Job Application. Reason: {reason}");

        public static Error NotFound(Guid applicationId) => Error.NotFound(
            "JobApplication.NotFound",
            $"The Job Application with the Id = '{applicationId}' was not found");
    }
}
