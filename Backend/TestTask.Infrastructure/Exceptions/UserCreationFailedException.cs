namespace TestTask.Infrastructure.Exceptions
{
    public class UserCreationFailedException : Exception
    {
        public UserCreationFailedException()
            : base("User creation failed.") { }

        public UserCreationFailedException(string message)
            : base(message) { }
    }
}
