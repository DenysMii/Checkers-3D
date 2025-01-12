using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PiecesGenerator piecesGenerator;
    [SerializeField] private PlayerPrefsLoader playerPrefsLoader;
    [SerializeField] private string debugSceneName;

    private void Start()
    {
        if(piecesGenerator != null)
            piecesGenerator.GeneratePiecesFromStaticData();

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
