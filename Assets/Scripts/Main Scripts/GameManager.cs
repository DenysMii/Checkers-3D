using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject instructions;

    [Header("Generators")]
    [SerializeField] private SquaresGenerator squaresGenerator;
    [SerializeField] private PiecesGenerator piecesGenerator;

    [Header("Main Scripts")]
    [SerializeField] private PlayerPrefsLoader playerPrefsLoader;
    [SerializeField] private SquaresHighlighter squaresHighlighter;
    [SerializeField] private TurnsManager turnsManager;
    [SerializeField] private AnimationsManager animationsManager;

    private void Start()
    {
        StaticData.isWhiteTurn = true;

        StaticData.piecesGenerator = piecesGenerator;
        StaticData.squaresHighlighter = squaresHighlighter;
        StaticData.turnsManager = turnsManager;
        StaticData.animationsManager = animationsManager;

        if(!PlayerPrefs.HasKey("Need Instructions") || PlayerPrefs.GetInt("Need Instructions") != 0)
        {
            instructions.SetActive(true);
            PlayerPrefs.SetInt("Need Instructions", 0);
        }

        squaresGenerator.LoadSquaresBehaviours();
        playerPrefsLoader.LoadPlayerPrefs();
        turnsManager.CheckForCaptures();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
