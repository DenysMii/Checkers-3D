using UnityEngine;
using UnityEngine.UI;

public class CustomShapeButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }
}
