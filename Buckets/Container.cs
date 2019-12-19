using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets
{
    abstract class Container
    {

        #region FIELDS
        private int _volume;
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
        public bool WarnWhenFull { get; set; }
        public bool IsFilled { get { return Volume == Contents ? true : false; } }
        public event BucketOverflowing IsOverflowing;
        #endregion

        #region METHODS
        public void Empty()
        {
            Contents = 0;
        }
        public void Empty(int amount)
        {
            Contents -= amount;
            if (Contents > 0) Contents = 0;
        }
        public void Fill(int amount) 
        {
            if (Contents + amount > Volume)
            {
                int overflow = Contents + amount - Volume;
                IsOverflowing(this, overflow);
            }
            else Contents += amount;
        }
        private void Container_IsOverflowing(object sender, int overflow)
        {
            throw new NotImplementedException();
        }

        public virtual void FillWithBucket(Bucket bucket, int amountFromBucket)
        {
            
        }
        #endregion
    }
}
