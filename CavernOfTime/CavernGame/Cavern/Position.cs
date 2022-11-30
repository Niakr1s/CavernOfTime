namespace CavernOfTime
{
    public record Position(int Row, int Col)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>New position, relevant to direction</returns>
        /// <exception cref="ArgumentException"></exception>
        public Position MoveToDirection(Direction direction)
        {
            return direction switch
            {
                Direction.North => this with { Row = Row + 1 },
                Direction.South => this with { Row = Row - 1 },
                Direction.East => this with { Col = Col + 1 },
                Direction.West => this with { Col = Col - 1 },
                _ => throw new ArgumentException($"Wrong direction: {direction.ToString()}"),
            };

        }

        public static Position RandomInBounds(Cavern cavern)
        {
            Random rand = new Random();
            int rows = rand.Next(cavern.Rows);
            int cols = rand.Next(cavern.Cols);

            return new Position(rows, cols);
        }

        /// <summary>
        /// Checks, if position is in bounds [rows,cols].
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        private bool IsInBounds(int rows, int cols)
        {
            return Row >= 0 && Row < rows && Col >= 0 && Col < cols;
        }

        /// <summary>
        /// Checks, if position is in bounds of cavern.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        internal bool IsInBounds(Cavern cavern)
        {
            return IsInBounds(cavern.Rows, cavern.Cols);
        }
    }
}