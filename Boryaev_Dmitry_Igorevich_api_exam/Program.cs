using Boryaev_Dmitry_Igorevich_api_exam;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

List<Status> statuses = new() {new("Создана"),new("Отменена"), new("Выполнена") };
List<BTask> tasks = new() {new("Приготовить еду","Лазанья", statuses[0]), new("Пропылесосить", "Всю комнату", statuses[1]), new("Вынести мусор", "Срочно", statuses[2]) };

app.MapGet("/tasks", () => tasks);
app.MapPost("/tasks/{title}/{description}/{status}", (string title, string description, string status) => 
    {
        Status neededStatus = statuses.Where(a => a.Name == status).First();
        BTask task = new(title, description, neededStatus);
        tasks.Add(task);
    });

app.MapPut("/tasks/{id}", async (int id, HttpContext context) =>
{
    var bTask = await context.Request.ReadFromJsonAsync<BTask>();
    BTask neededTask = tasks.Where(a => id == a.Id).First();
    neededTask.Title = bTask.Title;
    neededTask.Description = bTask.Description;
    neededTask.Status = bTask.Status;
});

app.MapDelete("/tasks/{id}", async (int id) =>
{
    BTask neededTask = tasks.Where(a => id == a.Id).First();
    tasks.Remove(neededTask);
});

app.Run();
