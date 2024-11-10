namespace FreelanceDB.Contracts.Requests
{
    public record SignUpRequest
    (
        string Name,
        string Login,
        string password
    );
}
