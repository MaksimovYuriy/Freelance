namespace FreelanceDB.RabbitMQ
{
    public interface IRabbitMqService
    {
        void SendMessage(object obj);
        void SendCreateUserMessage(string message);
        void SendCreateTaskMessage(long id, int price);
    }
}
