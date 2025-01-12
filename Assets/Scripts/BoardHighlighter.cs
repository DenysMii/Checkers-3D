using System.Collections.Generic;
using UnityEngine;

public class SquaresHighlighter
{
    [SerializeField] private Material pressedSquare;
    [SerializeField] private Material greenSquare;
    [SerializeField] private Material yellowSquare;
    [SerializeField] private Material redSquare;

    private SquareBehaviour currentSquare;

    public void HighlightSquares(int i, int j, List<int[]> highlightSquaresBPos)
    {
        StaticData.squaresBehaviourScripts[i, j].HighlightSquare(pressedSquare, SquareBehaviour.HighlightStatus.Pressed);
    }
}
