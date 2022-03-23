using Screechr.Core.Enums;
using System;

namespace Screechr.Dtos.Entities
{
    public record  UserDto
    {
        public ulong Id { get; init; }
        public string UserName { get; init; }
        public string FirstName { get; init;  }
        public string LastName { get; init; }
        public string Secret { get; init; }
        public string ProfileImageUrl { get; init; }
        public DateTime CreatedOn { get; init; }
        public DateTime ModifiedOn { get; init; }
    }

    public record UserResult
    {
        public UserDto User { get; init; }
        public OperationResult Status { get; init; }
    }

    public record UpdateProfileResult
    {
        public ulong UserId { get; init; }
        public OperationResult Status { get; init; }
    }

    public record UserLoginResult
    {
        public OperationResult Status { get; init; }
        public UserLoginDetails User { get; init; }
    }

    public record UserLoginDetails
    {
        public ulong Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Secret { get; init; }

    }
}
