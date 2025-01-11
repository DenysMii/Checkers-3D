using System;
using UnityEngine;
using UnityEngine.UI;

public class BoardSettings : MonoBehaviour
{
    const string BOARD_PREF_NAME = "BoardIndex";

    [SerializeField] private GameObject blackSquaresHolder;
    [SerializeField] private Renderer whiteSquaresRenderer;
    [SerializeField] private Renderer bordersRenderer;

    [SerializeField] private Dropdown boardDropdown;
    [SerializeField] private BoardVariant[] boardVariants;

    private void Start()
    {
        if (PlayerPrefs.HasKey(BOARD_PREF_NAME))
            PlayerPrefsSetVariant();
        else
            SetVariant(0);

        boardDropdown.onValueChanged.AddListener(OnVariantChanged);
    }

    private void OnVariantChanged(int index)
    {
        SetVariant(index);
    }

    private void PlayerPrefsSetVariant()
    {
        int index = PlayerPrefs.GetInt(BOARD_PREF_NAME);
        boardDropdown.value = index;
        SetVariant(index);
    }

    private void SetVariant(int index)
    {
        if (index < 0 || index >= boardVariants.Length) return;

        BoardVariant selectedVariant = boardVariants[index];

        whiteSquaresRenderer.material = selectedVariant.whiteSquareMaterial;
        bordersRenderer.material = selectedVariant.bordersMaterial;

        Renderer[] blackSquaresRenderers = blackSquaresHolder.GetComponentsInChildren<Renderer>();
        foreach (Renderer bsRenderer in blackSquaresRenderers)
            bsRenderer.material = selectedVariant.blackSquareMaterial;

        PlayerPrefs.SetInt(BOARD_PREF_NAME, index);
    }
}
