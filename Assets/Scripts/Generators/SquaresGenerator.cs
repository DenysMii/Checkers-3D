using UnityEngine;

public class SquaresGenerator : MonoBehaviour
{
    [SerializeField] private GameObject squaresHolder;
    [SerializeField] private GameObject squarePrefab;

    public void LoadSquaresBehaviours()
    {
        SquareBehaviour[] squareBehaviours = squaresHolder.GetComponentsInChildren<SquareBehaviour>();
        int squaresCounter = 0;

        for (int i = 0; i < 8; i++)
        {
            for (int j = i % 2; j < 8; j += 2)
            {
                StaticData.squares[i, j] = squareBehaviours[squaresCounter];
                squaresCounter++;
            }
        }
    }

    [ContextMenu("Generate Squares")]
    public void GenerateSquares()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = i % 2; j < 8; j += 2)
            {
                GenerateSquare(i, j);
            }
        }
    }

    private void GenerateSquare(int i, int j)
    {
        Vector3 pos = new Vector3(StaticData.startPos + j * StaticData.squaresDiff, 0.5f, StaticData.startPos + i * StaticData.squaresDiff);
        GameObject newSquare = Instantiate(squarePrefab, pos, squarePrefab.transform.rotation, squaresHolder.transform);

        string name = (char)('A' + j) + (i + 1).ToString();
        newSquare.name = name;

        GenerateSquareBehaviour(newSquare, i, j);
    }

    private void GenerateSquareBehaviour(GameObject square, int i, int j)
    {
        SquareBehaviour newSquareBehaviour = null;

        switch (i)
        {
            case 0:
            case 7:
                PromoteSquareBehaviour promoteSquare = square.AddComponent<PromoteSquareBehaviour>();
                promoteSquare.promoteForWhite = (i == 7); // true for case 7, false for case 0
                newSquareBehaviour = promoteSquare;
                break;
            default:
                newSquareBehaviour = square.AddComponent<SquareBehaviour>();
                break;
        }

        newSquareBehaviour.boardPos = new int[2] { i, j };
    }

    [ContextMenu("Delete All Squares")]
    public void DeleteSquares()
    {
        for (int i = squaresHolder.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(squaresHolder.transform.GetChild(i).gameObject);
        }
    }
}
