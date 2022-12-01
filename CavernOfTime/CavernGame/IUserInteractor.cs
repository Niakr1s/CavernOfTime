namespace CavernOfTime
{
    public interface IUserInteractor
    {
        #region Display

        void DisplayWelcomScreen();

        void DisplayGoodbyeScreen();

        /// <summary>
        /// It will be called every time cavern needed to redraw.
        /// </summary>
        /// <param name="cavern"></param>
        void DisplayCavern(Cavern cavern);


        void DisplayError(string errorMessage);

        void DisplayError(Exception e)
        {
            DisplayError(e.Message);
        }

        #endregion



        #region Ask

        void AskCavernDimensions(out int rows, out int cols);

        public PlayerAction WaitPlayerAction();

        #endregion
    }
}