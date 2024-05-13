using System;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDashboardC : MonoBehaviour
{
    [SerializeField] private EnergySystemC energySystem;
    [SerializeField] private Image fillBar;
    private void Start()
    {
        energySystem.OnEnergyChanged += useEnergy;
    }

    private void useEnergy(float energy)
    {
        fillBar.fillAmount = energySystem.Fuel / energySystem.MaxFuel;
    }
}