using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buddy.Api.Exceptions
{
	public static class ODataErrorExtensions
	{
		public const string DefaultODataErrorMessage = "A server error occurred.";

		public static ODataError CreateODataError(this SerializableError serializableError, bool isDevelopment)
		{
			var convertedError = SerializableErrorExtensions.CreateODataError(serializableError);
			var error = new ODataError();
			if (isDevelopment)
			{
				error = convertedError;
			}
			else
			{
				error.Message = DefaultODataErrorMessage;
				error.Details = new[] { new ODataErrorDetail { Message = convertedError.Message } };
			}

			return error;
		}

		public static ODataError CreateODataError(this Exception ex, bool isDevelopment)
		{
			var error = new ODataError();

			if (ex is BusinessLogicException)
			{
				error.ErrorCode = ((BusinessLogicException)ex).ErrorCode;
				error.Message = ex.Message;
			}
			else if (isDevelopment)
			{
				error.Message = ex.Message;
				error.InnerError = new ODataInnerError(ex);
			}
			else
			{
				error.Message = DefaultODataErrorMessage;
			}

			return error;
		}
	}

}
