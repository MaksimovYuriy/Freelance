namespace FreelanceDB.Contracts.Requests.UserRequests
{
    public record UserRequest
    (
        string Name,
        string Login,
        string password,
        string Balance,
        string FreezeBalance
    );
}
