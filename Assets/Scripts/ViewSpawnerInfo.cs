using TMPro;
using UnityEngine;

public class ViewSpawnerInfo : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TextMeshProUGUI _infoAmountActive;
    [SerializeField] private TextMeshProUGUI _infoAmountCreated;
    [SerializeField] private TextMeshProUGUI _infoAmountSpawned;

    private void OnEnable()
    {
        _spawner.InfoAboutAmountChanged += SetInfo;
    }

    private void OnDisable()
    {
        _spawner.InfoAboutAmountChanged -= SetInfo;
    }

    private void SetInfo(InfoAboutAmountObjects info)
    {
        _infoAmountActive.text = info.AmountActive.ToString();
        _infoAmountCreated.text = info.AmountCreated.ToString();
        _infoAmountSpawned.text = info.AmountSpawned.ToString();
    }
}