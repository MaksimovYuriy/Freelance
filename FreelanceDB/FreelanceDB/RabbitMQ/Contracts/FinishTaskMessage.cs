namespace FreelanceDB.RabbitMQ.Contracts
{
    public record FinishTaskMessage
    (
        long AuthorId,
        decimal Price,
        long WorkerId
    );
}
