using System;
using SkiaSharp;

namespace SkiaSharpDemo
{
    public class DataPoint
    {
        public float time;
        public float value;
        public SKPoint point;
        public Abnormality range;

        public DataPoint(int time, int value, Abnormality range = Abnormality.Normal)
        {
            this.time = time;
            this.value = value;
            this.point = new SKPoint(time, value);
            this.range = range;
        }

        public enum Abnormality
        {
            Normal,
            Abnormal,
            Critical
        }
    }
}
