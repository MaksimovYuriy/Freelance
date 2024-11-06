namespace FreelanceDB.Contracts
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
