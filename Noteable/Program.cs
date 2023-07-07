using Blazored.Modal;
using Blazored.Toast;
using Blazored.Toast.Services;
using Noteable.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<INoteRepository, InMemoryNoteRepository>();

builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredToast();
builder.Services.AddSingleton<IToastService, ToastService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
