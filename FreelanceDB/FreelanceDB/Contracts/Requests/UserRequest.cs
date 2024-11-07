namespace FreelanceDB.Contracts.Requests
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
