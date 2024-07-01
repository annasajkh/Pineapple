using Microsoft.Extensions.Logging;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using Pineapple.Bindings.SDL;
using SixLabors.ImageSharp.PixelFormats;
using SkiaSharp;
using static Pineapple.Core.ApplicationInternal;
using Image = SixLabors.ImageSharp.Image;
using OpenTKImage = OpenTK.Windowing.Common.Input.Image;

namespace Pineapple.Core;

public static class Application
{
    public static event Action? Load;
    public static event UpdateEvent? Update;
    public static event DrawEvent? Draw;
    public static event Action? Unload;

    /// <summary>
    ///  Gets or sets a double representing the update frequency, in hertz
    /// </summary>
    /// <remarks>
    /// A value of 0.0 indicates that UpdateFrame events are generated at the maximum
    /// possible frequency (i.e. only limited by the hardware's capabilities)
    /// 
    /// Values lower than 1.0Hz are clamped to 0.0. Values higher than 500.0Hz are clamped to 500.0Hz
    /// </remarks>
    public static double UpdateFrequency
    {
        get
        {
            return ApplicationInternal.UpdateFrequency;
        }

        set
        {
            ApplicationInternal.UpdateFrequency = value;
        }
    }

    /// <summary>
    /// Processes pending window events and waits timeout seconds for events
    /// </summary>
    /// <param name="timeout">The timeout in seconds</param>
    /// <returns>This function will always return true</returns>
    public static bool ProcessEvents(double timeout)
    {
        return ApplicationInternal.ProcessEvents(timeout);
    }

    /// <summary>
    /// Resets the time since the last update. This function is useful when implementing updates on resize using windows
    /// </summary>
    public static void ResetTimeSinceLastUpdate()
    {
        ApplicationInternal.ResetTimeSinceLastUpdate();
    }

    /// <summary>
    /// The current time since the last update. This function is useful when implementing updates on resize using windows
    /// </summary>
    /// <remarks>
    /// Don't use this in OpenTK.Windowing.Desktop.GameWindow.OnUpdateFrame(OpenTK.Windowing.Common.FrameEventArgs)
    /// or OpenTK.Windowing.Desktop.GameWindow.OnRenderFrame(OpenTK.Windowing.Common.FrameEventArgs),
    /// instead use OpenTK.Windowing.Common.FrameEventArgs.Time
    /// </remarks>
    /// <returns>The time since the last update</returns>
    public static double TimeSinceLastUpdate()
    {
        return ApplicationInternal.TimeSinceLastUpdate();
    }

    /// <summary>
    /// transform Vector2i from screen to window coordinates
    /// </summary>
    public static Vector2i ScreenToWindow(Vector2i value)
    {
        return ApplicationInternal.PointToClient(value);
    }

    /// <summary>
    /// transform Vector2i from window to screen coordinates
    /// </summary>
    public static Vector2i WindowToScreen(Vector2i value)
    {
        return ApplicationInternal.PointToScreen(value);
    }

    /// <summary>
    /// Makes the GraphicsContext current on the calling thread
    /// </summary>
    public static void MakeCurrent()
    {
        ApplicationInternal.MakeCurrent();
    }

    /// <summary>
    /// Updates the input state in preparation for a call to GLFW.PollEvents
    /// or GLFW.WaitEvents. Do not call this
    /// function if you are calling ProcessEvents() or if you are running the window
    /// using OpenTK.Windowing.Desktop.GameWindow.Run.
    /// </summary>
    public static void NewInputFrame()
    {
        ApplicationInternal.NewInputFrame();
    }

    /// <summary>
    /// Gets a value representing the current graphics API profile
    /// </summary>
    public static ContextProfile Profile
    {
        get
        {
            return ApplicationInternal.Profile;
        }
    }

