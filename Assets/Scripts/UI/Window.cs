using System;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    public event Action Started;

    private void Awake() => _playButton.onClick.AddListener(StartGame);

    private void OnDestroy() => _playButton.onClick.RemoveAllListeners();

    private void StartGame()
    {
        Started?.Invoke();
        gameObject.SetActive(false);
    }
}