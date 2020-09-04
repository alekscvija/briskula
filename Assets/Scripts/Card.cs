using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCardState{
  deck,
  playerset,
  playerdeck,
  AIset,
  AIdeck,
  playedbyPlayer,
  playedbyAI
}

public class Card : MonoBehaviour
{
  public string Suit;
  public int Value;
  public int pointsVal;
  public eCardState state=eCardState.deck;

//  public int points; count points
  public GameObject back;//back of card

  public bool faceUp{
    get{ return(!back.activeSelf);}
    set{
      back.SetActive(!value);
    }
  }

  /*public Card(string Suit,int Value,int pointsVal){
  this.Suit=Suit;
  this.Value=Value;
this.pointsVal=pointsVal;
}*/
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    virtual public void OnMouseUpAsButton(){
      //call the cardclicked method on the briscola singleton
      if (Briscola.B.playingSpace.Count < 4)
      {
        Briscola.B.CardClicked(this);
      }

      if (Briscola.B.ai.compSet.Count != 0)
      {
        Briscola.B.AIplays(Briscola.B.ai.compSet, Briscola.B.playingSpace);
      }




    }


}
//virtual methods can be overwritten by subclass methods with the same name
