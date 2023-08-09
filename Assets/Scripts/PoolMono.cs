using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    public T prefab { get; private set; }
    public bool autoExpand { get; set;}

    public Transform container { get; private set; }

    public List<T> pool;

    public PoolMono(T prefab, int capacity, Transform container = null)
    {
        this.prefab = prefab;
        this.container = container;

        CreatePool(capacity);
    }

    private void CreatePool(int capacity)
    {
        pool = new List<T>();

        for (int i = 0; i < capacity; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(prefab, container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (mono.gameObject.activeInHierarchy == false)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }

        if (autoExpand)
        {
            return CreateObject(true);
        }
        else
        {
            throw new Exception($"There is no free elements in pool of type {typeof(T)}");
        }
    }

    public List<T> GetAllElements()
    {
        return pool;
    }
}
