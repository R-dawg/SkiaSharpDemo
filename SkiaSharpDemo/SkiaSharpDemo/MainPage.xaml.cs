using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaSharpDemo
{
    public partial class MainPage : ContentPage
    {
        private List<ScrollView> scrollList = new List<ScrollView>();

        private SKPaint trendLinePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Orange,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Butt,
            IsAntialias = true
        };

        private SKPaint graphLinePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.LightGray,
            StrokeWidth = 1,
            StrokeCap = SKStrokeCap.Square
        };

        public MainPage()
        {
            InitializeComponent();
            scrollList.Add(Sv1);
            scrollList.Add(Sv2);
            scrollList.Add(Sv3);
        }

        protected override void OnAppearing()
        {
            Sv1.ScrollToAsync(Sv1.Width - 1, Sv1.Height - 1, false);
        }

        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(SKColors.Purple);

        }

        private void canvasView_Line1(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            // draw graph Grid
            canvas.DrawRect(0,0,2000, 180, graphLinePaint);
            for (int i = 50; i < e.Info.Width; i += 50)
            {
                canvas.DrawLine(i, 0, i, 180, graphLinePaint);
            }

            for (int i = 18; i < 180; i += 18)
            {
                canvas.DrawLine(0, i, 2000, i, graphLinePaint);
            }

            // draw trend line
            canvas.DrawLine(0, 100, 800, 100, trendLinePaint);
            canvas.DrawLine(800, 100, 1200, 150, trendLinePaint);
            canvas.DrawLine(1200, 150, 1500, 50, trendLinePaint);

        }

        private void canvasView_Line2(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            // draw graph Grid
            canvas.DrawRect(0,0,2000, 180, graphLinePaint);
            for (int i = 50; i < e.Info.Width; i += 50)
            {
                canvas.DrawLine(i, 0, i, 180, graphLinePaint);
            }

            for (int i = 18; i < 180; i += 18)
            {
                canvas.DrawLine(0, i, 2000, i, graphLinePaint);
            }

            // draw trend line
            canvas.DrawLine(0, 100, 800, 100, trendLinePaint);
            canvas.DrawLine(800, 100, 1200, 150, trendLinePaint);
            canvas.DrawLine(1200, 150, 1500, 50, trendLinePaint);
          
        }

        private void canvasView_Line3(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            // draw graph Grid
            canvas.DrawRect(0,0,2000, 180, graphLinePaint);
            for (int i = 50; i < e.Info.Width; i += 50)
            {
                canvas.DrawLine(i, 0, i, 180, graphLinePaint);
            }

            for (int i = 18; i < 180; i += 18)
            {
                canvas.DrawLine(0, i, 2000, i, graphLinePaint);
            }

            // draw trend line
            canvas.DrawLine(0, 100, 800, 100, trendLinePaint);
            canvas.DrawLine(800, 100, 1200, 150, trendLinePaint);
            canvas.DrawLine(1200, 150, 1500, 50, trendLinePaint);
          
        }

        private void ScrollView_OnScrolled(object sender, ScrolledEventArgs e)
        {
            foreach (ScrollView scrollView in scrollList)
            {
                if (scrollView != sender)
                {
                    scrollView.ScrollToAsync(e.ScrollX, e.ScrollY, true);
            
                }
            }

            // if(sender != Sv1)
            //     Sv1.ScrollToAsync(e.ScrollX, e.ScrollY, true);
            // if(sender != Sv2)
            //     Sv2.ScrollToAsync(e.ScrollX, e.ScrollY, true);
            // if(sender != Sv3)
            //     Sv3.ScrollToAsync(e.ScrollX, e.ScrollY, true);

        }

        // private void ScrollView_OnScrolledPosition(object sender, ScrolledEventArgs e)
        // {
        //     Sv2.SetScrolledPosition(e.ScrollX, 0);
        // }
    }
}
