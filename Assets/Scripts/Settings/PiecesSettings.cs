using UnityEngine;
using UnityEngine.UI;

public class PiecesSettings : MonoBehaviour
{
    const string PIECES_PREF_NAME = "PiecesIndex";

    [SerializeField] private GameObject whitePiecesHolder;
    [SerializeField] private GameObject blackPiecesHolder;

    [SerializeField] private Dropdown piecesDropdown;
    [SerializeField] private PiecesVariant[] piecesVariants;

    private void Start()
    {
        if (PlayerPrefs.HasKey(PIECES_PREF_NAME))
            PlayerPrefsSetVariant();
        else
            SetVariant(0);

        piecesDropdown.onValueChanged.AddListener(OnVariantChanged);
    }

    private void OnVariantChanged(int index)
    {
        SetVariant(index);
    }

    private void PlayerPrefsSetVariant()
    {
        int index = PlayerPrefs.GetInt(PIECES_PREF_NAME);
        piecesDropdown.value = index;
        SetVariant(index);
    }

    private void SetVariant(int index)
    {
        if (index < 0 || index >= piecesVariants.Length) return;

        PiecesVariant selectedVariant = piecesVariants[index];

        SetMaterialForPieces(selectedVariant.whitePieceMaterial, whitePiecesHolder);
        SetMaterialForPieces(selectedVariant.blackPieceMaterial, blackPiecesHolder);

        PlayerPrefs.SetInt(PIECES_PREF_NAME, index);
    }

    private void SetMaterialForPieces(Material material, GameObject piecesHolder)
    {
        Renderer[] piecesRenderers = piecesHolder.GetComponentsInChildren<Renderer>();
        foreach (Renderer pieceRenderer in piecesRenderers)
        {
            if (!pieceRenderer.gameObject.name.Equals("Spheres") && !pieceRenderer.gameObject.name.Equals("Crown"))
                pieceRenderer.material = material;
        }
    }
}
