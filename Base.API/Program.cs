
using Base.API.Extensions;
using Base.API.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.AddControllers();
builder.AddSwagger();
builder.AddCache();
builder.AddJwtAuth();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();