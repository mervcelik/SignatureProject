using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Security.Extensions;
using Core.CrossCuttingConcerns.Dtos;
namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IMediator Mediator =>
        _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");

    private IMediator? _mediator;

    [NonAction]
    protected string getIpAddress()
    {
        string ipAddress = Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"].ToString()
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
                ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");
        return ipAddress;
    }
    [NonAction]
    protected int getUserIdFromRequest() //todo authentication behavior?
    {
        var userId = Convert.ToInt32(HttpContext.User.GetUserId()!);
        
         return userId;
    }

    [NonAction]
    public IActionResult CreateActionResult<T>(ResponseDto<T> response)
    {
        if (response.StatusCode == 204)
            return new ObjectResult(null)
            {
                StatusCode = response.StatusCode
            };

        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };


    }
}
