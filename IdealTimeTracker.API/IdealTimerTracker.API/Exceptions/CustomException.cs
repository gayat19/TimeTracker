namespace IdealTimeTracker.API.Exceptions
{
    public class CustomException
    {
    }
    public class ContextException : Exception {
        public ContextException() : base("Context is Empty")
        {

        }
        public ContextException(string message) : base(message)
        {

        } 
        
    }

    public class UserException : Exception
    {
        public UserException() : base("User exception")
        {

        }
        public UserException(string message) : base(message)
        {

        }

    }


    public class UserLogException : Exception
    {
        public UserLogException() : base("User log exception")
        {

        }
        public UserLogException(string message) : base(message)
        {

        }

    }

    public class UserActivityException : Exception
    {
        public UserActivityException() : base("User log exception")
        {

        }
        public UserActivityException(string message) : base(message)
        {

        }

    }
}



public class ApplicationConfigurationException : Exception
{
    public ApplicationConfigurationException() : base("Application Configuration Exception")
    {

    }
    public ApplicationConfigurationException(string message) : base(message)
    {

    }

}