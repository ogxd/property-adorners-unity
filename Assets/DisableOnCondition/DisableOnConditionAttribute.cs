public delegate bool BoolDelegate();

public class DisableOnConditionAttribute : PropertyAdornerAttribute {

    public readonly string condition;

    public DisableOnConditionAttribute(string condition) {
        this.condition = condition;
    }
}