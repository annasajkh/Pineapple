// Ignore Spelling: App

using OpenTK.Mathematics;
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
    SKImage? catImage;
    MusicPlayer? musicPlayer;
    Music? music;

    SoundEffect? soundEffect;
    SoundEffectPlayer soundEffectPlayer;

    Random random = new Random();

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
        music = Music.Load(Path.Combine("Assets", "Musics", "Different Heaven & Sian Area - Feel Like Horrible [NCS Release].ogg"), AudioType.Ogg);
        music.Volume = 50;

        musicPlayer.SetSource(music);
        musicPlayer.Play();

        soundEffectPlayer = new SoundEffectPlayer(); 
        soundEffect = SoundEffect.LoadWav(Path.Combine("Assets", "Sounds", "meow.wav"), volume: 128, loop: true);

        List<Task> playAudioTasks = new();

        for (int i = 0; i < 3; i++)
        {
            Task task = PlaySoundEffectAsync(soundEffectPlayer, soundEffect);

            playAudioTasks.Add(task);
        }
    }

    async Task PlaySoundEffectAsync(SoundEffectPlayer soundEffectPlayer, SoundEffect soundEffect)
    {
        await Task.Delay(random.Next() % 3000);

        soundEffectPlayer.SetSource(soundEffect);
        soundEffectPlayer.Play();

    }

    void Update(float delta)
    {
        if (Input.IsKeyPressed(Keys.P))
        {
            musicPlayer!.Paused = !musicPlayer.Paused;

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
        catImage!.Dispose();
        music!.Dispose();
    }

    public void Run()
    {
        Application.Run(title: "Testing", size: new Vector2i(960, 540), windowBorder: WindowBorder.Resizable);
    }
}