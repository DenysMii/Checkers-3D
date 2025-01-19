using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{
    private Animation animation;

    private void Start()
    {
        animation = GetComponent<Animation>();
    }

    private void OnMouseDown()
    {
        animation.Play();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
}
