using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using SixLabors.ImageSharp.PixelFormats;
using System.ComponentModel;
using Image = SixLabors.ImageSharp.Image;
using OpenTKImage = OpenTK.Windowing.Common.Input.Image;
using WindowState = OpenTK.Windowing.Common.WindowState;

namespace Pineapple.Core;

public static class Window
{
    /// <summary>
    /// Gets or sets the title of the window
    /// </summary>
    public static string Title
    {
        get
        {
            return Application.ApplicationInternal.Title;
        }

        set
        {
            Application.ApplicationInternal.Title = value;
        }
    }

    /// <summary>
    ///  Gets or sets the size of the window
    /// </summary>
    public static Vector2i Size
    {
        get
        {
            return Application.ApplicationInternal.ClientSize;
        }

        set
        {
            Application.ApplicationInternal.ClientSize = value;
        }
    }

    /// <summary>
    /// Set window position
    /// </summary>
    public static Vector2i Position
    {
        get
        {
            return Application.ApplicationInternal.ClientLocation;
        }

        set
        {
            Application.ApplicationInternal.ClientLocation = value;
        }
    }

    private static string? iconPath;

    /// <summary>
    ///  Gets or sets the icon path of the window
    /// </summary>
    public static string? IconPath
    {
        get
        {
            return iconPath;
        }

        set
        {
            iconPath = value;

            if (iconPath is not null)
            {
                if (!Path.Exists(iconPath))
                {
                    throw new Exception("File path doesn't exist");
                }

                var icon = Image.Load<Rgba32>(iconPath);
                var iconData = new byte[icon.Width * icon.Height * 32];

                icon.CopyPixelDataTo(iconData);

                Application.ApplicationInternal.Icon = new WindowIcon(new OpenTKImage(icon.Width, icon.Height, iconData));

                icon.Dispose();
            }
        }
    }

    /// <summary>
    /// Gets a value representing the current graphics API
    /// </summary>
    public static ContextAPI API
    {
        get
        {
            return Application.ApplicationInternal.API;
        }
    }

    /// <summary>
    /// Gets a value representing the current version of the graphics API
    /// </summary>
    public static Version APIVersion
    {
        get
        {
            return Application.ApplicationInternal.APIVersion;
        }
    }

    /// <summary>
    /// Occurs whenever the framebuffer is resized
    /// </summary>
    public static event Action<FramebufferResizeEventArgs> FramebufferResize
    {
        add
        {
            Application.ApplicationInternal.FramebufferResize += value;
        }

        remove
        {
            Application.ApplicationInternal.FramebufferResize -= value;
        }
    }

    /// <summary>
    /// Occurs whenever the window is moved
    /// </summary>
    public static event Action<WindowPositionEventArgs> Move
    {
        add
        {
            Application.ApplicationInternal.Move += value;
        }

        remove
        {
            Application.ApplicationInternal.Move -= value;
        }
    }

    /// <summary>
    /// Occurs whenever the window is resize
    /// </summary>
    public static event Action<ResizeEventArgs> Resize
    {
        add
        {
            Application.ApplicationInternal.Resize += value;
        }

        remove
        {
            Application.ApplicationInternal.Resize -= value;
        }
    }

    /// <summary>
    /// Occurs whenever the window is refreshed
    /// </summary>
    public static event Action Refresh
    {
        add
        {
            Application.ApplicationInternal.Refresh += value;
        }

        remove
        {
            Application.ApplicationInternal.Refresh -= value;
        }
    }

    /// <summary>
    /// Occurs when the window is about to close
    /// </summary>
    public static event Action<CancelEventArgs> Closing
    {
        add
        {
            Application.ApplicationInternal.Closing += value;
        }

        remove
        {
            Application.ApplicationInternal.Closing -= value;
        }
    }

    /// <summary>
    /// Occurs when the window is about to close
    /// </summary>
    public static event Action<TextInputEventArgs> TextInput
    {
        add
        {
            Application.ApplicationInternal.TextInput += value;
        }

        remove
        {
            Application.ApplicationInternal.TextInput -= value;
        }
    }

