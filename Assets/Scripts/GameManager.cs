using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardGenerator boardGenerator;
    [SerializeField] private PlayerPrefsLoader playerPrefsLoader;
    [SerializeField] private string debugSceneName;

    private void Start()
    {
        if(boardGenerator != null)
        {
            boardGenerator.SetSquareObjects();
            boardGenerator.GeneratePiecesFromStaticData();

        }

        if(playerPrefsLoader != null)
            playerPrefsLoader.LoadPlayerPrefs();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            SceneManager.LoadScene(debugSceneName);
        }
    }
}
