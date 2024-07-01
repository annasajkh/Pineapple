using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Pineapple.Core;

public static class Input
{
    /// <summary>
    /// Gets the current state of the keyboard as of the last time the window processed events
    /// </summary>
    public static KeyboardState KeyboardState
    {
        get
        {
            return Application.ApplicationInternal.KeyboardState;
        }
    }

    /// <summary>
    /// The current mouse position
    /// </summary>
    public static Vector2 MousePosition
    {
        get
        {
            return Application.ApplicationInternal.MousePosition;
        }
    }

    /// <summary>
    /// Gets the current state of the mouse as of the last time the window processed events
    /// </summary>
    public static MouseState MouseState
    {
        get
        {
            return Application.ApplicationInternal.MouseState;
        }
    }

    /// <summary>
    ///     Gets a OpenTK.Mathematics.Vector2 representing the absolute position of the pointer
    ///     in the previous frame, relative to the top-left corner of the contents of the
    ///     window
    /// </summary>
    public static Vector2 MousePreviousPosition
    {
        get
        {
            return Application.ApplicationInternal.MouseState.PreviousPosition;
        }
    }

    /// <summary>
    /// Get a Vector2 representing the previous scroll of the mouse wheel
    /// </summary>
    public static Vector2 MousePreviousScroll
    {
        get
        {
            return Application.ApplicationInternal.MouseState.PreviousScroll;
        }
    }

    /// <summary>
    ///  Gets a OpenTK.Mathematics.Vector2 representing the amount that the mouse moved
    ///  since the last frame. This does not necessarily correspond to pixels, for example
    ///  in the case of raw input
    /// </summary>
    public static Vector2 MouseDelta
    {
        get
        {
            return Application.ApplicationInternal.MouseState.Delta;
        }
    }

    /// <summary>
    /// Get a Vector2 representing the position of the mouse wheel
    /// </summary>
    public static Vector2 MouseWheelScroll
    {
        get
        {
            return Application.ApplicationInternal.MouseState.Scroll;
        }
    }

    /// <summary>
    /// Get a Vector2 representing the amount that the mouse wheel moved since the last frame
    /// </summary>
    public static Vector2 MouseWheelScrollDelta
    {
        get
        {
            return Application.ApplicationInternal.MouseState.ScrollDelta;
        }
    }

    /// <summary>
    /// true if any key is down; otherwise, false
    /// </summary>
    public static bool IsAnyKeyDown
    {
        get
        {
            return Application.ApplicationInternal.IsAnyKeyDown;
        }
    }

    /// <summary>
    /// true if any mouse button is pressed; otherwise, false
    /// </summary>
    public static bool IsAnyMouseButtonDown
    {
        get
        {
            return Application.ApplicationInternal.IsAnyMouseButtonDown;
        }
    }

    /// <summary>
    /// Get joystick states
    /// </summary>
    public static IReadOnlyList<JoystickState> JoystickStates
    {
        get
        {
            return Application.ApplicationInternal.JoystickStates;
        }
    }

    /// <summary>
    /// Gets or sets the current cursor
    /// </summary>
    public static MouseCursor Cursor
    {
        get
        {
            return Application.ApplicationInternal.Cursor;
        }

        set
        {
            Application.ApplicationInternal.Cursor = value;
        }
    }

    /// <summary>
    /// Gets or sets the cursor state of the windows cursor
    /// </summary>
    public static CursorState CursorState
    {
        get
        {
            return Application.ApplicationInternal.CursorState;
        }

        set
        {
            Application.ApplicationInternal.CursorState = value;
        }
    }

    /// <summary>
    /// Occurs when a joystick is connected or disconnected
    /// </summary>
    public static event Action<JoystickEventArgs> JoystickConnected
    {
        add
        {
            Application.ApplicationInternal.JoystickConnected += value;
        }

        remove
        {
            Application.ApplicationInternal.JoystickConnected -= value;
        }
    }

