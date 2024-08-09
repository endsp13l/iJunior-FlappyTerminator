using UnityEngine;

[RequireComponent(typeof(Game))]
public class GameView : MonoBehaviour
{
    [SerializeField] private Window _launchScreen;
    [SerializeField] private Window _endScreen;
    [SerializeField] private PlayerView _playerView;

    private Game _game => GetComponent<Game>();
    
    private void OnEnable()
    {
        _game.Launched += Launch;
        _game.Ended += End;
        
        _launchScreen.Started += _game.Play;
        _endScreen.Started += _game.Play;
    }
    
    private void OnDisable()
    {
        _game.Launched -= Launch;
        _game.Ended -= End;
        
        _launchScreen.Started -= _game.Play;
        _endScreen.Started -= _game.Play;
    }

    public void Launch()
    {
        _launchScreen.gameObject.SetActive(true);
        _playerView.ShowFlightAnimation();
    }

    public void End() => _endScreen.gameObject.SetActive(true);
}