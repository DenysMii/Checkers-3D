using System.Collections;
using UnityEngine;

public class DestroyFracture : Fracture
{
    [Header("Destroy")]
    [SerializeField] private float destroyPieceDelay;

    private void Start()
    {
        callbackOptions.onCompleted.AddListener(DestroyPiece);
    }

    private void DestroyPiece()
    {
        gameObject.SetActive(true);
        StartCoroutine(DestroyPieceCoroutine());
    }

    private IEnumerator DestroyPieceCoroutine()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;           
        gameObject.GetComponent<MeshCollider>().enabled = false;           
        yield return new WaitForSeconds(destroyPieceDelay);
        Destroy(gameObject);
        Destroy(fragmentRoot);
    }
}
