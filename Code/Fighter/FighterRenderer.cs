using DG.Tweening;
using UnityEngine;

public class FighterRenderer : MonoBehaviour
{
    [SerializeField] private Fighter _fighter = null;
    [SerializeField] private FighterHealth _health = null;
    [SerializeField] private Material _startMaterial = null;
    [SerializeField] private Renderer _renderer = null;

    [SerializeField] private float _defaultSeconds = 1f;

    [SerializeField] private Color _damageColor = Color.white;
    [SerializeField] private float _damageSeconds = 1f;

    private Color _defaultColor;
    private Sequence _damaging;
    private Material _material;

    private void Awake()
    {
        _defaultColor = _startMaterial.color;
        _material = new Material(_startMaterial);

        _fighter.Playing += OnPlaying;
        _health.Damaged += OnDamaged;
    }

    private void OnDestroy()
    {
        _fighter.Playing -= OnPlaying;
        _health.Damaged -= OnDamaged;
    }

    private void OnPlaying()
    {
        _renderer.material = _material;
    }

    private void OnDamaged()
    {
        _damaging?.Kill();

        _damaging = DOTween.Sequence()
            .Append(_material
                .DOColor(_damageColor, _damageSeconds * 2))
            .Append(_material
                .DOColor(_defaultColor, _defaultSeconds * 2));
    }
}