    /// <summary>
    /// Occurs whenever a Unicode code point is typed.s
    /// </summary>
    public static event Action<MinimizedEventArgs> Minimized
    {
        add
        {
            Application.ApplicationInternal.Minimized += value;
        }

        remove
        {
            Application.ApplicationInternal.Minimized -= value;
        }
    }

    /// <summary>
    /// Occurs when the window is maximized
    /// </summary>
    public static event Action<MaximizedEventArgs> Maximized
    {
        add
        {
            Application.ApplicationInternal.Maximized += value;
        }

        remove
        {
            Application.ApplicationInternal.Maximized -= value;
        }
    }

    /// <summary>
    /// Occurs when the focus of the window changes.
    /// </summary>
    public static event Action<FocusedChangedEventArgs> FocusedChanged
    {
        add
        {
            Application.ApplicationInternal.FocusedChanged += value;
        }

        remove
        {
            Application.ApplicationInternal.FocusedChanged -= value;
        }
    }

    /// <summary>
    /// Occurs whenever the mouse cursor enters the window bounds
    /// </summary>
    public static event Action MouseEnter
    {
        add
        {
            Application.ApplicationInternal.MouseEnter += value;
        }

        remove
        {
            Application.ApplicationInternal.MouseEnter -= value;
        }
    }

    /// <summary>
    /// Occurs whenever the mouse cursor leaves the window bounds
    /// </summary>
    public static event Action MouseLeave
    {
        add
        {
            Application.ApplicationInternal.MouseLeave += value;
        }

        remove
        {
            Application.ApplicationInternal.MouseLeave -= value;
        }
    }

    /// <summary>
    /// Set window state
    /// </summary>
    public static WindowState State
    {
        get
        {
            return Application.ApplicationInternal.WindowState;
        }

        set
        {
            Application.ApplicationInternal.WindowState = value;
        }
    }

    /// <summary>
    /// Occurs whenever one or more files are dropped on the window
    /// </summary>
    public static event Action<FileDropEventArgs> FileDrop
    {
        add
        {
            Application.ApplicationInternal.FileDrop += value;
        }

        remove
        {
            Application.ApplicationInternal.FileDrop -= value;
        }
    }

    /// <summary>
    /// Set window visibility
    /// </summary>
    public static bool Visible
    {
        get
        {
            return Application.ApplicationInternal.IsVisible;
        }

        set
        {
            Application.ApplicationInternal.IsVisible = value;
        }
    }

    /// <summary>
    /// Set window border
    /// </summary>
    public static WindowBorder Border
    {
        get
        {
            return Application.ApplicationInternal.WindowBorder;
        }

        set
        {
            Application.ApplicationInternal.WindowBorder = value;
        }
    }

    /// <summary>
    /// Set window VSync
    /// </summary>
    public static VSyncMode VSync
    {
        get
        {
            return Application.ApplicationInternal.VSync;
        }

        set
        {
            Application.ApplicationInternal.VSync = value;
        }
    }

    /// <summary>
    /// Gets or sets the aspect ratio this window is locked to
    /// </summary>
    public static (int numerator, int denominator)? AspectRatio
    {
        get
        {
            return Application.ApplicationInternal.AspectRatio;
        }

        set
        {
            Application.ApplicationInternal.AspectRatio = value;
        }
    }

