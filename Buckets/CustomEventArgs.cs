using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets
{
    class CustomEventArgs:EventArgs
    {
        public CustomEventArgs(int amount, int ogValue, Bucket fromBucket)
        {
            Amount = amount;
            OgValue = ogValue;
            FromBucket = fromBucket;
        }

        public int Amount { get; set; }
        public int OgValue { get; set; }
        public Bucket FromBucket { get; set; }
    }
}
