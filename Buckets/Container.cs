using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets
{
    abstract class Container
    {

        #region FIELDS
        private int _volume; 
        #endregion

        #region PROPERTIES
        public int Volume { get { return _volume; }}
        public int Contents { get; set; }
        public bool WarnWhenFull { get; set; }
        public bool IsFull { get { return Volume == Contents ? true : false; } }
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
        public void Fill(int amount, out int overflow) 
        {
            overflow = 0;
            if(Contents + amount > Volume)
            {
                overflow = (Contents + amount) - Volume;
                Console.WriteLine($"Bucket will overflow with an excess of{overflow}. Continue? (y/n)"); 
                Contents = (Console.ReadKey().Key == ConsoleKey.Y)?Volume:Contents;
            }
        }
        #endregion
    }
}
