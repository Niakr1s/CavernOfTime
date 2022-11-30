namespace CavernOfTime
{
    public interface IUserInteractor
    {
        #region Say

        void SayWelcome();

        void SayGoodbye();

        void Say(string message);


        void SayError(string errorMessage);

        void SayError(Exception e)
        {
            SayError(e.Message);
        }


        #endregion




        #region Log

        void AddToLog(string message);

        void ShowLog();

        #endregion




        #region Ask

        void AskCavernDimensions(out int rows, out int cols);

        #endregion

        #region Step

        /// <summary>
        /// Get called at end of step (bunch of actions). For console it can be used to clear screen.
        /// </summary>
        void StepEnd();

        #endregion

        #region Helpers

        #endregion
    }
}