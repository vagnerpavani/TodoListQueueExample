

using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/mail", (MailBodyDto dto) =>
{
    Console.WriteLine("Request POST /mail received");
    bool error = false;
    string mailId = DateTime.Now.ToString("yyyyMMddHHmmssffff");
    //tempo de resposta demorado
    var rnd = new Random();
    int waitTime = rnd.Next(3, 10) * 1000;
    Thread.Sleep(waitTime);

    //chance de sucesso ou falha.
    int successChance = rnd.Next(1, 10);
    if (successChance < 5) error = true;

    if (error) {
        Console.WriteLine("Falha no envio de email. Ops!");
        return Results.Problem($"Failed atempt to send email => send: {dto.FromEmail} to: {dto.TargetEmail} mailId: {mailId}");
    }

    //Se não falhar, salva arquivo txt do email.
    Console.WriteLine($"Email to:{dto.TargetEmail} \n Email body: {dto.Content}");
    var docPath = Environment.CurrentDirectory;
    File.WriteAllText(Path.Combine(docPath, $"Mails/mail{mailId}.txt"), $"From: {dto.FromEmail} \nTo: {dto.TargetEmail} \n\n{dto.Content}");

    return Results.Ok(mailId);
})
.WithName("mail")
.WithOpenApi();

app.Run();


