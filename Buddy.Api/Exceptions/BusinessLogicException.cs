using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Buddy.Api.Exceptions
{
	public class BusinessLogicException : Exception
	{
		public BusinessLogicException()
		{
		}

		public BusinessLogicException(string errorCode, string message) : base(message)
		{
			ErrorCode = errorCode;
		}

		public BusinessLogicException(string errorCode, string message, Exception innerException) : base(message, innerException)
		{
			ErrorCode = errorCode;
		}

		protected BusinessLogicException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public string ErrorCode { get; private set; }
	}

}
