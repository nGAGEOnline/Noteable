using Blazored.Toast;
using Blazored.Toast.Configuration;
using Blazored.Toast.Services;
using Noteable.Components;

namespace Noteable.Models;

public enum ToastType
{
	Info,
	Success,
	Warning,
	Error
}

public class Toast
{
	private IToastService ToastService { get; }
	private string Id { get; set; }
	private string Header { get; set; }
	private string Body { get; set; }
	private ToastLevel Type { get; set; }
	private string Class { get; set; }
	private string Icon { get; set; }
	private string IconColor { get; set; }
	private bool ShowProgressBar { get; set; } = true;
	private int Timeout { get; set; } = 3;

	public Toast(IToastService toastService) => ToastService = toastService;

	public void Show(string header, string body, ToastLevel type, int timeout = 10, bool showProgressBar = true, ToastPosition toastPosition = ToastPosition.BottomRight)
	{
		Id = Guid.NewGuid().ToString();
		Header = header;
		Body = body;
		Type = type;
		Timeout = timeout;
		ShowProgressBar = showProgressBar;
		Icon = type switch
		{
			ToastLevel.Info => "bi bi-info-circle-fill",
			ToastLevel.Success => "bi bi-hand-thumbs-up-fill",
			ToastLevel.Warning => "bi bi-exclamation-triangle-fill",
			ToastLevel.Error => "bi bi-x-octagon-fill",
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
		Class = type switch
		{
			ToastLevel.Info => "toast-info",
			ToastLevel.Success => "toast-success",
			ToastLevel.Warning => "toast-warning",
			ToastLevel.Error => "toast-error",
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
		IconColor = type switch
		{
			ToastLevel.Info => "text-info",
			ToastLevel.Success => "text-success",
			ToastLevel.Warning => "text-warning",
			ToastLevel.Error => "text-danger",
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};

		var parameters = new ToastParameters();
		parameters.Add(nameof(ToastComponent.Header), Header);
		parameters.Add(nameof(ToastComponent.Body), Body);
		parameters.Add(nameof(ToastComponent.Icon), Icon);
		parameters.Add(nameof(ToastComponent.IconColor), IconColor);
		parameters.Add(nameof(ToastComponent.Class), Class);
		ToastService.ShowToast<ToastComponent>(parameters, settings =>
		{
			settings.Position = toastPosition;
			settings.Icon = Icon;
			settings.ShowCloseButton = true;
			settings.Timeout = Timeout;
			settings.ShowProgressBar = ShowProgressBar;
			settings.PauseProgressOnHover = true;
		});
	}
}