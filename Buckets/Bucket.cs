using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Buckets
{
    class Bucket
    {
        //
        //FIELDS
        //
        private int _volume = 10;
        //
        //CONSTRUCTORS
        //
        public Bucket(int volume = 10 )
        {
            _volume = volume;
        }

        //
        //PROPERTIES
        //
        public int Volume { get { return _volume; } set { if (value < 10) throw new ArgumentException("Volume is too low"); } }
        public int Contents { get; set; }
        public bool WarnWhenFull { get; set; }
        public bool IsFull { get { return Volume == Contents ? true : false; } }
    }
}
