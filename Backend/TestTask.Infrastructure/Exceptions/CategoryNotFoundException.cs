namespace TestTask.Infrastructure.Exceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException()
            : base("The specified category was not found.") { }

        public CategoryNotFoundException(string message)
            : base(message) { }

        public CategoryNotFoundException(Guid categoryId)
            : base($"Category with ID '{categoryId}' was not found.") { }
    }
}
