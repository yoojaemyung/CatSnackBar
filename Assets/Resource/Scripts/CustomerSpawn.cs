using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    public GameObject _customerPrefab;


    public delegate void DeleteCustomer();
    public static event DeleteCustomer OnDeleteCustomer;

    void Start()
    {
        OnDeleteCustomer += Spawn;
        //Spawn();
    }


    public void Spawn()
    {
        StartCoroutine(SpawnWithDelay(0.5f)); // 0.5초 딜레이 후 실제 스폰 작업 수행
    }

    private IEnumerator SpawnWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 지정된 시간만큼 대기
        Instantiate(_customerPrefab, this.transform);
    }

    public void TriggerDeleteEvent()
    {
        OnDeleteCustomer?.Invoke();
    }
   

}
