namespace FreelanceDB.Contracts.Requests.UserRequests
{
    public record LoginRequest
    (
        string login,
        string password
    );
}
