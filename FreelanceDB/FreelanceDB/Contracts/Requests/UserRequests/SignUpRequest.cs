namespace FreelanceDB.Contracts.Requests.UserRequests
{
    public record SignUpRequest
    (
        string Name,
        string Login,
        string password
    );
}
