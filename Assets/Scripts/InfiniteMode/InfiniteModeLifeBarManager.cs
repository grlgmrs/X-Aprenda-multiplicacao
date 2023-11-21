using System;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteModeLifeBarManager
{
  public int CurrentHealth { get; private set; }
  private readonly GameObject LifeBar;
  private readonly GameObject HealthPointPrefab;
  private readonly int MaxHealth;
  private readonly List<GameObject> InstantiatedHealthPoints = new();

  public InfiniteModeLifeBarManager(GameObject lifeBar, GameObject healthPointPrefab)
  {
    LifeBar = lifeBar;
    HealthPointPrefab = healthPointPrefab;
    MaxHealth = Constants.InfiniteModeMaxHealthPointIntances;
  }

  public void ResetCurrentHealth()
  {
    CurrentHealth = MaxHealth;

    for (var i = 0; i < MaxHealth; i++)
      InstantiateHealthPoint(i);
  }

  public void RemoveHealthPoints(int damage)
  {
    for (var i = 0; i < damage; i++)
    {
      var healthPoint = InstantiatedHealthPoints[^1];

      MonoBehaviour.Destroy(healthPoint);
      InstantiatedHealthPoints.Remove(healthPoint);

      if (--CurrentHealth == 0) break;
    }
  }

  public void AddHealthPoints(int health)
  {
    if (CurrentHealth + health > MaxHealth) return;

    for (var i = CurrentHealth; i < CurrentHealth + health; i++)
      InstantiateHealthPoint(i);

    CurrentHealth += health;
  }

  private void InstantiateHealthPoint(int index)
  {
    var offset = new Vector3(-170.5f, 0);
    GameObject healthPointInstance = MonoBehaviour.Instantiate(HealthPointPrefab, LifeBar.transform);
    healthPointInstance.transform.localPosition = offset + Helpers.GetHealthPointCoordinates(index);

    InstantiatedHealthPoints.Add(healthPointInstance);
  }
}
