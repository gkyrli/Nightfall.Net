using System;
using System.Runtime.Serialization;

namespace Nightfall.Net.Exceptions
{
    public class BaseNightfallException : Exception
    {
        public BaseNightfallException(ApiErrorResponse apiErrorResponse)
        {
            ApiErrorResponse = apiErrorResponse;
        }

        public ApiErrorResponse ApiErrorResponse { get; private set; }
    }

    public class NightfallIncorrectFileState409 : BaseNightfallException
    {
        public NightfallIncorrectFileState409(ApiErrorResponse apiErrorResponse) : base(apiErrorResponse)
        {
        }
    }

    public class NightfallInvalidFileId404 : BaseNightfallException
    {
        public NightfallInvalidFileId404(ApiErrorResponse apiErrorResponse) : base(apiErrorResponse)
        {
        }
    }

    public class NightfallInternalServer500 : BaseNightfallException
    {
        public NightfallInternalServer500(ApiErrorResponse apiErrorResponse) : base(apiErrorResponse)
        {
        }
    }

    public class NightfallRateLimit429 : BaseNightfallException
    {
        public NightfallRateLimit429(ApiErrorResponse apiErrorResponse) : base(apiErrorResponse)
        {
        }
    }

    public class NightfallUnprocessableRequestPayload422 : BaseNightfallException
    {
        public NightfallUnprocessableRequestPayload422(ApiErrorResponse apiErrorResponse) : base(apiErrorResponse)
        {
        }
    }

    public class NightfallAuthenticationFailure401 : BaseNightfallException
    {
        public NightfallAuthenticationFailure401(ApiErrorResponse apiErrorResponse) : base(apiErrorResponse)
        {
        }
    }

    public class NightfallInvalidRequest400 : BaseNightfallException
    {
        public NightfallInvalidRequest400(ApiErrorResponse apiErrorResponse) : base(apiErrorResponse)
        {
        }
    }

    public class NightfallUnknownExceptionResponse : BaseNightfallException
    {
        public NightfallUnknownExceptionResponse(ApiErrorResponse apiErrorResponse) : base(apiErrorResponse)
        {
        }
    }
}