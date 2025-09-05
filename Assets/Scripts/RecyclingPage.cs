using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RecyclingPage : MonoBehaviour
{
    [SerializeField] Canvas instructions;
    [SerializeField] Button startButton;

    [SerializeField] Canvas scanner;

    [SerializeField] ResultPage resultPage;

    void Start()
    {
        instructions.enabled = true;
        startButton.onClick.AddListener(() =>
        {
            instructions.enabled = false;
            scanner.enabled = true;
            StopAllCoroutines();
            StartCoroutine(StartScanning());
        });

        scanner.enabled = false;
        resultPage.Hide();
    }

    IEnumerator StartScanning()
    {
        scanner.enabled = true;
        yield return new WaitForSeconds(
            Random.Range(2f, 4f)
        );
        int tickets = Random.Range(1, 3);
        InventoryService.Instance.AddTickets(tickets);
        scanner.enabled = false;
        resultPage.Show(tickets);
    }

    void OnDestroy()
    {
        startButton.onClick.RemoveAllListeners();
    }
}
