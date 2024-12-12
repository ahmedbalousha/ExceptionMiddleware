namespace ExceptionMiddleware.API.Exceptions
{
    public class OperationLoginFailedException : Exception
    {
        public OperationLoginFailedException(string message) : base(message) { }
    }

    public class OperationFailedException : Exception
    {
        public OperationFailedException(string message) : base(message) { }
    }

    public class DuplicateEmailOrPhoneException : Exception
    {
        public DuplicateEmailOrPhoneException(string message) : base(message) { }
    }

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message) { }
    }

    public class InvalidDataException : Exception
    {
        public InvalidDataException(string message) : base(message) { }
    }
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message) { }
    }

}
