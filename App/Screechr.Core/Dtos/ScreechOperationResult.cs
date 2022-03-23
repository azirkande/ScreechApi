using Screechr.Core.Enums;

namespace Screechr.Core.Dtos
{
    public record ScreechOperationResult
    {
        public ulong Id { get; set; }
        public OperationResult Status { get; init; }
    }

    public record ScreechResult
    {
        public ScreechDto Screech { get; init; }
        public OperationResult Status { get; init; }
    }
}
