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

        private SKPaint graphLinePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.LightGray,
            StrokeWidth = 1,
            StrokeCap = SKStrokeCap.Square
        };

        private SKPaint trendLinePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Orange,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Butt,
            IsAntialias = true
        };

        private SKPaint dataPointPaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Orange,
            StrokeWidth = 2,
            IsAntialias = true
        };

        public MainPage()
        {
            InitializeComponent();
            // scrollList.Add(Sv1);
            // scrollList.Add(Sv2);
            // scrollList.Add(Sv3);
        }

        protected override void OnAppearing()
        {
            Sv1.ScrollToAsync(Sv1.Width - 1, Sv1.Height - 1, false);
        }

        // private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        // {
        //     SKSurface surface = e.Surface;
        //     SKCanvas canvas = surface.Canvas;
        //
        //     canvas.Clear(SKColors.Purple);
        //
        // }

        private void canvasView_Line1(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            drawGraph(canvas, e);

            List<int> data = new List<int>();
            data.Add(100);
            data.Add(400);
            data.Add(300);
            data.Add(600);

            // draw trend line
            canvas.DrawCircle(300, 100, 6, dataPointPaint);
            canvas.DrawCircle(800, 100, 6, dataPointPaint);
            canvas.DrawCircle(1200, 150, 6, dataPointPaint);
            canvas.DrawCircle(1500, 50, 6, dataPointPaint);
            dataPointPaint.Style = SKPaintStyle.Fill;
            dataPointPaint.Color = SKColors.White;

            canvas.DrawLine(300, 100, 800, 100, trendLinePaint);
            canvas.DrawLine(800, 100, 1200, 150, trendLinePaint);
            canvas.DrawLine(1200, 150, 1500, 50, trendLinePaint);

            canvas.DrawCircle(300, 100, 6, dataPointPaint);
            canvas.DrawCircle(800, 100, 6, dataPointPaint);
            canvas.DrawCircle(1200, 150, 6, dataPointPaint);
            canvas.DrawCircle(1500, 50, 6, dataPointPaint);


        }

        private void canvasView_Line2(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            drawGraph(canvas, e);

            // draw trend line
            canvas.DrawCircle(300, 100, 6, dataPointPaint);
            canvas.DrawCircle(800, 100, 6, dataPointPaint);
            canvas.DrawCircle(1200, 150, 6, dataPointPaint);
            canvas.DrawCircle(1500, 50, 6, dataPointPaint);
            dataPointPaint.Style = SKPaintStyle.Fill;
            dataPointPaint.Color = SKColors.White;

            canvas.DrawLine(300, 100, 800, 100, trendLinePaint);
            canvas.DrawLine(800, 100, 1200, 150, trendLinePaint);
            canvas.DrawLine(1200, 150, 1500, 50, trendLinePaint);

            canvas.DrawCircle(300, 100, 6, dataPointPaint);
            canvas.DrawCircle(800, 100, 6, dataPointPaint);
            canvas.DrawCircle(1200, 150, 6, dataPointPaint);
            canvas.DrawCircle(1500, 50, 6, dataPointPaint);

        }

        private void canvasView_Line3(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            drawGraph(canvas, e);

            // draw trend line
            canvas.DrawCircle(300, 100, 6, dataPointPaint);
            canvas.DrawCircle(800, 100, 6, dataPointPaint);
            canvas.DrawCircle(1200, 150, 6, dataPointPaint);
            canvas.DrawCircle(1500, 50, 6, dataPointPaint);
            dataPointPaint.Style = SKPaintStyle.Fill;
            dataPointPaint.Color = SKColors.White;

            canvas.DrawLine(300, 100, 800, 100, trendLinePaint);
            canvas.DrawLine(800, 100, 1200, 150, trendLinePaint);
            canvas.DrawLine(1200, 150, 1500, 50, trendLinePaint);

            canvas.DrawCircle(300, 100, 6, dataPointPaint);
            canvas.DrawCircle(800, 100, 6, dataPointPaint);
            canvas.DrawCircle(1200, 150, 6, dataPointPaint);
            canvas.DrawCircle(1500, 50, 6, dataPointPaint);

        }

        private void ScrollView_OnScrolled(object sender, ScrolledEventArgs e)
        {
            //foreach (ScrollView scrollView in scrollList)
            //{
            //    if (scrollView != sender)
            //    {
            //        scrollView.ScrollToAsync(e.ScrollX, e.ScrollY, true);
            
            //    }
            //}

            // if(sender != Sv1)
            //     Sv1.ScrollToAsync(e.ScrollX, e.ScrollY, true);
            // if(sender != Sv2)
            Sv2.ScrollToAsync(e.ScrollX, e.ScrollY, true);
            // if(sender != Sv3)
            Sv3.ScrollToAsync(e.ScrollX, e.ScrollY, true);

        }

        private void drawGraph(SKCanvas canvas, SKPaintSurfaceEventArgs e)
        {
            // draw graph Grid
            canvas.DrawRect(0,0,2000, 180, graphLinePaint);

            // draw vertical grid lines
            for (int i = 50; i < 2000; i += 50)
            {
                canvas.DrawLine(i, 0, i, 180, graphLinePaint);
            }

            // draw horizontal grid lines
            for (int i = 18; i < 180; i += 18)
            {
                canvas.DrawLine(0, i, 2000, i, graphLinePaint);
            }
        }
    }
}