    /// <summary>
    /// Occurs whenever a keyboard key is pressed
    /// </summary>
    public static event Action<KeyboardKeyEventArgs> KeyDown
    {
        add
        {
            Application.ApplicationInternal.KeyDown += value;
        }

        remove
        {
            Application.ApplicationInternal.KeyDown -= value;
        }
    }


    /// <summary>
    /// Occurs whenever a keyboard key is released
    /// </summary>
    public static event Action<KeyboardKeyEventArgs> KeyUp
    {
        add
        {
            Application.ApplicationInternal.KeyUp += value;
        }

        remove
        {
            Application.ApplicationInternal.KeyUp -= value;
        }
    }

    /// <summary>
    /// Occurs whenever a mouse button is clicked
    /// </summary>
    public static event Action<MouseButtonEventArgs> MouseDown
    {
        add
        {
            Application.ApplicationInternal.MouseDown += value;
        }

        remove
        {
            Application.ApplicationInternal.MouseDown -= value;
        }
    }

    /// <summary>
    /// Occurs whenever a mouse button is released
    /// </summary>
    public static event Action<MouseButtonEventArgs> MouseUp
    {
        add
        {
            Application.ApplicationInternal.MouseUp += value;
        }

        remove
        {
            Application.ApplicationInternal.MouseUp -= value;
        }
    }

    /// <summary>
    /// Occurs whenever the mouse cursor is moved
    /// </summary>
    public static event Action<MouseMoveEventArgs> MouseMove
    {
        add
        {
            Application.ApplicationInternal.MouseMove += value;
        }

        remove
        {
            Application.ApplicationInternal.MouseMove -= value;
        }
    }

    /// <summary>
    /// Occurs whenever a mouse wheel is moved
    /// </summary>
    public static event Action<MouseWheelEventArgs> MouseWheel
    {
        add
        {
            Application.ApplicationInternal.MouseWheel += value;
        }

        remove
        {
            Application.ApplicationInternal.MouseWheel -= value;
        }
    }

    /// <summary>
    /// Occurs whenever a Unicode code point is typed
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
    /// Gets a System.Boolean indicating whether this key is currently down
    /// </summary>
    /// <param name="keys">The key to check</param>
    /// <returns>true if key is in the down state; otherwise, false</returns>
    public static bool IsKeyDown(Keys keys)
    {
        return Application.ApplicationInternal.IsKeyDown(keys);
    }

    /// <summary>
    /// Gets whether the specified key is pressed in the current frame but released in the previous frame
    /// </summary>
    /// <param name="keys">The key to check</param>
    /// <returns>True if the key is pressed in this frame, but not the last frame</returns>
    public static bool IsKeyPressed(Keys keys)
    {
        return Application.ApplicationInternal.IsKeyPressed(keys);
    }

    /// <summary>
    /// Gets whether the specified key is released in the current frame but pressed in the previous frame
    /// </summary>
    /// <param name="keys">The key to check</param>
    /// <returns>True if the key is released in this frame, but pressed the last frame</returns>
    public static bool IsKeyReleased(Keys keys)
    {
        return Application.ApplicationInternal.IsKeyReleased(keys);
    }

    /// <summary>
    /// Gets a System.Boolean indicating whether this button is currently down
    /// </summary>
    /// <param name="button">The OpenTK.Windowing.GraphicsLibraryFramework.MouseButton to check</param>
    /// <returns>true if button is in the down state; otherwise, false</returns>
    public static bool IsMouseButtonDown(MouseButton button)
    {
        return Application.ApplicationInternal.IsMouseButtonDown(button);
    }

    /// <summary>
    /// Gets whether the specified mouse button is pressed in the current frame but released in the previous frame
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <returns>True if the button is pressed in this frame, but not the last frame</returns>
    public static bool IsMouseButtonPressed(MouseButton button)
    {
        return Application.ApplicationInternal.IsMouseButtonPressed(button);
    }

    /// <summary>
    /// Gets whether the specified mouse button is released in the current frame but pressed in the previous frame
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <returns>True if the button is released in this frame, but pressed the last frame</returns>
    public static bool IsMouseButtonReleased(MouseButton button)
    {
        return Application.ApplicationInternal.IsMouseButtonReleased(button);
    }
}