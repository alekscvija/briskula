using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public List<Card> cardArr=new List<Card>();
   int points;

   public List<Card> pointDeck=new List<Card>();
   public bool PWinLastRnd= false;
   private bool tooklast=false;
   public bool tookLast{
     get{return (tooklast);}
     set{tooklast=value;}
   }
   public Vector3[] playerSlot=new Vector3[4];

    // Start is called before the first frame update
    void Start()
    {
    //  CardLayout(cardArr);


    }

    // Update is called once per frame
    void Update()
    {
    //  if(AI.cardArr[4]==null){}

    }

    public void CardLayout(List<Card> playerCards){
      cardArr.Add(playerCards[0]);
      cardArr.Add(playerCards[2]);
      cardArr.Add(playerCards[4]);
      cardArr.Add(playerCards[6]);

    /*  playerCards.RemoveAt(0);
      playerCards.RemoveAt(2);
      playerCards.RemoveAt(4);
      playerCards.RemoveAt(6);
*/
    
      for(int i=0;i<4;i++){
      cardArr[i].faceUp=true;
      cardArr[i].state=eCardState.playerset;
      iTween.MoveTo(cardArr[i].gameObject, cardArr[i].transform.position+=new Vector3(i*6+20,10,-15), 1);

      //cardArr[i].transform.position+=new Vector3(i*6+20,10,-15);
      playerSlot[i]=cardArr[i].transform.position;

    }
}

    public void MakeMove(Card card)
    {
      Briscola.B.CardClicked(card);
    }
   /* void MoveToPile(List<Card> card){
      for(int k=0;k<card.Count;k++){
        pointDeck.Add(card[k]);
        pointDeck[k].state=eCardState.playerdeck;
        pointDeck[k].transform.position=new Vector3(34,10,-10);
        pointDeck[k].transform.Rotate(90.0f,0.0f,0.0f,Space.World);
      }
      card.RemoveAt(0);
      card.RemoveAt(1);
      card.RemoveAt(2);
      card.RemoveAt(3);




    }*/
}
