using Avalonia.Controls;
using Avalonia.Media;

namespace TestCustomDrawing.Custom;

internal class CustomControl : Control
{
    public override void Render(DrawingContext context)
    {
        base.Render(context);

        context.Custom(new CustomDrawOp(Bounds));
    }
}
