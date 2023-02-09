using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPool : MonoBehaviour
{
    private List<GameObject> _poolWeapon = new List<GameObject>();
    private List<GameObject> _poolBonuses = new List<GameObject>();
    private int _poolMultiplier = 2;

    public void InitializePool(Fallings[] _fallingsWeapons, Fallings[] _fallingsBonuses)
    {
        for (int i = 0; i < _poolMultiplier; i++)
        {
            AddInList(_fallingsWeapons, _poolWeapon);
            AddInList(_fallingsBonuses, _poolBonuses);
        }
    }

    private void AddInList(Fallings[] _fallings, List<GameObject> list)
    {
        foreach (Fallings falling in _fallings)
        {
            GameObject spawned = Instantiate(falling.gameObject, transform);
            spawned.SetActive(false);
            list.Add(spawned);
        }
    }

    public GameObject GetRandomWeapon()
    {
        return WithdrawalFromList(_poolWeapon);
    }
    public GameObject GetRandomBonus()
    {
        return WithdrawalFromList(_poolBonuses);
    }

    private GameObject WithdrawalFromList(List<GameObject> list)
    {
        int index = Random.Range(0, list.Capacity);
        GameObject gameObject = list[index];
        gameObject.SetActive(true);
        list.RemoveAt(index);
        return gameObject;
    }

    public void ReturnWeapon(Fallings weapon)
    {
        weapon.gameObject.SetActive(false);
        _poolWeapon.Add(weapon.gameObject);
    }
    
    public void ReturnBonus(Fallings bonus)
    {
        bonus.gameObject.SetActive(false);
        _poolBonuses.Add(bonus.gameObject);
    }
}