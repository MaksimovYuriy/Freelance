namespace FreelanceDB.Contracts.Requests.ReviewRequests
{
    public record NewReviewRequest
    (
        string Description,
        int Rate,
        long AuthorId,
        long RecipientId
    );
}
