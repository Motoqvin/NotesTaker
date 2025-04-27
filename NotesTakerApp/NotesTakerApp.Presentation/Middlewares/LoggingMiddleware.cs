namespace NotesTakerApp.Presentation.Middlewares;
public class LoggingMiddleware
{
    private readonly RequestDelegate next;

    public LoggingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        await this.next(context);
        
    }
}