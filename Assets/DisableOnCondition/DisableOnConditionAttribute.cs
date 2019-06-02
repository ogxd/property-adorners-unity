public delegate bool BoolDelegate();

public class DisableOnConditionAttributeAttribute : PropertyAdornerAttribute {

    public readonly string condition;

    public DisableOnConditionAttributeAttribute(string condition) {
        this.condition = condition;
    }
}