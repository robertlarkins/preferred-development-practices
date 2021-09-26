# Schema Docs from Code
When using SwashBuckle (NuGet package) to create the OpenAPI schema (also known as Swagger),
it is useful to automatically provide details into the schema from comments and rules provided in the code.

The schema docs can be added to from several sources, all of which are driven from the Controller Action XML docs, attributes and parameters.

## XML Comments
XML comments are the standard comments that appear above an action, class, or property. I _don't think_ method docs go into the schema docs.

### Action
The following XML tags on an Action occur in the schema docs.
- `<summary>Goes into the summary field for the http call</summary>` While tags such as `<para>` and `<code>` can be used inside this, their tags get displayed in the swagger UI
- `<remarks>The description field for the http call.</remarks>` There are further tags, such as `<para>` and `<code>`, that can be used inside this.
- `<param name="someParam">The parameter description</param>` If the parameter is using a primitive type, then this will become the parameter description. If the parameter is a class this description will not go into the swagger docs, but is still useful for a developer reading the code.
- `<response code="someHttpCode">The response title</response>` The description for the response with the given http code

> Note:
> Both `<c>` and `<code>` indicate code block, though `<c>` is for single line text, while `<code>` can be used for multiple lines.
> However, the `<list>` tag does not appear to work as desired in the Swagger UI.

The following tags don't seem to provide anything to the docs
- `<returns></returns>`
- `<value></value>`
- `<exception cref="">`

### Classes
Often the parameters into an action are a class or nested class that contains multiple properties.
ASP.Net will map the passed in parameter values into class properties, for both `[FromRoute]` and `[FromBody]`.

The following XML tags applied to the class occur in the schema docs:
- `<summary>` This goes into the object's description that contains the params

The following tags don't seem to provide anything to the docs
- `<remarks>`
- `<example>`

### Property
The specific properties inside these parameter classes can have their own docs. These properties are the parameters visible to the client of this API.
- `<summary>` This goes into the parameter's description, as well as the description in the parameter's schema field
- `<example>` This can be used to provide an example of what the parameter value can be.  
  Examples only seem to appear in the schema for properties that have a primitive type. A property using a reference (list or custom class) or value (struct) type don't appear to work.  
  At present, the open and close tags need to be on the same line (`<example>1234</example>`), if they are spread across multiple lines the example does not appear to be put into the docs.

> Note:
> These tags seem to cause duplications in the schema for the parameter. Once at the parameter level and then at the schema level (under parameter).
> It is unknown at present why this is happening, whether it is by design or a bug in the library that maps the comments to the schema docs.

The following tags don't seem to provide anything to the docs
- `<remarks>`
- `<value>`

## Asp.net data annotations
There are a number of built in ASP.Net data validations that can be applied to a property, either in the Action signature or in the class.
As this document is more about using Fluent Validations in the place of data annotations, they won't be described in detail.
However, there is at least one (and possibly other) data annotation attributes that provide docs not available any other way.
This is
- `[DefaultValue(theValue)]` which goes into the default field in the parameters, in particular under the schema field (not sure of the schema field purpose though).

## Fluent Validations
As a replacement for ASP.net data annotations, the Fluent Validations NuGet package can be used. It allows rules to be provided that validate the input from the client.
These rules can also be used to provide additional information about the parameters in the schema docs, such as whether a parameter is required (`NotNull` or `NotEmpty`),
or if it has to be within a certain bound (such as for an int).

To achieve this mapping of fluent validation rules to the OpenAPI schema requires the
[MicroElements.Swashbuckle.FluentValidations](https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation) NuGet package.
This GitHub page has details for how to use the package and what rules Fluent Validations rules are presented in the docs.

For the validator rules to be mapped to the schema requires the validator to target the parameter class.
If it does not target the parameter class then the mapping cannot find the rules. Infact if there is no other mechanism for running the validator,
then these parameter will not be validated. If the parameter is defined in the Action signature using a primative type, then it too will not be validated.
This also applies if the parameters are mapped to a class inside the action and this class has a validator, as this validator is not automatically run (unless you are using another mechanism to run the validator). There is no way for the system to know that this internal class and validator should be mapped to the schema.

