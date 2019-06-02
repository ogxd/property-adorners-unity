public class NameAttribute : PropertyAdornerAttribute {

    public readonly string name;

    public NameAttribute(string name) {
        this.name = name;
    }
}