    /// <summary>
    /// Gets or sets a Box2i structure the contains the external bounds
    /// of this window, in screen coordinates. External bounds include the title bar,
    /// borders and drawing area of the window
    /// </summary>
    public static Box2i Bounds
    {
        get
        {
            return Application.ApplicationInternal.Bounds;
        }

        set
        {
            Application.ApplicationInternal.Bounds = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the application window will be 
    /// if the focus changes while the window is in fullscreen mode. The default
    /// is true
    /// </summary>
    public static bool AutoIconify
    {
        get
        {
            return Application.ApplicationInternal.AutoIconify;
        }

        set
        {
            Application.ApplicationInternal.AutoIconify = value;
        }
    }

    /// <summary>
    /// Gets or sets a Box2i structure that contains the internal
    /// bounds of this window, in client coordinates. The internal bounds include the
    /// drawing area of the window, but exclude the title bar and window borders
    /// </summary>
    public static Box2i Rectangle
    {
        get
        {
            return Application.ApplicationInternal.ClientRectangle;
        }

        set
        {
            Application.ApplicationInternal.ClientRectangle = value;
        }
    }

    /// <summary>
    /// Gets the graphics context associated with this window
    /// </summary>
    public static IGLFWGraphicsContext Context
    {
        get
        {
            return Application.ApplicationInternal.Context;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window has been created and has not been destroyed
    /// </summary>
    public static bool Exists
    {
        get
        {
            return Application.ApplicationInternal.Exists;
        }
    }

    /// <summary>
    /// Gets a Vector2i structure that contains the framebuffer size of this window
    /// </summary>
    public static Vector2i FramebufferSize
    {
        get
        {
            return Application.ApplicationInternal.FramebufferSize;
        }
    }

    /// <summary>
    /// Whether or not the window has a transparent framebuffer
    /// </summary>
    public static bool HasTransparentFramebuffer
    {
        get
        {
            return Application.ApplicationInternal.HasTransparentFramebuffer;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether or not this window is event-driven. An
    /// event-driven window will wait for events before updating/rendering. It is useful
    /// for non-game applications, where the program only needs to do any processing
    /// after the user inputs something
    /// </summary>
    public static bool IsEventDriven
    {
        get
        {
            return Application.ApplicationInternal.IsEventDriven;
        }

        set
        {
            Application.ApplicationInternal.IsEventDriven = value;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the shutdown sequence has been initiated for
    /// this window, by calling Window.Close() or hitting the 'close' button
    /// </summary>
    public static bool IsExiting
    {
        get
        {
            return Application.ApplicationInternal.IsExiting;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this window has input focus
    /// </summary>
    public static bool IsFocused
    {
        get
        {
            return Application.ApplicationInternal.IsFocused;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window is fullscreen or not
    /// </summary>
    public static bool IsFullscreen
    {
        get
        {
            return Application.ApplicationInternal.IsFullscreen;
        }
    }

    /// <summary>
    /// Gets or sets a Vector2i structure that contains the maximum external size of this window
    /// </summary>
    /// <remarks>
    /// Set to null to remove the maximum size constraint. If you set size limits and
    /// an aspect ratio that conflict, the results are undefined
    /// </remarks>
    public static Vector2i? MaximumSize
    {
        get
        {
            return Application.ApplicationInternal.MaximumSize;
        }

        set
        {
            Application.ApplicationInternal.MaximumSize = value;
        }
    }

    /// <summary>
    /// Gets or sets a Vector2i structure that contains the minimum external size of this window
    /// </summary>
    /// <remarks>
    /// Set to null to remove the minimum size constraint. If you set size limits and
    /// an aspect ratio that conflict, the results are undefined
    /// </remarks>
    public static Vector2i? MinimumSize
    {
        get
        {
            return Application.ApplicationInternal.MinimumSize;
        }

        set
        {
            Application.ApplicationInternal.MinimumSize = value;
        }
    }

    /// <summary>
    /// Gets or sets the OpenTK.Windowing.Desktop.NativeWindow.WindowState for this window
    /// </summary>
    public static WindowState WindowState
    {
        get
        {
            return Application.ApplicationInternal.WindowState;
        }

        set
        {
            Application.ApplicationInternal.WindowState = value;
        }
    }

    /// <summary>
    /// Gets or sets the OpenTK.Windowing.Desktop.NativeWindow.WindowBorder for this window
    /// </summary>
    public static WindowBorder WindowBorder
    {
        get
        {
            return Application.ApplicationInternal.WindowBorder;
        }

        set
        {
            Application.ApplicationInternal.WindowBorder = value;
        }
    }

    /// <summary>
    /// Centers the window on the monitor where it resides
    /// </summary>
    public static void Center()
    {
        Application.ApplicationInternal.CenterWindow();
    }

    /// <summary>
    /// Close the window
    /// </summary>
    public static void Close()
    {
        Application.ApplicationInternal.Close();
    }

    /// <summary>
    /// Brings the window into focus
    /// </summary>
    public static void Focus()
    {
        Application.ApplicationInternal.Focus();
    }
}
