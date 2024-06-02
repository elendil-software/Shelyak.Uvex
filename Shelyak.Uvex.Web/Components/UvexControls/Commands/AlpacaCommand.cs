using Ardalis.Result;
using FastEndpoints;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.Web.Components.UvexControls.Commands;

public record AlpacaCommand<T>(string Route) : ICommand<Result<AlpacaResponse<T>>>;
public record AlpacaPutCommand<T>(T Value, string Route) : AlpacaCommand<T>(Route);
public record AlpacaPutWithoutValueCommand<T>(string Route) : AlpacaCommand<T>(Route);
public record AlpacaGetCommand<T>(string Route) : AlpacaCommand<T>(Route);



