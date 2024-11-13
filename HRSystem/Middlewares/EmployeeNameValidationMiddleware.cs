namespace HRSystem.WebAPI.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="EmployeeNameValidationMiddleware" />
    /// </summary>
    public class EmployeeNameValidationMiddleware
    {
        /// <summary>
        /// Defines the _next
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Defines the _logger
        /// </summary>
        private readonly ILogger<EmployeeNameValidationMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeNameValidationMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next<see cref="RequestDelegate"/></param>
        /// <param name="logger">The logger<see cref="ILogger{EmployeeNameValidationMiddleware}"/></param>
        public EmployeeNameValidationMiddleware(RequestDelegate next, ILogger<EmployeeNameValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// The InvokeAsync
        /// </summary>
        /// <param name="context">The context<see cref="HttpContext"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            {
                if (context.Request.ContentType != null && context.Request.ContentType.Contains("application/json"))
                {
                    context.Request.EnableBuffering();

                    var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                    context.Request.Body.Position = 0;

                    var jsonBody = JObject.Parse(body);

                    if (jsonBody.ContainsKey("name") && ContainsNumber(jsonBody["name"].ToString()))
                    {
                        _logger.LogWarning("Name contains numeric characters, which is not allowed.");

                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("The 'name' field must not contain any numbers.");
                        return;
                    }
                }
            }
            //sucess
            await _next(context);
        }


        /// <summary>
        /// The ContainsNumber
        /// </summary>
        /// <param name="input">The input<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool ContainsNumber(string input)
        {
            return Regex.IsMatch(input, @"\d");
        }
    }
}
