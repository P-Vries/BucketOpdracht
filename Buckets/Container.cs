using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets
{
    abstract class Container
    {

        #region FIELDS
        protected int _volume;
        public delegate void BucketOverflowing(object sender, int Overflow);
        #endregion

        #region CONSTRUCTOR
        public Container()
        {
            Name = new Guid().ToString();
            IsOverflowing += Container_IsOverflowing;
        }
        #endregion

        #region PROPERTIES
        public string Name { get;}
        public int Volume { get { return _volume; }}
        public int Contents { get; set; }
        public bool AllowOverflow { get; set; }
        public bool IsFilled { get { return _volume == Contents ? true : false; } }
        public event BucketOverflowing IsOverflowing;
        #endregion

        #region METHODS
        public void Empty()
        {
            Console.WriteLine("Emptying the bucket");
            Contents = 0;
            Console.WriteLine("Container is empty");
        }
        public void Empty(int amount)
        {
            Console.WriteLine($"Emptying the bucket with {amount} litres");
            if (amount > Contents)
            {
                Console.WriteLine($"Not enough litres. Emptying cancelled");
            }
            else Contents = amount;
            Console.WriteLine($"Container has {Contents} litres");
        }
        public void Fill(int amount) 
        {
            Console.WriteLine($"Filling with {amount} litres");
            if (Contents + amount > Volume)
            {
                if (AllowOverflow)
                {
                    int overflow = Contents + amount - Volume;
                    IsOverflowing(this, overflow);
                }
                else Console.WriteLine("This bucket is not allowed to overflow");
            }
            else Contents += amount;
            Console.WriteLine($"Conainer contains {Contents} litres");
        }
        private void Container_IsOverflowing(object sender, int overflow)
        {
            throw new NotImplementedException();
        }

        public virtual void FillWithBucket(Bucket bucket, int amountFromBucket)
        {
            if (bucket.Contents > amountFromBucket)
            {
                if (amountFromBucket + Contents > _volume)
                {
                    if (AllowOverflow)
                    {
                        bucket.Empty(amountFromBucket);
                        Fill(amountFromBucket);
                    }
                    else Console.WriteLine("This bucket is not allowed to overflow");
                }
                else
                {
                    bucket.Empty(amountFromBucket);
                    Fill(amountFromBucket);
                }
            }
            else Console.WriteLine("Not enough litres in bucket");
            Console.WriteLine($"Conainer contains {Contents} litres");
        }
        #endregion
    }
}
