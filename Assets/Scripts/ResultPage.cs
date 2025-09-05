using NUnit.Framework;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class ResultPage : MonoBehaviour
{
    [SerializeField] TMP_Text description;

    void Awake()
    {
        Assert.IsNotNull(description);
    }

    public void Hide()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void Show(int items)
    {
        if (items < 1)
        {
            Debug.LogError("Items must be greater than 0.");
        }

        GetComponent<Canvas>().enabled = true;
        description.text = $"Congratulations! You earned {items} tickets for recyling!."; //items.ToString();
    }
}
