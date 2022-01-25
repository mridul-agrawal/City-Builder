using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Action OnBuildAreaHandler;
    private Action OnCancleActionHandler;
    private Action OnDemolishActionHandler;


    public Button buildResidentialAreaBtn;
    public Button cancleActionBtn;
    public GameObject cancleActionPanel;

    public GameObject buildingMenuPanel;
    public Button openBuildMenuBtn;
    public Button demolishBtn;

    // Start is called before the first frame update
    void Start()
    {
        cancleActionPanel.SetActive(false);
        buildingMenuPanel.SetActive(false);
        buildResidentialAreaBtn.onClick.AddListener(OnBuildAreaCallback);
        cancleActionBtn.onClick.AddListener(OnCancleActionCallback);
        cancleActionBtn.onClick.AddListener(OnCancleActionCallback);
        openBuildMenuBtn.onClick.AddListener(OnOpenBuildMenu);
        demolishBtn.onClick.AddListener(OnDemolishHandler);
    }


    private void OnOpenBuildMenu()
    {
        buildingMenuPanel.SetActive(true);
    }

    private void OnDemolishHandler()
    {
        OnDemolishActionHandler?.Invoke();
        cancleActionPanel.SetActive(true);
        buildingMenuPanel.SetActive(false);
    }

    private void OnBuildAreaCallback()
    {
        cancleActionPanel.SetActive(true);
        buildingMenuPanel.SetActive(false);
        OnBuildAreaHandler?.Invoke();
    }

    private void OnCancleActionCallback()
    {
        cancleActionPanel.SetActive(false);
        OnCancleActionHandler?.Invoke();
    }

    public void AddListenerOnBuildAreaEvent(Action listener)
    {
        OnBuildAreaHandler += listener;
    }

    public void RemoveListenerOnBuildAreaEvent(Action listener)
    {
        OnBuildAreaHandler -= listener;
    }
    public void AddListenerOnCancleActionEvent(Action listener)
    {
        OnCancleActionHandler += listener;
    }

    public void RemoveListenerOnCancleActionEvent(Action listener)
    {
        OnCancleActionHandler -= listener;
    }

    public void AddListenerOnDemolishActionEvent(Action listener)
    {
        OnDemolishActionHandler += listener;
    }

    public void RemoveListenerOnDemolishActionEvent(Action listener)
    {
        OnDemolishActionHandler -= listener;
    }

}