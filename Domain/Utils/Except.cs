using System.Net;

namespace WeCare.Domain.Utils;

public class Except : Exception
{
    public int StatusCode { get; }
    public Except(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
    public static Except NotFound(string message) => new Except(message, (int)HttpStatusCode.NotFound);
    public static Except BadRequest(string message) => new Except(message, (int)HttpStatusCode.BadRequest);
    public static Except Forbidden(string message) => new Except(message, (int)HttpStatusCode.Forbidden);
    public static Except Unauthorized(string message) => new Except(message, (int)HttpStatusCode.Unauthorized);
}