    /// <summary>
    /// Enables or disables raw mouse input. Raw mouse input is only enabled when OpenTK.Windowing.Desktop.NativeWindow.CursorState=OpenTK.Windowing.Common.CursorState.Grabbed.
    /// Check OpenTK.Windowing.Desktop.NativeWindow.SupportsRawMouseInput before settings this
    /// </summary>
    public static bool RawMouseInput
    {
        get
        {
            return ApplicationInternal.RawMouseInput;
        }

        set
        {
            ApplicationInternal.RawMouseInput = value;
        }
    }

    /// <summary>
    /// Whether or not RawMouseInput is supported
    /// </summary>
    public static bool SupportsRawMouseInput
    {
        get
        {
            return ApplicationInternal.SupportsRawMouseInput;
        }
    }

    /// <summary>
    /// Gets a value representing the current graphics profile flags
    /// </summary>
    public static ContextFlags Flags
    {
        get
        {
            return ApplicationInternal.Flags;
        }
    }

    /// <summary>
    /// The expected scheduler period in milliseconds. Used to provide accurate sleep timings
    /// </summary>
    public static int ExpectedSchedulerPeriod
    {
        get
        {
            return ApplicationInternal.ExpectedSchedulerPeriod;
        }

        set
        {
            ApplicationInternal.ExpectedSchedulerPeriod = value;
        }
    }

    /// <summary>
    /// Gets or sets the clipboard string
    /// </summary>
    public static string ClipboardString
    {
        get
        {
            return ApplicationInternal.ClipboardString;
        }

        set
        {
            ApplicationInternal.ClipboardString = value;
        }
    }

    /// <summary>
    /// Gets a double representing the time spent in the UpdateFrame function, in seconds
    /// </summary>
    public static double UpdateTime
    {
        get
        {
            return ApplicationInternal.UpdateTime;
        }

        set
        {
            ApplicationInternal.UpdateTime = value;
        }
    }

    public static ApplicationInternal ApplicationInternal { get; private set; }

    public static void Run(string title, Vector2i size, Vector2i? windowPosition = null, WindowState windowState = WindowState.Normal, WindowBorder windowBorder = WindowBorder.Fixed, bool transparent = false, VSyncMode vsync = VSyncMode.On, Vector2i? minimumSize = null, Vector2i? maximumSize = null)
    {
        ApplicationInternal = new ApplicationInternal(title, size, windowPosition, windowState, windowBorder, transparent, vsync, minimumSize, maximumSize);

        ApplicationInternal.Load += Load;
        ApplicationInternal.Update += Update;
        ApplicationInternal.Draw += Draw;
        ApplicationInternal.Unload += Unload;

        ApplicationInternal.Run();
    }

    public static void Run(NativeWindowSettings nativeWindowSettings)
    {
        ApplicationInternal = new ApplicationInternal(nativeWindowSettings);

        ApplicationInternal.Load += Load;
        ApplicationInternal.Update += Update;
        ApplicationInternal.Draw += Draw;
        ApplicationInternal.Unload += Unload;

        ApplicationInternal.Run();
    }
}

public class ApplicationInternal : GameWindow
{
    /// <summary>
    /// Gets a double representing the time spent in the UpdateFrame function, in seconds
    /// </summary>
    public double UpdateTime
    {
        get
        {
            return UpdateTime;
        }

        set
        {
            UpdateTime = value;
        }
    }

    public delegate void DrawEvent(SKCanvas canvas, SKPaint paint);
    public delegate void UpdateEvent(float delta);

    public event Action? Load;
    public event UpdateEvent? Update;
    public event DrawEvent? Draw;
    public event Action? Unload;

    private static ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    public ILogger<ApplicationInternal> Logger { get; }

    GRGlInterface grgInterface;
    GRContext grContext;
    SKSurface surface;
    GRBackendRenderTarget renderTarget;
    SKCanvas canvas;
    SKPaint paint;

