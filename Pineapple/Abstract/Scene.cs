using Microsoft.Extensions.Logging;
using Pineapple.Core;
using SkiaSharp;

namespace Pineapple.Abstract;

public abstract class Scene
{
    public abstract void Load();
    public abstract void Update(float delta);
    public abstract void Draw(SKCanvas canvas);
    public abstract void Unload();

    public void LoadInternal()
    {
        Load();
        Application.Logger.LogInformation($"{GetType().Name} Scene is Loaded");
    }

    public void UnloadInternal()
    {
        Unload();
        Application.Logger.LogInformation($"{GetType().Name} Scene is Unloaded");
    }
}