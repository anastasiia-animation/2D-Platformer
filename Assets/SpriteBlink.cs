using Unity.VisualScripting;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    [SerializeField]
    private float blinkDecaySpeed = 1f;

    private SpriteRenderer[] _spriteRenderers;
    private MaterialPropertyBlock _propertyBlock;
    private float _blinkFactor;

    private void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _propertyBlock = new MaterialPropertyBlock();

    }

    private void Update()
    {
        if (_blinkFactor <= 0f)
        {
            return;
        }

        _blinkFactor -= Mathf.Lerp(_blinkFactor, 0f, Time.deltaTime * blinkDecaySpeed);
        if (_blinkFactor < 0.01f)
        {
            _blinkFactor = 0f;
        }

        ApplyBlinkFactor();
    }
    public void Blink()
    {
        _blinkFactor = 1f;
        ApplyBlinkFactor();
    }


    private void ApplyBlinkFactor()
    {
        foreach (var renderer in _spriteRenderers)
        {
            renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetFloat("_BlinkFactor", _blinkFactor);
            renderer.SetPropertyBlock(_propertyBlock);
        }
           
    }
}
