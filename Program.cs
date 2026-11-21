using Engine;
using Game;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IEngine, RaylibEngine.RaylibEngine>();
services.AddSingleton<GameHost>();

using var provider = services.BuildServiceProvider();
var gameHost = provider.GetRequiredService<GameHost>();
gameHost.Run();
