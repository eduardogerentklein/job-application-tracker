using System.Text.Json.Serialization;

namespace Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ApplicationStatus
{
    Applied = 1,
    Interview = 2,
    Offer = 3,
    Accepted = 4,
    Rejected = 5
};