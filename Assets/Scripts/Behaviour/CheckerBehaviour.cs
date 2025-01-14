using System.Collections.Generic;

public class CheckerBehaviour : PieceBehaviour
{
    protected override void SetMoveHighlightSquaresBPos()
    {
        int cellsDirIndex = isWhite ? 0 : 2;
        highlightedSquaresBPos = new List<int[]>
        {
            CoordsSum(attachedSquareBehaviour.boardPos, cellsDir[cellsDirIndex]),
            CoordsSum(attachedSquareBehaviour.boardPos, cellsDir[cellsDirIndex + 1])
        };
    }
}
