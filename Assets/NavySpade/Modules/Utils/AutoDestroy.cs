using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float _delay = 0.4f;

    private void Start()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_delay);
        Destroy(gameObject);
    }
}