Therefore, if there are rules to be mapped to the schema, they need to be in a validator that is applied to a class that is a parameter in the Action signature.

## Example
The following is an example of how these pieces can be used:

```C#
[Route("calendar/{calendarId}")]
public CalendarController
{
    /// <summary>
    /// Update a calendar date.
    /// </summary>
    /// <remarks>
    /// Some details about how to about the Update Calendar endpoint.
    /// <para><code>
    /// Some code example.
    /// </code></para>
    /// </remarks>
    /// <param name="routeParams">The parameters that have come from the route.</param>
    /// <param name="commandValues">The values used to update the caledar.</param>
    /// <response code="500">Well damn, something went wrong.</response>
    /// <response code="200">Hooray, you did it!</response>
    /// <returns>The updated values.</returns>
    [HttpPost("date/{year}/{month}/{day}")]
    [ProducesResponseType(typeof(UpdateCalendarResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateList(
        [FromRoute] UpdateCalendarCommand.RouteParamsDto routeParams,
        [FromBody] UpdateCalendarCommand.Values commandValues)
    {
        var command = new CreateListCommand
        {
            RouteParams = routeParams,
            CreateValues = commandValues
        };

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}

public class UpdateCalendarCommand : IRequest<UpdateCalendarResponse>
{
    public RouteParamsDto RouteParams { get; set; }

    public Values CreateValues { get; set; }

    /// <summary>
    /// Route Param Desc.
    /// </summary>
    public class RouteParamsDto
    {
        /// <summary>
        /// The identifier of the organisation this list is being created for.
        /// <code>Some i++ code</code>
        /// <para>A paragraph again!</para>
        /// </summary>
        /// <example>f30aa67b-0618-44a3-b331-a64138fcee85</example>
        public Guid CalendarId { get; set; }

        /// <summary>
        /// The year.
        /// </summary>
        /// <example>1927</example>
        [DefaultValue(1999)]
        public int Year { get; set; }

        /// <summary>
        /// The month.
        /// </summary>
        /// <example>12</example>>
        public int Month { get; set; }

        public int Day { get; set; }
    }

    public class Values
    {
        /// <summary>
        /// The date name, which should be meaningful.
        /// </summary>
        /// <example>christmas</example>
        public string DateName { get; set; }

        public List<Event> Events { get; set; }

        public class Event
        {
            /// <summary>
            /// Gets or sets the reference, which is a provided identifier for the event.
            /// </summary>
            /// <example>My reference</example>
            public string Reference { get; set; }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <example>my example</example>
            public string SomeOtherValue { get; set; }
        }
    }
}

/// <summary>
/// Response description.
/// </summary>
public class UpdateCalendarResponse
{
    /// <summary>
    /// The identifier!
    /// </summary>
    /// <example>an example for me</example>
    public Guid Id { get; set; }

    public string ObjectUrl { get; set; }
}

public class UpdateCalendarCommandValuesValidator : AbstractValidator<UpdateCalendarCommand.Values>
{
    public UpdateCalendarCommandValuesValidator()
    {
        RuleFor(p => p.DateName)
            .NotEmpty()
            .WithMessage(c => $"{nameof(c.DateName)} is required.");

        RuleForEach(p => p.Events).ChildRules(
            event =>
            {
                event.RuleFor(r => r.Reference).NotEmpty();
                event.RuleFor(r => r.SomeOtherValue).NotNull();
            }).WithMessage("Some sort of event error.");
    }
}

public class UpdateCalendarCommandRouteParamsValidator : AbstractValidator<UpdateCalendarCommand.RouteParamsDto>
{
    public UpdateCalendarCommandRouteParamsValidator()
    {
        RuleFor(p => p.CalendarId)
            .NotNull()
            .WithMessage(c => $"{nameof(c.CalendarId)} is required.");

        RuleFor(p => p.Day)
            .GreaterThanOrEqualTo(1).LessThanOrEqualTo(31)
            .WithMessage(c => $"{nameof(c.Day)} is not a recognised day.");

        RuleFor(p => p.Month)
            .NotNull()
            .GreaterThanOrEqualTo(1).LessThanOrEqualTo(12)
            .WithMessage(c => $"{nameof(c.Month)} is not a recognised month.");
    }
}
```
