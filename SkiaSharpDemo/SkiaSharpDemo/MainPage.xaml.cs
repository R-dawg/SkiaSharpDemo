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
        List<DataPoint> _VS1DataPoints = new List<DataPoint>();
        List<DataPoint> _VS2DataPoints = new List<DataPoint>();
        List<SKPoint> _SKPointsVS1 = new List<SKPoint>();
        List<SKPoint> _SKPointsVS1a = new List<SKPoint>();
        List<SKPoint> _SKPointsVS2 = new List<SKPoint>();
        List<DataPoint> _VS3DataPoints = new List<DataPoint>();
        List<SKPoint> _SKPointsVS3 = new List<SKPoint>();

        #region SKPaints

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

        #endregion
        

        public MainPage()
        {
            InitializeComponent();
            InitializeVS1();
            InitializeVS2();
            InitializeVS3();

            _trendViewScrollViews.Add(Sv1);
            _trendViewScrollViews.Add(Sv2);
            _trendViewScrollViews.Add(Sv3);

            foreach (ScrollView view in _trendViewScrollViews)
            {
                view.PropertyChanging += GraphPropertyChanging;
            }

        }

        private void InitializeVS1()
        {
            _VS1DataPoints.Add(new DataPoint(300, 100));
            _VS1DataPoints.Add(new DataPoint(800, 100));
            _VS1DataPoints.Add(new DataPoint(1200, 150));
            _VS1DataPoints.Add(new DataPoint(1500, 50));

            _SKPointsVS1.Add(_VS1DataPoints.FirstOrDefault().point);
            foreach (var dataPoint in _VS1DataPoints.Skip(1))
            {
                _SKPointsVS1.Add(dataPoint.point);
                _SKPointsVS1.Add(dataPoint.point);
            }

            // Just adding a 2nd line to see what it looks like on the graph
            _SKPointsVS1a.Add(new SKPoint(300, 70));
            _SKPointsVS1a.Add(new SKPoint(800, 50));
            _SKPointsVS1a.Add(new SKPoint(800, 50));
            _SKPointsVS1a.Add(new SKPoint(1200, 110));
            _SKPointsVS1a.Add(new SKPoint(1200, 110));
            _SKPointsVS1a.Add(new SKPoint(1500, 15));
        }

        private void InitializeVS2()
        {
            _VS2DataPoints.Add(new DataPoint(300, 100));
            _VS2DataPoints.Add(new DataPoint(800, 100));
            _VS2DataPoints.Add(new DataPoint(1200, 150));
            _VS2DataPoints.Add(new DataPoint(1500, 50));

            _SKPointsVS2.Add(_VS2DataPoints.FirstOrDefault().point);
            foreach (var dataPoint in _VS2DataPoints.Skip(1))
            {
                _SKPointsVS2.Add(dataPoint.point);
                _SKPointsVS2.Add(dataPoint.point);
            }

        }

        private void InitializeVS3()
        {
            _VS3DataPoints.Add(new DataPoint(300, 100));
            _VS3DataPoints.Add(new DataPoint(800, 100));
            _VS3DataPoints.Add(new DataPoint(1200, 150));
            _VS3DataPoints.Add(new DataPoint(1500, 50));

            _SKPointsVS3.Add(_VS3DataPoints.FirstOrDefault().point);
            foreach (var dataPoint in _VS3DataPoints.Skip(1))
            {
                _SKPointsVS3.Add(dataPoint.point);
                _SKPointsVS3.Add(dataPoint.point);
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
            }
        }

        protected override void OnAppearing()
        {
            Sv1.ScrollToAsync(1200, 0, false);
        }

        private void TrendView1(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);

            // draw trend line
            trendLinePaint.Color = SKColors.Orange;
            canvas.DrawPoints(SKPointMode.Lines, _SKPointsVS1.ToArray(), trendLinePaint);
            foreach (SKPoint point in _SKPointsVS1)
            {
                dataPointPaint.Style = SKPaintStyle.Stroke;
                dataPointPaint.Color = SKColors.Orange;
                canvas.DrawCircle(point, 6, dataPointPaint);
                dataPointPaint.Style = SKPaintStyle.Fill;
                dataPointPaint.Color = SKColors.White;
                canvas.DrawCircle(point, 6, dataPointPaint);
            }

            // 2nd line
            canvas.DrawPoints(SKPointMode.Lines, _SKPointsVS1a.ToArray(), trendLinePaint);
            foreach (SKPoint point in _SKPointsVS1a)
            {
                dataPointPaint.Style = SKPaintStyle.Stroke;
                dataPointPaint.Color = SKColors.Orange;
                canvas.DrawCircle(point, 6, dataPointPaint);
                dataPointPaint.Style = SKPaintStyle.Fill;
                dataPointPaint.Color = SKColors.White;
                canvas.DrawCircle(point, 6, dataPointPaint);
            }

        }

        private void TrendView2(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);

            // draw trend line
            trendLinePaint.Color = SKColors.Green;
            canvas.DrawPoints(SKPointMode.Lines, _SKPointsVS2.ToArray(), trendLinePaint);
            foreach (SKPoint point in _SKPointsVS2)
            {
                dataPointPaint.Style = SKPaintStyle.Stroke;
                dataPointPaint.Color = SKColors.Green;
                canvas.DrawCircle(point, 6, dataPointPaint);
                dataPointPaint.Style = SKPaintStyle.Fill;
                dataPointPaint.Color = SKColors.White;
                canvas.DrawCircle(point, 6, dataPointPaint);
            }

        }

        private void TrendView3(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);
            
            // draw trend line
            trendLinePaint.Color = SKColors.Red;
            canvas.DrawPoints(SKPointMode.Lines, _SKPointsVS3.ToArray(), trendLinePaint);
            foreach (SKPoint point in _SKPointsVS3)
            {
                dataPointPaint.Style = SKPaintStyle.Stroke;
                dataPointPaint.Color = SKColors.Red;
                canvas.DrawCircle(point, 6, dataPointPaint);
                dataPointPaint.Style = SKPaintStyle.Fill;
                dataPointPaint.Color = SKColors.White;
                canvas.DrawCircle(point, 6, dataPointPaint);
            }

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
                var touched = _VS1DataPoints.FirstOrDefault(d => Math.Abs(d.value - e.Location.Y) <= 6 && Math.Abs(d.time - e.Location.X) <= 6);
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
