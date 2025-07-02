namespace H2.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? FullName { get; }
        string? DeviceId { get; }
    }
}
