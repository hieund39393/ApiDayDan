using System;
using System.Globalization;

namespace EVN.Core.Exceptions
{
    public class EvnException : Exception
    {
        public const string ErrorCode = "error_code";
        public EvnException()
        {

        }
        public EvnException(string message) : base(message)
        {
        }

        public EvnException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture,
            message, args))
        {
        }

        public EvnException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public EvnException(string message, int code) : base(message)
        {
            Data.Add(ErrorCode, code);
        }
    }
}
