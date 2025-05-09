using Common;

namespace Domain.Errors
{
    public static class JobApplicationErrors
    {
        public static Error CreateValidationFailed() => Error.Validation(
            "JobApplication.CreateValidationFailed",
            $"Failed to create a Job Application. Please contact the administrator.");

        public static Error UpdateValidationFailed() => Error.Validation(
            "JobApplication.UpdateValidationFailed",
            $"Failed to update a Job Application. Please contact the administrator.");

        public static Error DeleteValidationFailed() => Error.Validation(
            "JobApplication.DeleteValidationFailed",
            $"Failed to delete a Job Application. Please contact the administrator.");

        public static Error NotFound(Guid applicationId) => Error.NotFound(
            "JobApplication.NotFound",
            $"The Job Application with the Id = '{applicationId}' was not found");
    }
}
