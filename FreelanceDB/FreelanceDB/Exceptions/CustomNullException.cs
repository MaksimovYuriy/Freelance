namespace FreelanceDB.Exceptions
{
    public class CustomNullException : Exception
    {
        public CustomNullException() { }

        public CustomNullException(string message) : base(message) { }
    }
}
