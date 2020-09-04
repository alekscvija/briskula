using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
  public List<Card> compSet=new List<Card>();
  int points;
  public List<Card> pointDeck1=new List<Card>();
  public bool AIWinLastRnd=false;
  private bool tooklast;
  public bool tookLastAI{
    get{return (tooklast);}
    set{tooklast=value;}
  }
  public Vector3[] AISlot=new Vector3[4];



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AICardLayout(List<Card> playerCards){
      compSet.Add(playerCards[1]);
      compSet.Add(playerCards[3]);
      compSet.Add(playerCards[5]);
      compSet.Add(playerCards[7]);

      playerCards.RemoveRange(0,8);
     





      for(int i=0;i<4;i++){
        compSet[i].state=eCardState.AIset;
        iTween.MoveTo(compSet[i].gameObject, compSet[i].transform.position += new Vector3(i * 6 + 20, 10, 15), 1);
      //compSet[i].transform.position+=new Vector3(i*6+20,10,15);
      AISlot[i]=compSet[i].transform.position;
      


    }
}
void MoveToPileAI(List<Card> card){
  for(int k=0;k<card.Count;k++){
    pointDeck1.Add(card[k]);
    pointDeck1[k].state=eCardState.AIdeck;
    
    pointDeck1[k].transform.position=new Vector3(34,10,10);
    pointDeck1[k].transform.Rotate(90.0f,0.0f,0.0f,Space.World);
  }
  card.RemoveAt(0);
  card.RemoveAt(1);
  card.RemoveAt(2);
  card.RemoveAt(3);




}
}
