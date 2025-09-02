using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public sealed class Counter : MonoBehaviour
{
    [SerializeField] TMP_Text Text;

    int Count { get; set; }

    void OnValidate()
    {
        Text = GetComponent<TMP_Text>();
        Text.SetText(Count.ToString());
    }

    public void Increment()
    {
        Count++;
        Text.SetText("Count: " + Count.ToString());
    }
}
