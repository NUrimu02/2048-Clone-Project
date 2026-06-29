using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    public TileState state { get; private set; }
    public TileCell cell { get; private set; }
    public int number { get; private set; }
    public bool locked { get; set; }

    private Image _background;
    private TMP_Text _text;
    private Tween moveTween;

    private void Awake()
    {
        _background = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();
    }

    public void SetState(TileState state, int number)
    {
        this.state = state;
        this.number = number;

        _background.color = state.backGroundColor;
        _text.color = state.textColor;
        _text.text = number.ToString();
    }


    public void Spawn(TileCell cell)
    {
        if(this.cell != null)
            this.cell.tile = null;
        
        this.cell = cell;
        this.cell.tile = this;

        transform.position = cell.transform.position;
    }

    public void MoveTo(TileCell cell)
    {
        if(this.cell != null)
            this.cell.tile = null;
        
        this.cell = cell;
        this.cell.tile = this;

        Animate(cell.transform.position, false);
    }

    public void Merge(TileCell cell)
    {
        if (this.cell != null)
            this.cell.tile = null;

        this.cell = null; // <-- Burada this.cell'i null yapıyorsun.
        cell.tile.locked = true;

        Animate(cell.transform.position, true);
    }
    
    private void Animate(Vector3 to, bool merging) 
    { 
        moveTween?.Kill(); 
        moveTween = transform.DOMove(to, 0.1f) 
            .SetEase(Ease.Linear); 
            
        if (!merging) 
            return; 
            
        moveTween.OnComplete(() => 
        { 
            Destroy(gameObject); 
        }); 
    }
}
