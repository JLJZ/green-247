using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] SceneAsset SceneToLoad;

    void Awake()
    {
        Assert.IsNotNull(SceneToLoad);
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => SceneManager.LoadScene(SceneToLoad.name));
    }

    void OnDestroy()
    {
        var button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }
}
