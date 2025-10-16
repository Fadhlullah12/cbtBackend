using cbtbackend.Repositories.Interface;
using cbtBackend.Context;
using cbtBackend.Repositories.Implementations;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Services.Implementations;
using cbtBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(opt =>
   opt.UseMySQL(builder.Configuration.GetConnectionString("ConnectionString")!));

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder => builder.WithOrigins("https://fadhlullah12.github.io")
                              .AllowAnyHeader()
                              .AllowAnyMethod());
    });
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
//Add Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ISubAdminRepository, SubAdminRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
builder.Services.AddScoped<IStudentExamRepository, StudentExamRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
//Add Services
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubAdminService, SubAdminService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<HomePageService>();
builder.Services.AddScoped<IGetCurrentUser, GetCurrentUser>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
app.UseRouting();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
    app.UseEndpoints(endpoints =>
    {
        _ = endpoints.MapControllers();
    });

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
