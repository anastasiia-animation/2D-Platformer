using System.Collections;
using UnityEngine;

public class Entety_VFX : MonoBehaviour
{
    private Material originalMat;
    private SpriteRenderer sr;

    [SerializeField] private Material OnDamageVFXMat;
    [SerializeField] private float OnDamageVFxDuration = .15f;
    private Coroutine onDamageVFXCo;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    public void PlayOnDamageVFX()
    {
        if(onDamageVFXCo != null)
            StopCoroutine(onDamageVFXCo);

        onDamageVFXCo = StartCoroutine(OnDamageVFXCo());
    }

    private IEnumerator OnDamageVFXCo()
    {
        sr.material = OnDamageVFXMat;

        yield return new WaitForSeconds(OnDamageVFxDuration);

        sr.material = originalMat;
    }
}
