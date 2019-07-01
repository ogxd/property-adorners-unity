using UnityEngine;

public enum ComputationMethod {
    Bruteforce,
    Fastest,
    Manual
}

public class SampleScript : MonoBehaviour {

    [Readonly]
    public int value1 = 125;

    [Readonly]
    public float value2 = 138452.2f;

    public bool useAutomaticParameters = true;

    [DisableOnConditionAttribute("test")]
    [TriggerOnChange("compute")]
    [Indent(1)]
    public ComputationMethod method;

    [Color(1f, 1f, 0f)]
    [TriggerOnChange("compute")]
    [Indent(1)]
    [DisableOnConditionAttribute("test")]
    [Range(1, 50)] // Property adorners are compatible with custom property drawers. Adorners must be declared above.
    public int count = 3;

    [Space(10)]

    [Name("Color (HDR)")]
    public Color colorHDR;

    private void Start() {
        
    }

    public void compute() {
        Debug.Log("compute");
    }

    public bool test() {
        return useAutomaticParameters;
    }
}