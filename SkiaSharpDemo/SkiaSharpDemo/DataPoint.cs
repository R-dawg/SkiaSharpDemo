using System;
namespace SkiaSharpDemo
{
    public class DataPoint
    {
        public float time;
        public float value;
        //public DateTime time;

        public DataPoint(int time, int value)
        {
            this.time = time;
            this.value = value;
            //this.time = DateTime.Now;
        }
    }
}
