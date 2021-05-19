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
        private bool _loadingGraphs = false;

        // For now I am creating custom DataPoint objects as well as SKPoints. We can pass an array of SKPoints to draw lines/points with less code
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
            // _trendViewScrollViews.Add(Sv2);
            // _trendViewScrollViews.Add(Sv3);
            // _trendViewScrollViews.Add(Sv4);
            // _trendViewScrollViews.Add(Sv5);
            // _trendViewScrollViews.Add(Sv6);
            // _trendViewScrollViews.Add(Sv7);
            // _trendViewScrollViews.Add(Sv8);

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
            _VS1DataPoints.Add(new DataPoint(150, 100));
            _VS1DataPoints.Add(new DataPoint(300, 100));
            _VS1DataPoints.Add(new DataPoint(500, 150, DataPoint.Abnormality.Abnormal));
            _VS1DataPoints.Add(new DataPoint(700, 50, DataPoint.Abnormality.Critical));
            _VS1DataPoints.Add(new DataPoint(840, 140, DataPoint.Abnormality.Abnormal));

            // Creating the SKPoints is not really necessary. I have done this so that I can easily change techniques for drawing lines/points.
            // SKPoints are just more convenient because we don't have to provide the x and y coordinates separately
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
            _SKPointsVS1a.Add(new SKPoint(1500, 15));
            _SKPointsVS1a.Add(new SKPoint(1710, 30));
        }

        private void InitializeVS2()
        {
            _VS2DataPoints.Add(new DataPoint(300, 100));
            _VS2DataPoints.Add(new DataPoint(800, 100));
            _VS2DataPoints.Add(new DataPoint(1200, 150));
            _VS2DataPoints.Add(new DataPoint(1500, 50));
            _VS2DataPoints.Add(new DataPoint(1710, 120));


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
            _VS3DataPoints.Add(new DataPoint(1710, 120));


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
            _VS4DataPoints.Add(new DataPoint(1710, 120));


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
            _VS5DataPoints.Add(new DataPoint(1710, 120));


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
            _VS6DataPoints.Add(new DataPoint(1710, 120));


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
            _VS7DataPoints.Add(new DataPoint(1710, 120));


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
            _VS8DataPoints.Add(new DataPoint(1710, 120));


            _SKPointsVS8.Add(_VS8DataPoints.FirstOrDefault().point);
            foreach (var dataPoint in _VS8DataPoints.Skip(1))
            {
                _SKPointsVS8.Add(dataPoint.point);
                _SKPointsVS8.Add(dataPoint.point);
            }

        }


        #endregion


        #region TrendViews

        private void TrendView1(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            DrawGraph(canvas, e);

            var start = new SKPoint();
            var startColor = new SKColor();
            var endColor = new SKColor();

            // Top line just to show 2 lines
            // This line also demonstrates how we can use DrawPoints() to automatically connect an array of SKPoints with a line
            // Note that this is just a line with no datapoint circles. There is also SPointMode.Points which will plot points based on the array
            // but I found the circles to be too small for what I was doing and they were also filled in with the selected color
            // canvas.DrawPoints(SKPointMode.Lines, _SKPointsVS1a.ToArray(), trendLinePaint);

            // foreach (SKPoint point in _SKPointsVS1a)
            // {
            //     // First we draw a circle using our selected color and style (in this case Stroke) to make an empty circle
            //     dataPointPaint.Style = SKPaintStyle.Stroke;
            //     dataPointPaint.Color = SKColors.Orange;
            //     canvas.DrawCircle(point, 6, dataPointPaint);
            //
            //     // Because the circle is drawn on top of the trend line, we can still see the line through the circle
            //     // To achieve the effect of an empty circle, we redraw the circle but this time using a fill
            //     dataPointPaint.Style = SKPaintStyle.Fill;
            //     dataPointPaint.Color = SKColors.White;
            //     canvas.DrawCircle(point, 6, dataPointPaint);
            // }

            // For the bottom line, this is a way we can make each line a different color. Each line will need to be
            // drawn individually with DrawLine() instead of DrawPoints() so that we can switch colors in between
            foreach(DataPoint dataPoint in _VS1DataPoints)
            {
                // Use the Range value on the Datapoint to determine the abnormality of each datapoint
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
                // We can draw text on the canvas too so pick a color and draw the text at the coordinates
                dataTextPaint.Color = endColor;
                canvas.DrawText($"{180 - end.Y}", end.X, end.Y - 8, dataTextPaint);

                // If there is already a datapoint on the graph to connect from
                if(!start.IsEmpty)
                {
                    // Draw a line using the color of the current data
                    trendLinePaint.Color = endColor;
                    canvas.DrawLine(start, end, trendLinePaint);

                    // Then redraw the previous data point (to get an empty circle back on top of the line)
                    dataPointPaint.Color = startColor;
                    dataPointPaint.Style = SKPaintStyle.Stroke;
                    canvas.DrawCircle(start, 6, dataPointPaint);

                    // Draw the current datapoint
                    dataPointPaint.Color = endColor;
                    canvas.DrawCircle(end, 6, dataPointPaint);

                    // Fill both in so we get those beautiful empty circles
                    dataPointPaint.Style = SKPaintStyle.Fill;
                    dataPointPaint.Color = SKColors.White;
                    canvas.DrawCircle(start, 6, dataPointPaint);
                    canvas.DrawCircle(end, 6, dataPointPaint);

                    // This point is where we will start the next line so save the color
                    startColor = endColor;
                }
                else
                {
                    // If there are no points on the graph yet, just draw our first datapoint circle
                    dataPointPaint.Style = SKPaintStyle.Stroke;
                    dataPointPaint.Color = endColor;
                    canvas.DrawCircle(end, 6, dataPointPaint);
                    dataPointPaint.Style = SKPaintStyle.Fill;
                    dataPointPaint.Color = SKColors.White;
                    canvas.DrawCircle(end, 6, dataPointPaint);
                }

                // This point is where we will start the next line so it becomes the new start
                start = end;

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

                dataTextPaint.Color = SKColors.Green;
                canvas.DrawText($"{180 - point.Y}", point.X, point.Y - 8, dataTextPaint);
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
            trendLinePaint.Color = SKColor.Parse("#f7a663");
            dataPointPaint.Color = SKColor.Parse("#f7a663");
            canvas.DrawPoints(SKPointMode.Lines, _SKPointsVS4.ToArray(), trendLinePaint);
            foreach (SKPoint point in _SKPointsVS4)
            {
                dataPointPaint.Style = SKPaintStyle.Stroke;
                dataPointPaint.Color = SKColor.Parse("#f7a663");
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

            // Draw box for latest values?
            // canvas.DrawRect(2000,0,180, 180, dataPointPaint);

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
                        if (_loadingGraphs)
                        {
                            view.ScrollToAsync(1200, 0, false);
                        }

                        else
                        {
                            view.ScrollToAsync(scrollView.ScrollX, scrollView.ScrollY, false);
                        }
                            
                        view.PropertyChanging += GraphPropertyChanging;
                    }
                }

                _loadingGraphs = false;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Yield();

            _loadingGraphs = true;
            await Sv1.ScrollToAsync(1200, 0, false);
        }

        private void DrawGraph(SKCanvas canvas, SKPaintSurfaceEventArgs e)
        {
            // Draw the rectangle that will contain the graph Grid
            canvas.DrawRect(0,0,2000, 180, graphLinePaint);

            // Draw vertical grid lines and label them at certain intervals
            // Here I just divided the height and width evenly to draw some lines. We can come up with a formula for line spacings
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
                var touched = _VS1DataPoints.FirstOrDefault(d => Math.Abs(d.time - e.Location.X) <= 6);
                // if(touched != null)
                // {
                    // foreach (var c in VS1_Stack.Children)
                    // {
                    //     if (TooltipEffect.GetHasTooltip(c))
                    //     {
                    //         TooltipEffect.SetHasTooltip(c, false);
                    //         TooltipEffect.SetHasTooltip(c, true);
                    //     }
                    // }
                    var screenWidth = absolute.Width;
                    var graphWidth = LineView.Width;
                    bar.X1 = e.Location.X;
                    bar.X2 = e.Location.X;
                    // var message = $"Touched {touched.value} and a time would go here for now it is the X-axis: {touched.time/50}";
                    // DisplayAlert("Datapoint clicked", message, "Got it!");
                    // Debug.WriteLine($"Touched {touched.value}");
                // }

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
