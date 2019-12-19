using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets
{
    class Bucket:Container
    {
        public Bucket(int volume = 10):base()
        {
            _volume = volume; 
        }
    }
}
