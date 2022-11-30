namespace CavernOfTime
{
    internal interface ICavernDisplayer
    {
        void Display(Cavern cavern);
    }

    internal abstract class CavernDisplayer : ICavernDisplayer
    {
        public abstract void Display(Cavern cavern);
    }
}
