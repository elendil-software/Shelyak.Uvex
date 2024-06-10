using Ardalis.Result;
using BlazorBootstrap;

namespace Shelyak.Uvex.Web.Components.Shared.Toasts;

public static class ToastServiceExtensions
{
    public static void DisplayToast<T>(this ToastService toastService, Result<T> result)
    {
        if(result.IsSuccess)
        {
            toastService.Notify(new ToastMessage(ToastType.Success, result.SuccessMessage));
        }
        else
        {
            toastService.Notify(new ToastMessage(ToastType.Danger, string.Join(", ", result.Errors.ToList())));
        }
    }
    
    public static void DisplaySuccessToast(this ToastService toastService, string message)
    {
        toastService.Notify(new ToastMessage(ToastType.Success, message));
    }
    
    public static void DisplayToastIfNotSuccess<T>(this ToastService toastService, Result<T> result)
    {
        toastService.DisplayErrorsToast(result);
    }

    public static void DisplayErrorsToast<T>(this ToastService toastService, Result<T> result)
    {
        toastService.Notify(new ToastMessage(ToastType.Danger, string.Join(", ", result.Errors.ToList())));
    }
}