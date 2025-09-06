using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class SceneSwitcher : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] SceneAsset SceneToLoad;
    void OnValidate()
    {
        SceneName = SceneToLoad.name;
    }
#endif
    [SerializeField] string SceneName;

    void Awake()
    {
        Assert.IsNotNull(SceneName);
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => SceneManager.LoadScene(SceneName));
    }

    void OnDestroy()
    {
        var button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }
}
