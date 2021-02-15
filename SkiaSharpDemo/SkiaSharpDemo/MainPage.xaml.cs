using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;
using PropertyChangingEventArgs = Xamarin.Forms.PropertyChangingEventArgs;

namespace SkiaSharpDemo
{
    public partial class MainPage : ContentPage
    {
        private readonly List<ScrollView> _trendViewScrollViews = new List<ScrollView>();
        int interval = 4;
        List<DataPoint> dataPoints = new List<DataPoint>();
        List<SKPoint> sKPoints = new List<SKPoint>();

        private SKPaint graphLinePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.LightGray,
            StrokeWidth = 1,
            StrokeCap = SKStrokeCap.Square
        };

        private SKPaint graphIntervalsPaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.DarkGray,
            StrokeWidth = 1,

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
            dataPoints.Add(new DataPoint(300, 100));
            dataPoints.Add(new DataPoint(800, 100));
            dataPoints.Add(new DataPoint(1200, 150));
            dataPoints.Add(new DataPoint(1500, 50));

            sKPoints.Add(new SKPoint(300, 80));
            sKPoints.Add(new SKPoint(800, 80));
            sKPoints.Add(new SKPoint(1200, 130));
            sKPoints.Add(new SKPoint(1500, 30));

            _trendViewScrollViews.Add(Sv1);
            _trendViewScrollViews.Add(Sv2);
            _trendViewScrollViews.Add(Sv3);

            foreach (ScrollView view in _trendViewScrollViews)
            {
                view.PropertyChanging += GraphPropertyChanging;
            }

        }

        private void GraphPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            var scrollView = (ScrollView) sender;
            if (e.PropertyName == "ScrollX")
            {
                foreach (ScrollView view in _trendViewScrollViews)
                {
                    if (scrollView != view)
                    {
                        view.PropertyChanging -= GraphPropertyChanging;
                        view.ScrollToAsync(scrollView.ScrollX, scrollView.ScrollY, false);
                        view.PropertyChanging += GraphPropertyChanging;
                    }
                }
                // Sv2.ScrollToAsync(scrollView.ScrollX, scrollView.ScrollY, false);
                // Sv3.ScrollToAsync(scrollView.ScrollX, scrollView.ScrollY, false);
                Debug.WriteLine("scrolling");
            }
            Debug.WriteLine("sv1 changed");
        }

        protected override void OnAppearing()
        {
            _ = Sv1.ScrollToAsync(2000, 0, false);
        }

        // private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        // {
        //     SKSurface surface = e.Surface;
        //     SKCanvas canvas = surface.Canvas;
        //
        //     canvas.Clear(SKColors.Purple);
        //
        // }

        private void CanvasView_Line1(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);

            //List<float> data = new List<float>();
            //data.Add(dataPoint.coord[0]);
            //data.Add(400);
            //data.Add(300);
            //data.Add(600);

            // draw trend line
            //canvas.DrawPoints(SKPointMode.Lines, sKPoints.ToArray(), dataPointPaint);
            var start = new SKPoint();
            foreach(DataPoint dataPoint in dataPoints)
            {
                dataPointPaint.Style = SKPaintStyle.Stroke;
                dataPointPaint.Color = SKColors.Orange;
                var end = new SKPoint(dataPoint.time, dataPoint.value);

                if(!start.IsEmpty)
                {
                    canvas.DrawLine(start, end, trendLinePaint);
                    canvas.DrawCircle(start, 6, dataPointPaint);
                    canvas.DrawCircle(end, 6, dataPointPaint);
                    dataPointPaint.Style = SKPaintStyle.Fill;
                    dataPointPaint.Color = SKColors.White;
                    canvas.DrawCircle(start, 6, dataPointPaint);
                    canvas.DrawCircle(end, 6, dataPointPaint);
                }
                else
                {
                    canvas.DrawCircle(end, 6, dataPointPaint);
                    dataPointPaint.Style = SKPaintStyle.Fill;
                    dataPointPaint.Color = SKColors.White;
                    canvas.DrawCircle(end, 6, dataPointPaint);
                }

                start = end;

            }

        }

        private void CanvasView_Line2(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);
            dataPointPaint.Style = SKPaintStyle.Stroke;
            dataPointPaint.Color = SKColors.Green;
            trendLinePaint.Style = SKPaintStyle.Stroke;
            trendLinePaint.Color = SKColors.Green;

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

        private void CanvasView_Line3(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);
            dataPointPaint.Style = SKPaintStyle.Stroke;
            dataPointPaint.Color = SKColors.Red;
            trendLinePaint.Style = SKPaintStyle.Stroke;
            trendLinePaint.Color = SKColors.Red;

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

        private void DrawGraph(SKCanvas canvas, SKPaintSurfaceEventArgs e)
        {
            // draw graph Grid
            canvas.DrawRect(0,0,2000, 180, graphLinePaint);

            // draw vertical grid lines
            for (int i = 50; i < 2000; i += 50)
            {
                canvas.DrawLine(i, 0, i, 180, graphLinePaint);

                if (i % 200 == 0)
                    canvas.DrawText($"{i/50}", x: i, y: 190, graphIntervalsPaint);
            }

            // draw horizontal grid lines
            for (int i = 18; i < 180; i += 18)
            {
                canvas.DrawLine(0, i, 2000, i, graphLinePaint);
                
            }
        }

        void LineView_Touch(System.Object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            if(e.ActionType == SKTouchAction.Pressed)
            {
                var touched = dataPoints.FirstOrDefault(d => Math.Abs(d.value - e.Location.Y) <= 6 && Math.Abs(d.time - e.Location.X) <= 6);
                if(touched != null)
                {
                    var message = $"Touched {touched.value}";
                    DisplayAlert("Datapoint clicked", message, "Got it!");
                    Console.WriteLine($"Touched {touched.value}");
                }

            }
        }
    }
}
