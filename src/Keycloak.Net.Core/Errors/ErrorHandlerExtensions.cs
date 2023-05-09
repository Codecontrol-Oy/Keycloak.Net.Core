using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Content;
using Keycloak.Net.Core.Errors.ClientSideErrors;
using Keycloak.Net.Core.Models;

namespace Keycloak.Net.Core.Errors;

public static class ErrorHandlerExtensions
{
    public static async Task<HttpResponseMessage> HandleErrorsAsync(this Task<HttpResponseMessage> request)
    {
        try
        {
            return await request.ConfigureAwait(false);
        }
        catch (FlurlHttpException ex)
        {
            await HandleFlurlHttpException(ex).ConfigureAwait(false);
        }

        return default;
    }
    
    public static async Task<T> HandleErrorsAsync<T>(this Task<T> request)
    {
        try
        {
            return await request.ConfigureAwait(false);
        }
        catch (FlurlHttpException ex)
        {
            await HandleFlurlHttpException(ex).ConfigureAwait(false);
        }

        return default;
    }

    private static async Task HandleFlurlHttpException(FlurlHttpException ex)
    {
        var error = await ex.GetResponseJsonAsync<KeycloakError>();
            switch (ex.Call.HttpStatus)
            {
                case HttpStatusCode.Continue: // 100
                    break;
                case HttpStatusCode.SwitchingProtocols: // 101
                    break;
                case HttpStatusCode.Processing: // 102
                    break;
                case HttpStatusCode.EarlyHints: // 103
                    break;
                case HttpStatusCode.OK: // 200
                    break;
                case HttpStatusCode.Created: // 201
                    break;
                case HttpStatusCode.Accepted: // 202
                    break;
                case HttpStatusCode.NonAuthoritativeInformation: // 203
                    break;
                case HttpStatusCode.NoContent: // 204
                    break;
                case HttpStatusCode.ResetContent: // 205
                    break;
                case HttpStatusCode.PartialContent: // 206
                    break;
                case HttpStatusCode.MultiStatus: // 207
                    break;
                case HttpStatusCode.AlreadyReported: // 208
                    break;
                case HttpStatusCode.IMUsed: // 226
                    break;
                case HttpStatusCode.Ambiguous: // 300
                    break;
                case HttpStatusCode.Moved: // 301
                    break;
                case HttpStatusCode.Found: // 302
                    break;
                case HttpStatusCode.RedirectMethod: // 303
                    break;
                case HttpStatusCode.NotModified: // 304
                    break;
                case HttpStatusCode.UseProxy: // 305
                    break;
                case HttpStatusCode.Unused: // 306
                    break;
                case HttpStatusCode.RedirectKeepVerb: // 307
                    break;
                case HttpStatusCode.PermanentRedirect: // 308
                    break;
                case HttpStatusCode.BadRequest: // 400
                    throw new KeycloakBadRequest(error.Error, error.ErrorDescription);
                case HttpStatusCode.Unauthorized: // 401
                    // throw new KeycloakUnauthorized(error.Error, error.ErrorDescription);
                    break;
                case HttpStatusCode.PaymentRequired: // 402
                    break;
                case HttpStatusCode.Forbidden: // 403
                    // throw new KeycloakForbidden(error.Error, error.ErrorDescription);
                    break;
                case HttpStatusCode.NotFound: // 404
                    throw new KeycloakNotFound(error.Error, error.ErrorDescription);
                case HttpStatusCode.MethodNotAllowed: // 405
                    // throw new KeycloakMethodNotAllowed(error.Error, error.ErrorDescription);
                    break;
                case HttpStatusCode.NotAcceptable: // 406
                    break;
                case HttpStatusCode.ProxyAuthenticationRequired: // 407
                    break;
                case HttpStatusCode.RequestTimeout: // 408
                    break;
                case HttpStatusCode.Conflict: // 409
                    throw new KeycloakConflict(error.ErrorMessage, error.ErrorDescription);
                case HttpStatusCode.Gone: // 410
                    break;
                case HttpStatusCode.LengthRequired: // 411
                    break;
                case HttpStatusCode.PreconditionFailed: // 412
                    break;
                case HttpStatusCode.RequestEntityTooLarge: // 413
                    break;
                case HttpStatusCode.RequestUriTooLong: // 414
                    break;
                case HttpStatusCode.UnsupportedMediaType: // 415
                    // throw new KeycloakUnsupportedMediaType(error.Error, error.ErrorDescription);
                    break;
                case HttpStatusCode.RequestedRangeNotSatisfiable: // 416
                    break;
                case HttpStatusCode.ExpectationFailed: // 417
                    break;
                case HttpStatusCode.MisdirectedRequest: // 421
                    break;
                case HttpStatusCode.UnprocessableEntity: // 422
                    break;
                case HttpStatusCode.Locked: // 423
                    break;
                case HttpStatusCode.FailedDependency: // 424
                    break;
                case HttpStatusCode.UpgradeRequired: // 426
                    break;
                case HttpStatusCode.PreconditionRequired: // 428
                    break;
                case HttpStatusCode.TooManyRequests: // 429
                    break;
                case HttpStatusCode.RequestHeaderFieldsTooLarge: // 431
                    break;
                case HttpStatusCode.UnavailableForLegalReasons: // 451
                    break;
                case HttpStatusCode.InternalServerError: // 500
                    // throw new KeycloakInternalServerError(error.Error, error.ErrorDescription);
                    break;
                case HttpStatusCode.NotImplemented: // 501
                    break;
                case HttpStatusCode.BadGateway: // 502
                    break;
                case HttpStatusCode.ServiceUnavailable: // 503
                    break;
                case HttpStatusCode.GatewayTimeout: // 504
                    break;
                case HttpStatusCode.HttpVersionNotSupported: // 505
                    break;
                case HttpStatusCode.VariantAlsoNegotiates: // 506
                    break;
                case HttpStatusCode.InsufficientStorage: // 507
                    break;
                case HttpStatusCode.LoopDetected: // 508
                    break;
                case HttpStatusCode.NotExtended: // 510
                    break;
                case HttpStatusCode.NetworkAuthenticationRequired: // 511
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            throw new Exception(error.Error);
    }
}
