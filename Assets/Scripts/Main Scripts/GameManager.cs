using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject instructions;

    [Header("Main Scripts")]
    [SerializeField] private BoardGenerator boardGenerator;
    [SerializeField] private PlayerPrefsLoader playerPrefsLoader;
    [SerializeField] private SquaresHighlighter squaresHighlighter;
    [SerializeField] private TurnsManager turnsManager;
    [SerializeField] private AnimationsManager animationsManager;

    private void Start()
    {
        StaticData.isWhiteTurn = true;

        StaticData.boardGenerator = boardGenerator;
        StaticData.squaresHighlighter = squaresHighlighter;
        StaticData.turnsManager = turnsManager;
        StaticData.animationsManager = animationsManager;

        if(!PlayerPrefs.HasKey("Need Instructions") || PlayerPrefs.GetInt("Need Instructions") != 0)
        {
            instructions.SetActive(true);
            PlayerPrefs.SetInt("Need Instructions", 0);
        }

        boardGenerator.SetSquares();
        boardGenerator.GeneratePieces();
        playerPrefsLoader.LoadPlayerPrefs();
        turnsManager.CheckForCaptures();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
