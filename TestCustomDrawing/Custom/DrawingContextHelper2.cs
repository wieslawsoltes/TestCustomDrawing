using System;
using Avalonia;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using SkiaSharp;

namespace TestCustomDrawing.Custom;

internal static class DrawingContextHelper2
{
    public static ISkiaDrawingContextImpl CreateDrawingContext(Size size, Vector dpi, GRContext? grContext = null)
    {
        if (grContext is null)
        {
            var surface = SKSurface.Create(
                new SKImageInfo(
                    (int)Math.Ceiling(size.Width),
                    (int)Math.Ceiling(size.Height),
                    SKImageInfo.PlatformColorType,
                    SKAlphaType.Premul));

            return DrawingContextHelper.WrapSkiaSurface(surface, dpi, surface);
        }
        else
        {
            var surface = SKSurface.Create(grContext, false,
                new SKImageInfo(
                    (int)Math.Ceiling(size.Width),
                    (int)Math.Ceiling(size.Height),
                    SKImageInfo.PlatformColorType,
                    SKAlphaType.Premul));

            return DrawingContextHelper.WrapSkiaSurface(surface, grContext, dpi, surface);
        }
    }
        
    public static void DrawTo_(this ISkiaDrawingContextImpl source, ISkiaDrawingContextImpl destination, SKPaint? paint = null)
    {
        destination.SkCanvas.DrawSurface(source.SkSurface, new SKPoint(0, 0), paint);
    }
}
