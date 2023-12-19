using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, IProduct
{
    public Image ElementImage;
    public CardStats _stats;
    public int handIndex;
    public GameObject spell;

    private Animator _animator;
    private bool _useCard = false;
    private PlayerManager _playerRef;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerRef = FindObjectOfType<PlayerManager>();
        ElementImage = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_useCard)
        {
            transform.position += new Vector3(0, 0.08f, 0);
            if (transform.position.y > -1)
            {
                _useCard = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        if(_playerRef.CanPlay)
        {
            if (!_playerRef._spellsQueue.FullQueue())
            {
                if (_playerRef.CurrentMana >= _stats.Cost)
                {
                    _playerRef.PlayCard(this);
                    GameManager.Instance.UpdateMana(_stats.Cost);
                    _playerRef.CurrentMana -= _stats.Cost;
                    _animator.SetTrigger("UseCard");
                    _useCard = true;
                }
                else
                    Debug.Log("Not enough Mana");
            }
            else
                Debug.Log("Only 3 spells can be thrown in 1 turn"); 
        }
        else
            Debug.Log("Wait For Your Turn To Play");
    }

    public IProduct Clone(int index)
    {
        return Instantiate(GameManager.Instance.availableCards[index]);
    }
}