    public ApplicationInternal(string title, Vector2i size, Vector2i? windowPosition = null, WindowState windowState = WindowState.Normal, WindowBorder windowBorder = WindowBorder.Fixed, bool transparent = false, VSyncMode vsync = VSyncMode.On, Vector2i? minimumSize = null, Vector2i? maximumSize = null) : this(new NativeWindowSettings
    {
        Title = title,
        Flags = ContextFlags.ForwardCompatible | ContextFlags.Debug,
        Profile = ContextProfile.Core,
        StartFocused = false,
        StartVisible = false,
        WindowBorder = windowBorder,
        ClientSize = size,
        Vsync = vsync,
        TransparentFramebuffer = transparent,
        WindowState = windowState,
        MinimumClientSize = minimumSize,
        MaximumClientSize = maximumSize
    })
    {
        Logger = loggerFactory.CreateLogger<ApplicationInternal>();
        string iconPath = Path.Combine("Assets", "Icons", "icon.png");

        if (File.Exists(iconPath))
        {
            var icon = Image.Load<Rgba32>(iconPath);
            var iconData = new byte[icon.Width * icon.Height * 32];

            icon.CopyPixelDataTo(iconData);

            Icon = new WindowIcon(new OpenTKImage(icon.Width, icon.Height, iconData));

            icon.Dispose();
        }
        else
        {
            Logger.LogWarning("Icon file doesn't exist in Assets/Icons/icon.png fallback using the default icon");
        }

        if (windowPosition is Vector2i position)
        {
            Location = position;
        }
        else
        {
            CenterWindow();
        }

        IsVisible = true;
        Focus();
    }

    public ApplicationInternal(NativeWindowSettings nativeWindowSettings) : base(new GameWindowSettings(), nativeWindowSettings)
    {
        grgInterface = GRGlInterface.Create();
        grContext = GRContext.CreateGl(grgInterface);
        renderTarget = new GRBackendRenderTarget(ClientSize.X, ClientSize.Y, 0, 8, new GRGlFramebufferInfo(0, (uint)SizedInternalFormat.Rgba8));
        surface = SKSurface.Create(grContext, renderTarget, GRSurfaceOrigin.BottomLeft, SKColorType.Rgba8888);

        canvas = surface.Canvas;

        paint = new SKPaint
        {
            Color = SKColors.White,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextAlign = SKTextAlign.Center,
            TextSize = 32
        };

        if (SDL.SDL_Init(SDL.SDL_INIT_AUDIO) < 0)
        {
            throw new Exception("Cannot initialize SDL Audio");
        }
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        Load?.Invoke();
    }

    protected override void OnResize(ResizeEventArgs resizeEventArgs)
    {
        GL.Viewport(0, 0, resizeEventArgs.Size.X, resizeEventArgs.Size.Y);

        renderTarget.Dispose();
        surface.Dispose();
        canvas.Dispose();

        renderTarget = new GRBackendRenderTarget(resizeEventArgs.Size.X, resizeEventArgs.Size.Y, 0, 8, new GRGlFramebufferInfo(0, (uint)SizedInternalFormat.Rgba8));
        surface = SKSurface.Create(grContext, renderTarget, GRSurfaceOrigin.BottomLeft, SKColorType.Rgba8888);
        canvas = surface.Canvas;

        base.OnResize(resizeEventArgs);
    }

    protected override void OnUpdateFrame(FrameEventArgs frameEventArgs)
    {
        base.OnUpdateFrame(frameEventArgs);

        Update?.Invoke((float)frameEventArgs.Time);
    }

    protected override void OnRenderFrame(FrameEventArgs frameEventArgs)
    {
        base.OnRenderFrame(frameEventArgs);

        Draw?.Invoke(canvas, paint);

        canvas.Flush();
        SwapBuffers();
    }

    protected override void OnUnload()
    {
        paint.Dispose();

        surface.Dispose();
        renderTarget.Dispose();
        grContext.Dispose();
        grgInterface.Dispose();

        base.OnUnload();

        Unload?.Invoke();

        SDL.SDL_Quit();
    }
}
