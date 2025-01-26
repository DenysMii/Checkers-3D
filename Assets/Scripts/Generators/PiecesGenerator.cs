using UnityEngine;
using UnityEditor;

public class PiecesGenerator : MonoBehaviour
{
    [Header("Pieces Holders")]
    [SerializeField] private GameObject whitePiecesHolder;
    [SerializeField] private GameObject blackPiecesHolder;

    [Header("Prefabs")]
    [SerializeField] private GameObject whiteCheckerPrefab;
    [SerializeField] private GameObject blackCheckerPrefab;
    [SerializeField] private GameObject whiteKingPrefab;
    [SerializeField] private GameObject blackKingPrefab;

    [ContextMenu("Generate Pieces")]
    public void GeneratePieces()
    {
        short[,] piecesPos = StaticData.piecesStartingPos;
        for (int i = 0; i < 8; i++)
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

        GameObject newPiece = null;
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
        }

        Vector3 localPos = new Vector3(StaticData.startPos + j * StaticData.squaresDiff, 0, StaticData.startPos + i * StaticData.squaresDiff);
        newPiece.transform.SetLocalPositionAndRotation(localPos, newPiece.transform.rotation);

        PieceBehaviour newPieceBehaviour = newPiece.GetComponent<PieceBehaviour>();
        AttachPieceSquare(newPiece); 
    }

    private void AttachPieceSquare(GameObject piece)
    {
        Vector3 piecePos = piece.transform.position;
        Vector3 rayOrigin = new(piecePos.x, piecePos.y + 1, piecePos.z);

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hitInfo, 1))
        {
            GameObject hitSquare = hitInfo.collider.gameObject;

            SquareBehaviour squareBehaviour = hitSquare.GetComponent<SquareBehaviour>();
            PieceBehaviour pieceBehaviour = piece.GetComponent<PieceBehaviour>();

            pieceBehaviour.attachedSquare = squareBehaviour;
            squareBehaviour.attachedPiece = pieceBehaviour;
            squareBehaviour.isOccupied = true;
        }
    }

    [ContextMenu("Delete All Pieces")]
    public void DeleteAllPieces()
    {
        DeletePieces(whitePiecesHolder);
        DeletePieces(blackPiecesHolder);
    }

    private void DeletePieces(GameObject holder)
    {
        for (int i = holder.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(holder.transform.GetChild(i).gameObject);
        }
    }

    public KingBehaviour PromoteChecker(CheckerBehaviour checkerBehaviour)
    {
        GameObject piecesHolder = checkerBehaviour.isWhite ? whitePiecesHolder : blackPiecesHolder;
        GameObject kingPrefab = checkerBehaviour.isWhite ? whiteKingPrefab : blackKingPrefab;

        GameObject oldChecker = checkerBehaviour.gameObject;
        Renderer checkerRenderer = oldChecker.GetComponent<Renderer>();

        GameObject newKing = Instantiate(kingPrefab, oldChecker.transform.position, kingPrefab.transform.rotation, piecesHolder.transform);
        newKing.name = oldChecker.name.Replace("Checker", "King");
        newKing.GetComponent<Renderer>().material = checkerRenderer.material;

        KingBehaviour newKingBehaviour = newKing.GetComponent<KingBehaviour>();
        newKingBehaviour.attachedSquare = checkerBehaviour.attachedSquare;
        newKingBehaviour.attachedSquare.attachedPiece = newKingBehaviour;

        oldChecker.tag = "Destroyed Piece";
        Destroy(oldChecker);

        if (newKingBehaviour.IsPossibleToCapture())
            StaticData.isObligatedToCapture = true;

        return newKingBehaviour;
    }
}
