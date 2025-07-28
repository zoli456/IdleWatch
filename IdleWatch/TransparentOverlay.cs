using System.Diagnostics;
using System.Numerics;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.DXGI;
using Vortice.Mathematics;
using static Vanara.PInvoke.User32;
using AlphaMode = Vortice.DCommon.AlphaMode;
using BitmapInterpolationMode = Vortice.Direct2D1.BitmapInterpolationMode;
using FontStyle = Vortice.DirectWrite.FontStyle;
using PixelFormat = Vortice.WIC.PixelFormat;

namespace IdleWatch;

public class TransparentOverlay : NativeWindow
{
    //private const uint LWA_ALPHA = 0x00000002;
    private readonly int screenHeight;
    private readonly int screenWidth;
    private IDWriteTextFormat boldTextFormat;
    internal bool isVisible;
    private string labelText1 = "Label 1";
    private string labelText2 = "Label 2";
    private ID2D1Bitmap overlayImage;
    private IDWriteTextFormat regularTextFormat;
    private ID2D1HwndRenderTarget renderTarget;
    private ID2D1SolidColorBrush textBrush;

    public TransparentOverlay()
    {
        screenWidth = Screen.PrimaryScreen.Bounds.Width;
        screenHeight = Screen.PrimaryScreen.Bounds.Height;
        InitializeOverlayWindow();
        InitializeDirect2D();
        LoadOverlayImage();
    }

    private void InitializeOverlayWindow()
    {
        var cp = new CreateParams
        {
            X = 0,
            Y = 0,
            Width = screenWidth,
            Height = screenHeight,
            Style = unchecked((int)(WindowStyles.WS_POPUP | WindowStyles.WS_VISIBLE)),
            ExStyle = (int)(WindowStylesEx.WS_EX_LAYERED | WindowStylesEx.WS_EX_TRANSPARENT |
                            WindowStylesEx.WS_EX_TOPMOST | (WindowStylesEx)0x00000080)
        };

        CreateHandle(cp);
        SetLayeredWindowAttributes(Handle, 0, 0, LayeredWindowAttributes.LWA_COLORKEY);
    }

    private void InitializeDirect2D()
    {
        var rtProps = new RenderTargetProperties
        {
            PixelFormat = new Vortice.DCommon.PixelFormat(Format.B8G8R8A8_UNorm, AlphaMode.Ignore)
        };
        var hwndProps = new HwndRenderTargetProperties
        {
            Hwnd = Handle,
            PixelSize = new SizeI(screenWidth, screenHeight),
            PresentOptions = PresentOptions.None
        };
        renderTarget = GraphicsFactories.D2DFactory.CreateHwndRenderTarget(rtProps, hwndProps);
        renderTarget.AntialiasMode = AntialiasMode.PerPrimitive;

        textBrush = renderTarget.CreateSolidColorBrush(new Color4(1.0f, 0.0f, 0.0f));

        boldTextFormat =
            GraphicsFactories.DWriteFactory.CreateTextFormat("Arial", FontWeight.Bold, FontStyle.Normal,
                FontStretch.Normal, 48);
        regularTextFormat =
            GraphicsFactories.DWriteFactory.CreateTextFormat("Arial", FontWeight.Regular, FontStyle.Normal,
                FontStretch.Normal, 36);
    }

    private void LoadOverlayImage()
    {
        using var stream = GetType().Assembly.GetManifestResourceStream("IdleWatch.Resources.warning.png");
        if (stream == null) throw new FileNotFoundException("Embedded resource not found.");
        using var decoder = GraphicsFactories.WicFactory.CreateDecoderFromStream(stream);
        using var frame = decoder.GetFrame(0);
        using var converter = GraphicsFactories.WicFactory.CreateFormatConverter();
        // Use the corrected pixel format
        converter.Initialize(frame, PixelFormat.Format32bppPBGRA);
        overlayImage = renderTarget.CreateBitmapFromWicBitmap(converter);
    }

    public void SetOverlayText(string text1, string text2)
    {
        labelText1 = text1;
        labelText2 = text2;

        if (isVisible) DrawOverlayContent();
    }

    public void DrawOverlayContent()
    {
        if (!isVisible) return;

        renderTarget.BeginDraw();
        renderTarget.Clear(null);

        var centerX = screenWidth / 2;
        var centerY = screenHeight / 2;

        // Calculate positions
        var text1Position = DrawTextContent(labelText1, boldTextFormat, centerX, centerY - 200);
        var text2Position = DrawTextContent(labelText2, regularTextFormat, centerX, centerY + 200);

        // Calculate image position between the two lines
        var imageCenterY = (text1Position.Y + text2Position.Y) / 2;
        DrawImageContent(centerX, (int)imageCenterY);

        renderTarget.EndDraw();
    }

    private Vector2 DrawTextContent(string text, IDWriteTextFormat format, int centerX, int centerY)
    {
        using var textLayout =
            GraphicsFactories.DWriteFactory.CreateTextLayout(text, format, screenWidth, screenHeight);
        var textMetrics = textLayout.Metrics;
        var position = new Vector2(centerX - textMetrics.Width / 2, centerY);

        renderTarget.DrawTextLayout(position, textLayout, textBrush);

        return position;
    }

    private void DrawImageContent(int centerX, float centerY)
    {
        if (overlayImage == null)
        {
            Debug.WriteLine("overlayImage is null. Image was not loaded.");
            return;
        }

        var imageWidth = overlayImage.PixelSize.Width;
        var imageHeight = overlayImage.PixelSize.Height;

        var sourceRect = new Rect(
            0,
            0,
            imageWidth,
            imageHeight
        );
        var destinationRect = new Rect(
            centerX - imageWidth / 2.0f,
            centerY - imageHeight / 2.0f,
            imageWidth,
            imageHeight
        );

        Debug.WriteLine($"Drawing image at: {destinationRect}");
        renderTarget.DrawBitmap(overlayImage, destinationRect, 1f, BitmapInterpolationMode.Linear, sourceRect);
    }


    /* protected override void WndProc(ref Message m)
     {
         if (m.Msg == 0x000F && isVisible) DrawOverlayContent();
         base.WndProc(ref m);
     }*/


    public void ShowOverlay()
    {
        if (isVisible) return;
        isVisible = true;
        DrawOverlayContent();
    }

    public void HideOverlay()
    {
        if (!isVisible) return;
        isVisible = false;
        renderTarget.BeginDraw();
        renderTarget.Clear(new Color4(0, 0, 0, 0));
        renderTarget.EndDraw();
    }
}