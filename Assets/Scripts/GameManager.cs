using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardGenerator boardGenerator;
    [SerializeField] private PlayerPrefsLoader playerPrefsLoader;
    [SerializeField] private SquaresHighlighter squaresHighlighter;
    [SerializeField] private TurnsManager turnsManager;

    private void Start()
    {
        boardGenerator.SetSquares();
        boardGenerator.GeneratePieces();
        playerPrefsLoader.LoadPlayerPrefs();
        

        StaticData.boardGenerator = boardGenerator;
        StaticData.squaresHighlighter = squaresHighlighter;
        StaticData.turnsManager = turnsManager;
        
        turnsManager.CheckForCaptures();
    }



    //public void CheckTime()
    //{
    //    var timer = Time.realtimeSinceStartup;
    //    turnsManager.CheckForCaptures();
    //    print("Default " + (Time.realtimeSinceStartup - timer).ToString("f6"));

    //    timer = Time.realtimeSinceStartup;
    //    turnsManager.CheckForCapturesHolders();
    //    print("Holders " + (Time.realtimeSinceStartup - timer).ToString("f6"));
    //}
}
