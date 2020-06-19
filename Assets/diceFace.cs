using UnityEngine;

public class DiceFace{
    public int Value;
    public Sprite sprite;

    public DiceFace(int val, Sprite spr){
        this.sprite = spr;
        this.Value = val;
    }

}