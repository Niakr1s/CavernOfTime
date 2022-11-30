namespace CavernOfTime.Common
{
    internal class LimitQueue<T> : Queue<T>
    {
        public LimitQueue(int limit) : base(limit)
        {
            Limit = limit;
        }



        private int Limit { get; }




        public new void Enqueue(T obj)
        {
            if (Count >= Limit) { Dequeue(); }
            base.Enqueue(obj);
        }
    }
}
