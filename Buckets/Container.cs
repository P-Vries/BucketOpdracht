using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets
{
    abstract class Container
    {

        #region FIELDS
        protected int _volume;
        public delegate void BucketOverflowing(object sender, int amount);
        public delegate void BucketOverflowingFromBucket(object sender, CustomEventArgs e);
        #endregion

        #region CONSTRUCTOR
        public Container()
        {
            Name = new Guid().ToString();
        }
        #endregion

        #region PROPERTIES
        public string Name { get;}
        public int Volume { get { return _volume; }}
        public int Contents { get; protected set; }
        public bool AllowOverflow { get; set; }

        public event BucketOverflowing IsOverflowing;
        public event BucketOverflowingFromBucket BucketOverflowingBucket;
        public bool Stop { get; set; }
        #endregion

        #region METHODS
        public void Empty()
        {
            Contents = 0;
        }
        public void Empty(int amount)
        {
            if (amount > Contents)
            {
                throw new ArgumentException("Not enough litres to empty. Cancelling operation");
            }
            else 
            {
                for (int i = 0; i < amount; i++)
                {
                    Contents -= 1;
                }
            }
        }
        public void Fill(int amount, Bucket bucket) 
        { 
            if (Contents + amount > Volume)
            {
                int ogAmount = Contents;
                while (!Stop)
                {
                    Contents += 1;
                    if (Contents == Volume) BucketOverflowingBucket(this, new CustomEventArgs(amount, ogAmount, bucket));
                }
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    Contents += 1;
                }
            }
        }
        public void Fill(int amount)
        {
            if (Contents + amount > Volume)
            {
                while (!Stop)
                {
                    Contents += 1;
                    if (Contents == Volume) IsOverflowing(this, amount);
                }
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    Contents += 1;
                }
            }
        }
        public void FillWithBucket(Bucket bucket, int amountFromBucket)
        {
            if (bucket.Contents > amountFromBucket)
            {
                bucket.Empty(amountFromBucket);
                Fill(amountFromBucket, bucket);
            }
            else throw new ArgumentException("Not enough litres in the bucket to transfer. Cancelling operation");
        }
        #endregion
    }
}
