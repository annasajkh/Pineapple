using SkiaSharp;

namespace Pineapple.Abstract;

public abstract class Scene
{
    public abstract void Load();
    public abstract void Update(float delta);
    public abstract void Draw(SKCanvas canvas, SKPaint paint);
    public abstract void Unload();

    public void UnloadInternal()
    {
        Unload();
        Console.WriteLine($"Scene: {GetType().Name} is unloaded");
    }
}