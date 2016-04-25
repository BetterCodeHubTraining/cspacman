using System;

namespace eu.sig.cspacman
{
    [Serializable]
    public class PacmanConfigurationException : SystemException
    {

        public PacmanConfigurationException(string message) : base(message)
        {
        }

        public PacmanConfigurationException(Exception cause) : base("(No message)", cause)
        {
        }

        public PacmanConfigurationException(string message, Exception cause) : base(message, cause)
        {
        }
    }

}
