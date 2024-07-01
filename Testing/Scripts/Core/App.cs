﻿using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Pineapple.Audio;
using Pineapple.Core;
using SkiaSharp;
using Window = Pineapple.Core.Window;

namespace Testing.Scripts.Core;

public class App
{
    float time;
    SKImage catImage;
    MusicPlayer musicPlayer;
    Music music;

    public App()
    {
        Application.Load += Load;
        Application.Update += Update;
        Application.Draw += Draw;
        Application.Unload += Unload;
    }

    void Load()
    {
        catImage = SKImage.FromEncodedData(Path.Combine("Assets", "Sprites", "this fucking cat.png"));
        musicPlayer = new MusicPlayer();
        music = Music.Load(Path.Combine("Assets", "Musics", "Different Heaven & Sian Area - Feel Like Horrible [NCS Release].ogg"), MusicType.Ogg);
        
        musicPlayer.Volume = 50;
        musicPlayer.SetSource(music);
        musicPlayer.Play();
    }

    void Update(float delta)
    {
        if (Input.IsKeyPressed(Keys.P))
        {
            musicPlayer.Paused = !musicPlayer.Paused;

            if (musicPlayer.Paused)
            {
                Console.WriteLine("Music is paused");
            }
            else
            {
                Console.WriteLine("Music is unpaused");
            }
        }

        time += delta;
    }

    void Draw(SKCanvas canvas, SKPaint paint)
    {
        canvas.Clear(SKColors.CornflowerBlue);

        canvas.DrawImage(catImage, new SKPoint(100, 100), paint);

        paint.Color = SKColors.Red;

        canvas.Save();

        canvas.Translate(Window.Size.X / 2, Window.Size.Y / 2);
        canvas.RotateRadians(time * 2);

        canvas.DrawRect(-50, -50, 100, 100, paint);
        canvas.Restore();

        paint.Color = SKColors.White;

        canvas.DrawCircle(new SKPoint(Input.MousePosition.X, Input.MousePosition.Y), 50, paint);
    }

    void Unload()
    {
        catImage.Dispose();
        music.Dispose();
    }

    public void Run()
    {
        Application.Run(title: "Testing", size: new Vector2i(960, 540), windowBorder: WindowBorder.Resizable);
    }
}