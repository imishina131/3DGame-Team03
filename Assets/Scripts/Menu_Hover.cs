using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverImage : MonoBehaviour
{
    public Image hoverImage;  // The image you want to appear on hover
    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        hoverImage.enabled = false;  // Hide the hover image initially
    }

    public void OnHoverEnter()
    {
        hoverImage.enabled = true;  // Show the hover image
    }

    public void OnHoverExit()
    {
        hoverImage.enabled = false;  // Hide the hover image
    }
}