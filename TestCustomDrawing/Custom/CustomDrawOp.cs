using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using SkiaSharp;

namespace TestCustomDrawing.Custom;

internal class CustomDrawOp : ICustomDrawOperation
{
    public CustomDrawOp(Rect bounds) => Bounds = bounds;

    public Rect Bounds { get; }

    public void Dispose() { }

    public bool HitTest(Point p) => false;

    public bool Equals(ICustomDrawOperation? other) => false;

    public void Render(IDrawingContextImpl context)
    {
        if (context is not ISkiaDrawingContextImpl skia)
        {
            return;
        }

        var bounds = Bounds;

        using var layer = DrawingContextHelper.CreateDrawingContext(bounds.Size, new Vector(96,96), skia.GrContext);
        //using var layer = DrawingContextHelper2.CreateDrawingContext(bounds.Size, new Vector(96,96), skia.GrContext);

        layer.DrawRectangle(new ImmutableSolidColorBrush(Colors.Green), null, new RoundedRect(bounds).Deflate(50, 50));
        layer.DrawEllipse(new ImmutableSolidColorBrush(Colors.Red), null, bounds.Deflate(100));

        using var filter = SKImageFilter.CreateBlur(4, 4, SKShaderTileMode.Clamp);
        using var paint = new SKPaint{ ImageFilter = filter};
        layer.DrawTo(skia, paint);
        //barsLayer.DrawTo_(skia, paint);
    }
}
