using UnityEngine;


public class Letter : Item
{
    public char letter;
    public override void Collect()
    {
        GameController.instance.lettersCollected += 1;
        Destroy(gameObject);
        
    }
}
