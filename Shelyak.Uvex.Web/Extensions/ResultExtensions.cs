﻿using Ardalis.Result;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.Web.Extensions;

public static class ResultExtensions
{
    public static bool IsSuccessAndHasValue<T>(this Result<AlpacaResponse<T>> result) => result is { IsSuccess: true, Value.Value: not null };
}