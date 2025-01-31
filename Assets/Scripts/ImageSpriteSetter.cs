using UnityEngine;
using UnityEngine.UI;

public class ImageSpriteSetter
{
    private Image image;

    public ImageSpriteSetter(Image image)
    {
        this.image = image;
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
