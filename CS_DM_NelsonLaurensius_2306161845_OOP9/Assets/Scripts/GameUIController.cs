using UnityEngine;
using UnityEngine.UIElements;

public class GameUIController : MonoBehaviour
{
    private Label healthLabel;
    private Label pointsLabel;
    private Label waveLabel;
    private Label enemiesLeftLabel;

    public int health = 100;
    public int points = 0;
    public int wave = 1;
    public int enemiesLeft = 10;

    private Player player;
    private HealthComponent playerHealthComponent;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        healthLabel = root.Q<Label>("Health");
        pointsLabel = root.Q<Label>("Points");
        waveLabel = root.Q<Label>("Wave");
        enemiesLeftLabel = root.Q<Label>("EnemiesLeft");

        player = Player.Instance;
        if (player != null)
        {
            playerHealthComponent = player.GetHealthComponent();
        }
        UpdateUI();
    }

    private void Update()
    {
        if (playerHealthComponent != null)
        {
            SetHealth(playerHealthComponent.GetHealth());
        }
    }

    private void UpdateUI()
    {
        if (healthLabel != null) healthLabel.text = $"Health: {health}";
        if (pointsLabel != null) pointsLabel.text = $"Points: {points}";
        if (waveLabel != null) waveLabel.text = $"Wave: {wave}";
        if (enemiesLeftLabel != null) enemiesLeftLabel.text = $"Enemies Left: {enemiesLeft}";
    }

    public void SetHealth(int newHealth)
    {
        healthLabel.text = $"Health: {newHealth}";
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        UpdateUI();
    }

    public void SetWave(int newWave)
    {
        wave = newWave;
        UpdateUI();
    }

    public void SetEnemiesLeft(int newEnemiesLeft)
    {
        enemiesLeft = newEnemiesLeft;
        UpdateUI();
    }
}
