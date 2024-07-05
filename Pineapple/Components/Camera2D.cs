using Pineapple.Core;
using SkiaSharp;
using System.Drawing;
using System.Numerics;

namespace Pineapple.Components;

public sealed class Camera2D
{
    /// <summary>
    /// Camera position
    /// </summary>
    public Vector2 position;

    /// <summary>
    /// Camera rotation
    /// </summary>
    public float rotation;

    /// <summary>
    /// Camera origin
    /// </summary>
    public Vector2 Origin
    {
        get
        {
            return new Vector2(Window.Size.X * 0.5f, Window.Size.Y * 0.5f);
        }
    }

    /// <summary>
    /// Camera zoom
    /// </summary>
    public float zoom;

    /// <summary>
    /// Camera rectangle boundary in the world space
    /// </summary>
    public RectangleF BoundingRectangle
    {
        get
        {
            return new RectangleF(
                x: position.X - Origin.X / zoom,
                y: position.Y - Origin.Y / zoom,
                width: Window.Size.X / zoom,
                height: Window.Size.Y / zoom
            );
        }
    }

    /// <summary>
    /// The view matrix of the camera
    /// </summary>
    public SKMatrix ViewMatrix
    {
        get
        {
            Vector2 firstTranslation = -(position - Origin);

            return SKMatrix.CreateTranslation(firstTranslation.X, firstTranslation.Y).PostConcat(
                   SKMatrix.CreateTranslation(-Origin.X, -Origin.Y)).PostConcat(
                   SKMatrix.CreateRotationDegrees(rotation)).PostConcat(
                   SKMatrix.CreateScale(zoom, zoom)).PostConcat(
                   SKMatrix.CreateTranslation(Origin.X, Origin.Y));
        }
    }

    public Camera2D(Vector2 position, float rotation, float zoom)
    {
        this.position = position;
        this.rotation = rotation;
        this.zoom = zoom;
    }
}