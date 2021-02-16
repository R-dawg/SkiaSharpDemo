using System;
using SkiaSharp;

namespace SkiaSharpDemo
{
    public class DataPoint
    {
        public float time;
        public float value;
        public SKPoint point;

        public DataPoint(int time, int value)
        {
            this.time = time;
            this.value = value;
            this.point = new SKPoint(time, value);
        }
    }
}
