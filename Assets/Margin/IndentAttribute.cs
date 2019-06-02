using UnityEngine;

public class IndentAttribute : PropertyAdornerAttribute {

    public readonly int indent;

    public IndentAttribute(int indent) {
        this.indent = indent;
    }
}