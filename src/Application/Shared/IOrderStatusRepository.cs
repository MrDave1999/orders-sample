namespace Application.Shared;

public interface IOrderStatusRepository
{
    Task<bool> IsInvalidAsync(int statusId);
}
