using FreelanceDB.Models;

namespace FreelanceDB.Contracts.Responses.ReviewResponses
{
    public record ReviewsResponse
    (
        List<ReviewModel> reviews
    );
}
