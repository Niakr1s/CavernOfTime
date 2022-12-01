using System.Diagnostics;

namespace CavernOfTime
{
    public struct InLineRelation
    {
        public Direction Direction { get; init; }

        public int Range { get; init; }

        public bool IsReachable(Weapon weapon)
        {
            return weapon.Range >= Range;
        }
    }

    public record Position(int Row, int Col)
    {
        /// <summary>
        /// Gets position, relative to direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>New position, relevant to direction</returns>
        /// <exception cref="ArgumentException"></exception>
        public Position To(Direction direction)
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


        /// <summary>
        /// Checks if this and target are are at straight line.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="relation"></param>
        /// <returns>
        /// True, target is in straight line with this.
        /// If target is same as this, returns false.
        /// </returns>
        public InLineRelation? InLineRelation(Position target)
        {
            if (this == target) { return null; }

            int rowDiff = target.Row - Row, colDiff = target.Col - Col;

            bool isInLine = rowDiff == 0 || colDiff == 0;
            if (!isInLine) { return null; }

            InLineRelation? relation = null;
            if (rowDiff > 0)
            {
                relation = new InLineRelation() { Direction = Direction.North, Range = rowDiff };
            }
            else if (rowDiff < 0)
            {
                relation = new InLineRelation() { Direction = Direction.South, Range = -rowDiff };
            }

            // in horisontal line from here
            else if (colDiff > 0)
            {
                relation = new InLineRelation() { Direction = Direction.East, Range = colDiff };
            }
            else if (colDiff < 0)
            {
                relation = new InLineRelation() { Direction = Direction.West, Range = -colDiff };
            }

            // O_O
            else
            {
                throw new UnreachableException();
            }

            return relation;
        }



        public static Position RandomInBounds(CavernMap cavern)
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
        internal bool IsInBounds(CavernMap map)
        {
            return IsInBounds(map.Rows, map.Cols);
        }

        public override string ToString()
        {
            return $"[{Row},{Col}]";
        }
    }
}