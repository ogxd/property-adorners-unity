public delegate void VoidDelegate();

public class TriggerOnChangeAttribute : PropertyAdornerAttribute {

    public readonly string onUpdate;

    public TriggerOnChangeAttribute(string onUpdate) {
        this.onUpdate = onUpdate;
    }
}