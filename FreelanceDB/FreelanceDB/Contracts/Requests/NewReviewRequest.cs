namespace FreelanceDB.Contracts.Requests
{
    public record NewReviewRequest
    (
        string Description,
        int Rate,
        long AuthorId,
        long RecipientId
    );
}
