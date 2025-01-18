using UnityEngine;
using UnityEditor;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private float startPos;
    [SerializeField] private float squaresDiff;

    [SerializeField] private SquaresHighlighter squaresHighlighter;
    [SerializeField] private GameObject squaresHolder;
    [SerializeField] private GameObject whitePiecesHolder;
    [SerializeField] private GameObject blackPiecesHolder;

    [SerializeField] private GameObject whiteCheckerPrefab;
    [SerializeField] private GameObject blackCheckerPrefab;
    [SerializeField] private GameObject whiteKingPrefab;
    [SerializeField] private GameObject blackKingPrefab;

    public void SetSquares()
    {
        Transform[] squares = squaresHolder.GetComponentsInChildren<Transform>();
        int squaresCounter = 1;

        for (int i = 0; i < 8; i++)
        {
            for (int j = i % 2; j < 8; j += 2)
            {
                SquareBehaviour newSquare = squares[squaresCounter].gameObject.AddComponent<SquareBehaviour>();
                newSquare.boardPos = new int[2] { i, j };
                if(i == 0)
                {
                    newSquare.isKingPromoteSquare = true;
                    newSquare.promoteForWhite = false;
                }
                else if (i == 7)
                {
                    newSquare.isKingPromoteSquare = true;
                    newSquare.promoteForWhite = true;
                }
                StaticData.squares[i, j] = newSquare;

                squaresCounter++;
            }
        }
    }

    public void GeneratePieces()
    {
        short[,] piecesPos = StaticData.piecesStartingPos;
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                GeneratePiece(piecesPos[i, j], i, j);
            }
        }
    }

    private void GeneratePiece(int pieceNumber, int i, int j)
    {
        if (pieceNumber == 0) return;

        GameObject newPiece;
        switch (pieceNumber)
        {
            case 1:
                newPiece = Instantiate(whiteCheckerPrefab, whitePiecesHolder.transform);
                newPiece.name = "WhiteChecker " + i + j;
                break;
            case 2:
                newPiece = Instantiate(blackCheckerPrefab, blackPiecesHolder.transform);
                newPiece.name = "BlackChecker " + i + j;
                break;
            case 3:
                newPiece = Instantiate(whiteKingPrefab, whitePiecesHolder.transform);
                newPiece.name = "WhiteKing " + i + j;
                break;
            case 4:
                newPiece = Instantiate(blackKingPrefab, blackPiecesHolder.transform);
                newPiece.name = "BlackKing " + i + j;
                break;
            default:
                newPiece = new GameObject();
                break;
        }

        Vector3 localPos = new Vector3(startPos + j * squaresDiff, 0, startPos + i * squaresDiff);
        newPiece.transform.SetLocalPositionAndRotation(localPos, newPiece.transform.rotation);

        PieceBehaviour newPieceScript = newPiece.GetComponent<PieceBehaviour>();
        SetSquareBehavoiur(newPieceScript, i, j);
        
    }

    private void SetSquareBehavoiur(PieceBehaviour pieceBehaviour, int i, int j )
    {
        pieceBehaviour.attachedSquare = StaticData.squares[i, j];
        pieceBehaviour.attachedSquare.isOccupied = true;
        pieceBehaviour.attachedSquare.attachedPiece = pieceBehaviour;
    }

    public void PromoteChecker(CheckerBehaviour checkerBehaviour, SquareBehaviour square)
    {
        GameObject piecesHolder = checkerBehaviour.isWhite ? whitePiecesHolder : blackPiecesHolder;
        GameObject kingPrefab = checkerBehaviour.isWhite ? whiteKingPrefab : blackKingPrefab;
        GameObject oldChecker = checkerBehaviour.gameObject;

        GameObject newKing = Instantiate(kingPrefab, oldChecker.transform.position, kingPrefab.transform.rotation, piecesHolder.transform);
        newKing.name = oldChecker.name.Replace("Checker", "King");

        KingBehaviour newKingBehaviour = newKing.GetComponent<KingBehaviour>();
        newKingBehaviour.attachedSquare = square;
        square.attachedPiece = newKingBehaviour;

        checkerBehaviour.isDestroyed = true;
        Destroy(oldChecker);

        if (newKingBehaviour.IsPossibleToCapture())
            StaticData.isObligatedToCapture = true;
    }
}
