using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    public bool isOpen = false;

    [Header("Sprites")]
    public Sprite openSprite;   // ← assigne ton sprite ouvert dans l'Inspector
    public Sprite closedSprite; // ← assigne ton sprite fermé dans l'Inspector

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private bool _previousState;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        if (_spriteRenderer == null) Debug.LogError("Aucun SpriteRenderer trouvé !");
        if (_collider == null) Debug.LogError("Aucun Collider2D trouvé !");
        if (openSprite == null) Debug.LogError("Aucun sprite 'ouvert' assigné !");
        if (closedSprite == null) Debug.LogError("Aucun sprite 'fermé' assigné !");

        ApplyState();
        _previousState = isOpen;
    }

    void Update()
    {
        if (isOpen != _previousState)
        {
            ApplyState();
            _previousState = isOpen;
        }
    }

    void ApplyState()
    {
        _spriteRenderer.sprite = isOpen ? openSprite : closedSprite;
        _collider.enabled = !isOpen; // collision active = porte fermée
    }
}