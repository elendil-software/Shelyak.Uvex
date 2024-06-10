using Ardalis.Result;
using BlazorBootstrap;

namespace Shelyak.Uvex.Web.Components.Shared.Toasts;

public static class ToastServiceExtensions
{
    public static void DisplayToast<T>(this ToastService toastService, Result<T> result)
    {
        if(result.IsSuccess)
        {
            toastService.DisplaySuccessToast(result.SuccessMessage);
        }
        else
        {
            toastService.DisplayErrorsToast(result);
        }
    }
    
    public static void DisplaySuccessToast(this ToastService toastService, string message)
    {
        toastService.Notify(new ToastMessage(ToastType.Success, message));
    }
    
    public static void DisplayErrorToast(this ToastService toastService, string message)
    {
        toastService.Notify(new ToastMessage(ToastType.Danger, message));
    }

    public static void DisplayErrorsToast<T>(this ToastService toastService, Result<T> result)
    {
        toastService.DisplayErrorToast(string.Join(", ", result.Errors.ToList()));
    }
}