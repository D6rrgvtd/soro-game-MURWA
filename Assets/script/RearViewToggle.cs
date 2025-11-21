using UnityEngine;
using UnityEngine.UI;

public class RearViewToggle : MonoBehaviour
{
    [Header("Œã•ûƒJƒƒ‰‰f‘œUI")]
    public RawImage rearViewDisplay; // © RenderTexture‚ğ•\¦‚µ‚Ä‚¢‚éRawImage

    private bool isVisible = true;

    void Start()
    {
        if (rearViewDisplay != null)
            rearViewDisplay.enabled = isVisible;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isVisible = !isVisible;
            if (rearViewDisplay != null)
                rearViewDisplay.enabled = isVisible;
        }
    }
}

