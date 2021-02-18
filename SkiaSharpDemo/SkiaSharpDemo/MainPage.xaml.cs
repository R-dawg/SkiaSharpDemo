using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using SkiaSharpDemo.Effects;
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
        private string message = "test";

        // For now I creating custom DataPoint objects as well as SKPoints. We can pass an array of SKPoints to draw lines/points with less code
        // or we can simply pass in the X/Y values to draw a single point (or a pair of X/Y values to draw a single line) 
        private List<DataPoint> _VS1DataPoints = new List<DataPoint>();
        private List<DataPoint> _VS2DataPoints = new List<DataPoint>();
        private List<DataPoint> _VS3DataPoints = new List<DataPoint>();
        private List<DataPoint> _VS4DataPoints = new List<DataPoint>();
        private List<DataPoint> _VS5DataPoints = new List<DataPoint>();
        private List<DataPoint> _VS6DataPoints = new List<DataPoint>();
        private List<DataPoint> _VS7DataPoints = new List<DataPoint>();
        private List<DataPoint> _VS8DataPoints = new List<DataPoint>();

        private List<SKPoint> _SKPointsVS1 = new List<SKPoint>();
        private List<SKPoint> _SKPointsVS1a = new List<SKPoint>();
        private List<SKPoint> _SKPointsVS2 = new List<SKPoint>();
        private List<SKPoint> _SKPointsVS3 = new List<SKPoint>();
        private List<SKPoint> _SKPointsVS4 = new List<SKPoint>();
        private List<SKPoint> _SKPointsVS5 = new List<SKPoint>();
        private List<SKPoint> _SKPointsVS6 = new List<SKPoint>();
        private List<SKPoint> _SKPointsVS7 = new List<SKPoint>();
        private List<SKPoint> _SKPointsVS8 = new List<SKPoint>();

        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;

        public MainPage()
        {
            InitializeComponent();
            InitializeVS1();
            InitializeVS2();
            InitializeVS3();
            InitializeVS4();
            InitializeVS5();
            InitializeVS6();
            InitializeVS7();
            InitializeVS8();

            _trendViewScrollViews.Add(Sv1);
            _trendViewScrollViews.Add(Sv2);
            _trendViewScrollViews.Add(Sv3);
            _trendViewScrollViews.Add(Sv4);
            _trendViewScrollViews.Add(Sv5);
            _trendViewScrollViews.Add(Sv6);
            _trendViewScrollViews.Add(Sv7);
            _trendViewScrollViews.Add(Sv8);

            foreach (ScrollView view in _trendViewScrollViews)
            {
                view.PropertyChanging += GraphPropertyChanging;
            }

        }

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

        private SKPaint dataTextPaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Orange,
            TextSize = 10,
            StrokeWidth = 1,
            IsAntialias = true
        };

        #endregion

        #region Initialize

        
        private void InitializeVS1()
        {
            _VS1DataPoints.Add(new DataPoint(300, 100));
            _VS1DataPoints.Add(new DataPoint(800, 100));
            _VS1DataPoints.Add(new DataPoint(1200, 150, DataPoint.Abnormality.Abnormal));
            _VS1DataPoints.Add(new DataPoint(1500, 50, DataPoint.Abnormality.Critical));

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

        private void InitializeVS4()
        {
            _VS4DataPoints.Add(new DataPoint(300, 100));
            _VS4DataPoints.Add(new DataPoint(800, 100));
            _VS4DataPoints.Add(new DataPoint(1200, 150));
            _VS4DataPoints.Add(new DataPoint(1500, 50));

            _SKPointsVS4.Add(_VS4DataPoints.FirstOrDefault().point);
            foreach (var dataPoint in _VS4DataPoints.Skip(1))
            {
                _SKPointsVS4.Add(dataPoint.point);
                _SKPointsVS4.Add(dataPoint.point);
            }

        }

        private void InitializeVS5()
        {
            _VS5DataPoints.Add(new DataPoint(300, 100));
            _VS5DataPoints.Add(new DataPoint(800, 100));
            _VS5DataPoints.Add(new DataPoint(1200, 150));
            _VS5DataPoints.Add(new DataPoint(1500, 50));

            _SKPointsVS5.Add(_VS5DataPoints.FirstOrDefault().point);
            foreach (var dataPoint in _VS5DataPoints.Skip(1))
            {
                _SKPointsVS5.Add(dataPoint.point);
                _SKPointsVS5.Add(dataPoint.point);
            }

        }

        private void InitializeVS6()
        {
            _VS6DataPoints.Add(new DataPoint(300, 100));
            _VS6DataPoints.Add(new DataPoint(800, 100));
            _VS6DataPoints.Add(new DataPoint(1200, 150));
            _VS6DataPoints.Add(new DataPoint(1500, 50));

            _SKPointsVS6.Add(_VS6DataPoints.FirstOrDefault().point);
            foreach (var dataPoint in _VS6DataPoints.Skip(1))
            {
                _SKPointsVS6.Add(dataPoint.point);
                _SKPointsVS6.Add(dataPoint.point);
            }

        }

        private void InitializeVS7()
        {
            _VS7DataPoints.Add(new DataPoint(300, 100));
            _VS7DataPoints.Add(new DataPoint(800, 100));
            _VS7DataPoints.Add(new DataPoint(1200, 150));
            _VS7DataPoints.Add(new DataPoint(1500, 50));

            _SKPointsVS7.Add(_VS7DataPoints.FirstOrDefault().point);
            foreach (var dataPoint in _VS7DataPoints.Skip(1))
            {
                _SKPointsVS7.Add(dataPoint.point);
                _SKPointsVS7.Add(dataPoint.point);
            }

        }

        private void InitializeVS8()
        {
            _VS8DataPoints.Add(new DataPoint(300, 100));
            _VS8DataPoints.Add(new DataPoint(800, 100));
            _VS8DataPoints.Add(new DataPoint(1200, 150));
            _VS8DataPoints.Add(new DataPoint(1500, 50));

            _SKPointsVS8.Add(_VS8DataPoints.FirstOrDefault().point);
            foreach (var dataPoint in _VS8DataPoints.Skip(1))
            {
                _SKPointsVS8.Add(dataPoint.point);
                _SKPointsVS8.Add(dataPoint.point);
            }

        }


        #endregion

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

        #region TrendViews

        private void TrendView1(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);

            var start = new SKPoint();
            var startColor = new SKColor();
            var endColor = new SKColor();
            foreach(DataPoint dataPoint in _VS1DataPoints)
            {

                switch (dataPoint.range)
                {
                    case DataPoint.Abnormality.Critical:
                        endColor = SKColors.Red;
                        break;

                    case DataPoint.Abnormality.Abnormal:
                        endColor = SKColors.Orange;
                        break;

                    default:
                        endColor = SKColors.Green;
                        break;
                }

                var end = new SKPoint(dataPoint.time, dataPoint.value);
                // Label the value
                dataTextPaint.Color = endColor;
                canvas.DrawText($"{180 - end.Y}", end.X, end.Y - 8, dataTextPaint);

                if(!start.IsEmpty)
                {
                    trendLinePaint.Color = endColor;
                    canvas.DrawLine(start, end, trendLinePaint);

                    dataPointPaint.Color = startColor;
                    dataPointPaint.Style = SKPaintStyle.Stroke;
                    canvas.DrawCircle(start, 6, dataPointPaint);

                    dataPointPaint.Color = endColor;
                    canvas.DrawCircle(end, 6, dataPointPaint);

                    dataPointPaint.Style = SKPaintStyle.Fill;
                    dataPointPaint.Color = SKColors.White;
                    canvas.DrawCircle(start, 6, dataPointPaint);
                    canvas.DrawCircle(end, 6, dataPointPaint);

                    startColor = endColor;
                }
                else
                {
                    dataPointPaint.Color = endColor;
                    canvas.DrawCircle(end, 6, dataPointPaint);
                    dataPointPaint.Style = SKPaintStyle.Fill;
                    dataPointPaint.Color = SKColors.White;
                    canvas.DrawCircle(end, 6, dataPointPaint);
                }

                start = end;

            }

            // 2nd line
            canvas.DrawPoints(SKPointMode.Lines, _SKPointsVS1a.ToArray(), trendLinePaint);
            foreach (SKPoint point in _SKPointsVS1a)
            {
                dataPointPaint.Style = SKPaintStyle.Stroke;
                dataPointPaint.Color = SKColors.Orange;
                trendLinePaint.Color = SKColors.Green;
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

        private void TrendView4(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);
            
            // draw trend line
            trendLinePaint.Color = SKColors.Red;
            canvas.DrawPoints(SKPointMode.Lines, _SKPointsVS4.ToArray(), trendLinePaint);
            foreach (SKPoint point in _SKPointsVS4)
            {
                dataPointPaint.Style = SKPaintStyle.Stroke;
                dataPointPaint.Color = SKColors.Red;
                canvas.DrawCircle(point, 6, dataPointPaint);
                dataPointPaint.Style = SKPaintStyle.Fill;
                dataPointPaint.Color = SKColors.White;
                canvas.DrawCircle(point, 6, dataPointPaint);
            }

        }

        private void TrendView5(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);
            
            // draw trend line
            trendLinePaint.Color = SKColors.Red;
            canvas.DrawPoints(SKPointMode.Lines, _SKPointsVS5.ToArray(), trendLinePaint);
            foreach (SKPoint point in _SKPointsVS5)
            {
                dataPointPaint.Style = SKPaintStyle.Stroke;
                dataPointPaint.Color = SKColors.Red;
                canvas.DrawCircle(point, 6, dataPointPaint);
                dataPointPaint.Style = SKPaintStyle.Fill;
                dataPointPaint.Color = SKColors.White;
                canvas.DrawCircle(point, 6, dataPointPaint);
            }

        }

        private void TrendView6(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);
            
            // draw trend line
            trendLinePaint.Color = SKColors.Red;
            canvas.DrawPoints(SKPointMode.Lines, _SKPointsVS6.ToArray(), trendLinePaint);
            foreach (SKPoint point in _SKPointsVS6)
            {
                dataPointPaint.Style = SKPaintStyle.Stroke;
                dataPointPaint.Color = SKColors.Red;
                canvas.DrawCircle(point, 6, dataPointPaint);
                dataPointPaint.Style = SKPaintStyle.Fill;
                dataPointPaint.Color = SKColors.White;
                canvas.DrawCircle(point, 6, dataPointPaint);
            }

        }

        private void TrendView7(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);
            
            // draw trend line
            trendLinePaint.Color = SKColors.Red;
            canvas.DrawPoints(SKPointMode.Lines, _SKPointsVS7.ToArray(), trendLinePaint);
            foreach (SKPoint point in _SKPointsVS7)
            {
                dataPointPaint.Style = SKPaintStyle.Stroke;
                dataPointPaint.Color = SKColors.Red;
                canvas.DrawCircle(point, 6, dataPointPaint);
                dataPointPaint.Style = SKPaintStyle.Fill;
                dataPointPaint.Color = SKColors.White;
                canvas.DrawCircle(point, 6, dataPointPaint);
            }

        }

        private void TrendView8(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);
            
            // draw trend line
            trendLinePaint.Color = SKColors.Red;
            canvas.DrawPoints(SKPointMode.Lines, _SKPointsVS8.ToArray(), trendLinePaint);
            foreach (SKPoint point in _SKPointsVS8)
            {
                dataPointPaint.Style = SKPaintStyle.Stroke;
                dataPointPaint.Color = SKColors.Red;
                canvas.DrawCircle(point, 6, dataPointPaint);
                dataPointPaint.Style = SKPaintStyle.Fill;
                dataPointPaint.Color = SKColors.White;
                canvas.DrawCircle(point, 6, dataPointPaint);
            }

        }

        
        #endregion

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
                    foreach (var c in VS1_Stack.Children)
                    {
                        if (TooltipEffect.GetHasTooltip(c))
                        {
                            TooltipEffect.SetHasTooltip(c, false);
                            TooltipEffect.SetHasTooltip(c, true);
                        }
                    }
                    // var message = $"Touched {180 -touched.value}";
                    // DisplayAlert("Datapoint clicked", message, "Got it!");
                    // Debug.WriteLine($"Touched {touched.value}");
                }

            }
        }

        void OnPinchUpdated (object sender, PinchGestureUpdatedEventArgs e)
		{
			if (e.Status == GestureStatus.Started) {
				// Store the current scale factor applied to the wrapped user interface element,
				// and zero the components for the center point of the translate transform.
				startScale = Content.Scale;
				Content.AnchorX = 0;
				Content.AnchorY = 0;
			}
			if (e.Status == GestureStatus.Running) {
				// Calculate the scale factor to be applied.
				currentScale += (e.Scale - 1) * startScale;
				currentScale = Math.Max (1, currentScale);

				// The ScaleOrigin is in relative coordinates to the wrapped user interface element,
				// so get the X pixel coordinate.
				double renderedX = Content.X + xOffset;
				double deltaX = renderedX / Width;
				double deltaWidth = Width / (Content.Width * startScale);
				double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

				// The ScaleOrigin is in relative coordinates to the wrapped user interface element,
				// so get the Y pixel coordinate.
				double renderedY = Content.Y + yOffset;
				double deltaY = renderedY / Height;
				double deltaHeight = Height / (Content.Height * startScale);
				double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

				// Calculate the transformed element pixel coordinates.
				double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
				double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

				// Apply translation based on the change in origin.
				Content.TranslationX = targetX.Clamp (-Content.Width * (currentScale - 1), 0);
				Content.TranslationY = targetY.Clamp (-Content.Height * (currentScale - 1), 0);

				// Apply scale factor
				Content.Scale = currentScale;
			}
			if (e.Status == GestureStatus.Completed) {
				// Store the translation delta's of the wrapped user interface element.
				xOffset = Content.TranslationX;
				yOffset = Content.TranslationY;
			}
		}

    }
}
