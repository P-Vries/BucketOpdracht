using System;

namespace Buckets
{
    class Program
    {
        static void Main(string[] args)
        {
            Scenario2();
        }

        #region METHODS
        static void Empty(Container cont)
        {
            Console.WriteLine("Emptying container");
            cont.Empty();
            Console.WriteLine("Container is empty");
        }

        static void Empty(Container cont, int amount)
        {
            Console.WriteLine($"Emptying {amount} litre.");
            try
            {
                cont.Empty(amount);
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine($"Operation finished, Container contains {cont.Contents} litres");
        } 

        static void Fill(Container cont, int amount)
        {
            cont.IsOverflowing += Cont_IsOverflowing;
            Console.WriteLine($"Filling container with {amount} litres");
            cont.Fill(amount);
            Console.WriteLine($"Conainer contains {cont.Contents} litres");
        }

        static void FillWithBucket(Container cont, Bucket bucket, int amountFromBucket)
        {
            cont.BucketOverflowingBucket += Cont_BucketOverflowingBucket;
            Console.WriteLine($"Filling container with {amountFromBucket} litres");
            try
            {
                cont.FillWithBucket(bucket, amountFromBucket);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine($"Current container conatins {cont.Contents} litres");
        }
        private static void Cont_IsOverflowing(object sender, int amount)
        {
            Console.WriteLine("Container about to overflow. Checking for allowance");
            Container cont = sender as Container;
            cont.Stop = true;
            if (cont.AllowOverflow) Console.WriteLine($"Overflow was allowed and spilled {amount - cont.Volume} litres");
            else Console.WriteLine("Overflow was not allowed so the operation was cancelled.");
        }

        private static void Cont_BucketOverflowingBucket(object sender,CustomEventArgs e)
        {
            Console.WriteLine("Container about to overflow. Checking for allowance");
            Container cont = sender as Container;
            cont.Stop = true;
            if (cont.AllowOverflow) Console.WriteLine($"Overflow was allowed and spilled {e.Amount - cont.Volume} litres");
            else 
            { 
                Console.WriteLine("Overflow was not allowed so the operation was cancelled.");
                e.FromBucket.Fill(e.OgValue + e.Amount - cont.Volume);
                Console.WriteLine($"Amount remaining in the bucket to transfer from is {e.FromBucket.Contents} litres");
            }
        }
        #endregion

        #region Scenarios
        static void Scenario1()
        {
            Bucket bucket1 = new Bucket();
            bucket1.AllowOverflow = true;
            Fill(bucket1, 1);
        }

        static void Scenario2()
        {
            Bucket bucket1 = new Bucket();
            bucket1.AllowOverflow = false;
            Bucket bucket2 = new Bucket(20);
            bucket2.Fill(20);
            FillWithBucket(bucket1, bucket2, 11);
        }
        #endregion
    }
}
