namespace Base.API.ViewModels;

public record ExceptionViewModel
{
    public ExceptionViewModel(string title, string message, string stackTrace, string detail, int statusCode, DateTime createAt)
    {
        Title = title;
        Message = message;
        StackTrace = stackTrace;
        CreateAt = createAt;
        Detail = detail;
        StatusCode = statusCode;
    }

    public ExceptionViewModel()
    {
        
    }
    
    public string Title { get; set; }
    public string Message { get; set; }
    public string Detail { get; set; }
    public string StackTrace { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreateAt { get; set; }
}