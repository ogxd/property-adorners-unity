using UnityEngine;

public class ColorAttribute : PropertyAdornerAttribute {

    public readonly Color color;

    public ColorAttribute(float r, float g, float b) {
        this.color = new Color(r, g, b);
    }
}