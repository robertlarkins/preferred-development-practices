# ASP.Net Controller Examples

This provides examples for how Actions in ASP.Net Controllers can be setup for use with Mediatr.

https://ardalis.com/moving-from-controllers-and-actions-to-endpoints-with-mediatr/


## Base Controller
To make the Mediatr property available to any controller, we can have them derive from a base controller class:
```C#
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BreedSocieties.BreedIT.Api.Controllers
{
    /// <summary>
    /// BaseController used for mediatr.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator? mediator;

        /// <summary>
        /// Gets the Mediator object.
        /// </summary>
        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    }
}
```

## Thin Controllers

A controller can be as thin as a single line in which Mediatr is called with the request object and the response returned.

```C#
[Authorize]
[Route("api/students")]
[ApiController]
public class StudentController : BaseController
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetStudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetStudentResponse> GetById(int id)
    {
        return await Mediator.Send(new GetStudentQuery { Id = id });
    }
}
```

though for consistency across Actions this might be better written to return an `IActionResult`:

```C#
[Authorize]
[Route("api/students")]
[ApiController]
public class StudentController : BaseController
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetStudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetStudentQuery { Id = id };
        var response = await Mediator.Send(query);
    
        return Ok(response);
    }
}
```

## Controller with Validation
In some circumstances, such as when the `id` and command values are provided separately,
the command needs to be constructed in the Action and then validated (using something like FluentValidations):

```C#
[HttpPut("{id}")]
public async Task<IActionResult> UpdateStudent(
    int id,
    [FromBody] UpdateStudentCommand.Values commandValues)
{
    var command = new UpdateStudentCommand
    {
        Id = id,
        UpdateValues = commandValues
    };

    var validator = new UpdateStudentCommandValidator();
    var result = await validator.ValidateAsync(command);

    if (!result.IsValid)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return this.ValidationProblem();
    }

    var response = await Mediator.Send(command);

    return Ok(response);
}
```
This structure could be generalised into a Mediatr Pipeline.
 - https://codeopinion.com/why-use-mediatr-3-reasons-why-and-1-reason-not/
 - https://github.com/FluentValidation/FluentValidation/issues/866
 - https://www.fatalerrors.org/a/data-validation-of-fluentvalidation-based-on-net.html
