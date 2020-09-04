using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.Assertions.Must;


public class Briscola : MonoBehaviour
{
  static public Briscola B;

  //[Header("Set Dynamically")]
  public Deck deck;
  public Player player1;
  public AI ai;
  public Card briskula;
 // public List<int> cardSlotP= new List<int>();
 // public List<int> cardSlotAI=new List<int>();
  public List<Card> playingSpace= new List<Card>();
  List<Card> removeP=new List<Card>();
  List<Card> removeAI=new List<Card>();

  private bool playsFirst;
  public GameObject exitButton;
  public GameObject newSetButton;
  
  
  public Text winnerText;
  

  Quaternion rotOfDeck=Quaternion.Euler(90.0f,0.0f,0.0f);

    // Start is called before the first frame update
    void Start()
    {
      deck=GetComponent<Deck>();
      player1=GetComponent<Player>();
      ai=GetComponent<AI>();
      deck.cardsList=deck.InstantiateCards();
      Deck.Shuffle(ref deck.cardsList);

      winnerText = GameObject.Find("winnerText").GetComponent<Text>();
      
      briskula=deck.cardsList[deck.cardsList.Count-1];
      briskula.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
      player1.CardLayout(deck.cardsList);
      ai.AICardLayout(deck.cardsList);
      briskula.faceUp=true;
      

    }

    void Awake(){
      B=this;//singleton prospector

    }

    void Update()
    {
      
    }
    // Update is called once per frame
    

    
    

    public void CardClicked(Card card){
      /*if (ai.compSet.Count == 0 & player1.cardArr.Count == 0)
      {
        WonRound(playingSpace);
      }*/
      
      switch(card.state){
        case eCardState.deck:
        
        

        break;
        case eCardState.playerset:
          if (playingSpace.Count == 0 & player1.PWinLastRnd == true)
          {
            
              player1.tookLast = true;
              // removeP.Add(card);
              //cardSlotP.Add(player1.cardArr.FindIndex(a => a == card));
              card.state = eCardState.playedbyPlayer;
              playingSpace.Add(card);
              
              //player1.cardArr.Remove(card);
              iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);
              // card.transform.position = new Vector3(20, 10, 0);
              SpriteRenderer SR = card.gameObject.GetComponent<SpriteRenderer>();
              SR.sortingOrder = playingSpace.Count;
              player1.cardArr.Remove(card);
              
            }
          else if(playingSpace.Count==0 & player1.PWinLastRnd==false){
            AIplays(ai.compSet,playingSpace);
          }
          
         else if (player1.tookLast == false & player1.cardArr.Count!=0)
          {
            if (playingSpace.Count < 4)
            {
              if (playingSpace.Count < 3)
              {
                player1.tookLast = true;
              }
              else
              {
                player1.tookLast = false;
              }

             // removeP.Add(card);
//              cardSlotP.Add(player1.cardArr.FindIndex(a => a == card));
              card.state = eCardState.playedbyPlayer;
              playingSpace.Add(card);


              //player1.cardArr.Remove(card);
              iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);
             // card.transform.position = new Vector3(20, 10, 0);
              SpriteRenderer SR = card.gameObject.GetComponent<SpriteRenderer>();
              SR.sortingOrder = playingSpace.Count;
              player1.cardArr.Remove(card);


            }
            
            /*if (player1.cardArr.Count <= 2 & deck.cardsList.Count == 0)
            {
              card.state = eCardState.playedbyPlayer;
              removeP.Add(card);

              playingSpace.Add(card);
              //player1.cardArr.Remove(card);
              card.transform.position = new Vector3(20, 10, 0);
              SpriteRenderer SR = card.gameObject.GetComponent<SpriteRenderer>();
              SR.sortingOrder = playingSpace.Count;
              player1.tookLast = true;
             AIplays(ai.compSet, playingSpace);
            }*/
          }
          else if ( player1.tookLast==true)
          {
            /*if (playingSpace.Count < 4)
            {
              AIplays(ai.compSet, playingSpace);
            }*/
          }

         // if(playingSpace.Count==4 & deck.cardsList.Count!=0  ){
          //  WonRound(playingSpace);
            //newSet(deck.cardsList);
          //}
          
           
          
          
          

          

            break;
        case eCardState.playerdeck:
          

        break;
        case eCardState.AIdeck:

        break;
        case eCardState.AIset:
          /*
        if(playingSpace.Count<4 & player1.tookLast==true)
            {

          cardSlotAI.Add(ai.compSet.FindIndex(a =>a==card ));
          removeAI.Add(card);
          card.state=eCardState.playedbyAI;
          playingSpace.Add(card);
        //  ai.compSet.Remove(card);
          card.transform.position=new Vector3(20,10,0);
          card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
          card.faceUp=true;
          SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
          SR.sortingOrder=playingSpace.Count;
          player1.tookLast=false;


            }*/
        /*if(playingSpace.Count==4 & deck.cardsList.Count!=0 ){
          WonRound(playingSpace);
          newSet(deck.cardsList);
        }
        else if (playingSpace.Count == 4 & deck.cardsList.Count == 0)
        {
          WonRound(playingSpace);
          bool winner=calcWinner(player1.pointDeck, ai.pointDeck1);
          GameOver(winner);

        }
        

        if (ai.compSet.Count <= 2 & deck.cardsList.Count == 0)
        {
        /*  card.state=eCardState.playedbyAI;
          playingSpace.Add(card);
          //  ai.compSet.Remove(card);
          card.transform.position=new Vector3(20,10,0);
          card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
          card.faceUp=true;
          SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
          SR.sortingOrder=playingSpace.Count;
          player1.tookLast=false;
         
        }
*/

        break;
      }
      if (playingSpace.Count == 4 & deck.cardsList.Count == 0 & player1.cardArr.Count==0 & ai.compSet.Count==0)
      {
        WonRound(playingSpace);
        bool winner=calcWinner(player1.pointDeck, ai.pointDeck1);
        GameOver(winner);

      }

    }

    public void WonRound(List<Card> playedCards){
       String firstSuit=playedCards[0].Suit;
       Card max=playedCards[0];
       for(int j=0;j<4;j++){
         if (string.Equals(playedCards[j].Suit, briskula.Suit))
         {
           if (max.pointsVal <= playedCards[j].pointsVal)
           {
             if (max.Value != 1 & max.Value != 3 & max.Value<playedCards[j].Value)
             {
               max = playedCards[j];
             }
             else if(max.pointsVal<playedCards[j].pointsVal)
             {
               max = playedCards[j];
             }

           }
         }
         else if (string.Equals(playedCards[j].Suit,firstSuit)){
           if(max.pointsVal<=playedCards[j].pointsVal){
             if (max.Value != 1 & max.Value != 3 & max.Value<playedCards[j].Value)
             {
               max = playedCards[j];
             }
             else if(max.pointsVal<playedCards[j].pointsVal)
             {
               max = playedCards[j];
             }
            
         }
         }
         
       }
       if(max.state==eCardState.playedbyPlayer){
         player1.pointDeck.AddRange(playedCards);
         player1.PWinLastRnd=true;
         foreach(Card c in playedCards)
         {
           iTween.MoveTo(c.gameObject, new Vector3(2, 10, -5),1);
          // c.transform.position=new Vector3(2,10,-5);
           if(c.state==eCardState.playedbyPlayer){
           c.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
          }
           c.state = eCardState.playerdeck;
           c.faceUp=false;
         }

         

       }
       else if (max.state==eCardState.playedbyAI){
         ai.pointDeck1.AddRange(playedCards);
         player1.PWinLastRnd=false;
         foreach(Card c in playedCards){
           iTween.MoveTo(c.gameObject, new Vector3(2, 10, 5),1);
          // c.transform.position=new Vector3(2,10,5);
           if(c.state==eCardState.playedbyPlayer){
           c.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);}
           c.faceUp=false;
           c.state = eCardState.AIdeck;

         }
         
       }

       playedCards.Clear();

    }

   public void newSet(List<Card> deck1){
     // player1.cardArr.Remove(removeP[0]);
    //  player1.cardArr.Remove(removeP[1]);
    // ai.compSet.Remove(removeAI[0]);
     // ai.compSet.Remove(removeAI[1]);
      
      //removeAI.Clear();
      if (deck1.Count != 0)
      {
        if (player1.PWinLastRnd == true)
        {

          deck1[0].state = eCardState.playerset;
          deck1[2].state = eCardState.playerset;
          deck1[1].state = eCardState.AIset;
          deck1[3].state = eCardState.AIset;
          
          player1.cardArr.Add(deck1[0]);
          player1.cardArr.Add(deck1[2]);
          ai.compSet.Add(deck1[1]);
          ai.compSet.Add(deck1[3]);



          for (int i = 0; i < 4; i++)
          {
            iTween.MoveTo(player1.cardArr[i].gameObject, player1.playerSlot[i],1);
            //player1.cardArr[i].transform.position = player1.playerSlot[i];
            player1.cardArr[i].faceUp = true;
            player1.cardArr[i].transform.rotation = rotOfDeck;
            iTween.MoveTo(ai.compSet[i].gameObject, ai.AISlot[i],1);
            //ai.compSet[i].transform.position = ai.AISlot[i];
            ai.compSet[i].faceUp = false;
            ai.compSet[i].transform.rotation = rotOfDeck;
          }
          


          
          
         


       
         
        }


        else
        {

          deck1[0].state = eCardState.AIset;
          deck1[2].state = eCardState.AIset;
          deck1[1].state = eCardState.playerset;
          deck1[3].state = eCardState.playerset;

          player1.cardArr.Add(deck1[1]);
          player1.cardArr.Add(deck1[3]);
          ai.compSet.Add(deck1[0]);
          ai.compSet.Add(deck1[2]);
          for (int i = 0; i < 4; i++)
          {
            iTween.MoveTo(player1.cardArr[i].gameObject, player1.playerSlot[i],1);

           // player1.cardArr[i].transform.position = player1.playerSlot[i];
            player1.cardArr[i].faceUp = true;
            player1.cardArr[i].transform.rotation = rotOfDeck;
            iTween.MoveTo(ai.compSet[i].gameObject, ai.AISlot[i],1);

           // ai.compSet[i].transform.position = ai.AISlot[i];
            ai.compSet[i].faceUp = false;
            ai.compSet[i].transform.rotation = rotOfDeck;
          }

          
         
          

        }
        deck1.RemoveRange(0,4);

      }

     /* player1.cardArr=player1.cardArr.OrderBy( a => a.transform.position.x)
    .ToList();
    ai.compSet=ai.compSet.OrderBy(a => a.transform.position.x)
      .ToList();*/
    

    }

    public bool calcWinner(List<Card> AIpoints,List<Card> PlayerPoints)
    {
      int totalAI = AIpoints.Sum(a=>a.pointsVal);
      int totalPlayer = PlayerPoints.Sum(a=>a.pointsVal);
      bool result;
      if (totalPlayer > totalAI)
      {
        result = true;
      }
      else
      {
        result = false;
      }

      return result;


    }
    void GoToScene(){
      SceneManager.LoadScene("SampleScene");
    }
    public void GameOver(bool won)
    {
      if (won)
        {
          winnerText.text=" You won!";
        }
        else
        {
          winnerText.text="GameOver.\n You lost.";
        }

      exitButton.SetActive(true);
        Invoke("GoToScene",10);
      }
    public void exitScene()
    {
      Application.Quit();
    }

    

    public void AIplays(List<Card> aiSet, List<Card> playedCards)
    {
      Card playedCard;

     switch (playedCards.Count)
      {
        case 0:
          if (player1.PWinLastRnd == false)
          {
            Card card = aiSet.Aggregate((i1, i2) => i1.pointsVal < i2.pointsVal ? i1 : i2);
            playedCards.Add(card);
//            cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
           //removeAI.Add(card);
            card.state=eCardState.playedbyAI;
            iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

            //card.transform.position=new Vector3(20,10,0);
            card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
            card.faceUp=true;
            SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
            SR.sortingOrder=playingSpace.Count;
            player1.tookLast=false;
            aiSet.Remove(card);


          }
          break;
        
        case 1:
           playedCard=playedCards[0];

          if (player1.tookLast == true)
          {
            if (playedCard.pointsVal >= 2 & !string.Equals(briskula.Suit, playedCard.Suit))
            {
              if (aiSet.Exists(a => string.Equals(a.Suit, briskula.Suit)))
              {
                Card card =aiSet.Find(a => string.Equals(a.Suit, briskula.Suit));
                playedCards.Add(card);
//                cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
                //removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

                //card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);


              }
              else
              {
                if (aiSet.Exists(a => string.Equals(a.Suit, playedCard.Suit) & a.pointsVal>playedCard.pointsVal))
                {

                  Card card =aiSet.Find(a =>
                    string.Equals(a.Suit, playedCard.Suit) & a.pointsVal > playedCard.pointsVal);
                  playedCards.Add(card);
//                  cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
                 //removeAI.Add(card);
                  card.state=eCardState.playedbyAI;
                  iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

                  //card.transform.position=new Vector3(20,10,0);
                  card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                  card.faceUp=true;
                  SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                  SR.sortingOrder=playingSpace.Count;
                  player1.tookLast=false;
                  aiSet.Remove(card);


                }
                else
                {
                  Card card = aiSet.Aggregate((i1, i2) => i1.pointsVal < i2.pointsVal ? i1 : i2);
                  playedCards.Add(card);
//                  cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
               //  removeAI.Add(card);
                  card.state=eCardState.playedbyAI;
                  iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

                  //card.transform.position=new Vector3(20,10,0);
                  card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                  card.faceUp=true;
                  SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                  SR.sortingOrder=playingSpace.Count;
                  player1.tookLast=false;
                  aiSet.Remove(card);



                }

              }
            }
            else if (playedCard.pointsVal >= 2 & string.Equals(playedCard.Suit,briskula.Suit) )
            {
              if (aiSet.Exists(a => string.Equals(a.Suit, briskula.Suit) & a.pointsVal > briskula.pointsVal))
              {
                Card card=aiSet.Find(a => string.Equals(a.Suit, briskula.Suit) & a.pointsVal > briskula.pointsVal);
                playedCards.Add(card);
//                cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
               // removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

                //card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);


              }
              else
              {
                Card card=aiSet.Aggregate((i1,i2) => i1.pointsVal < i2.pointsVal ? i1 : i2);
                playedCards.Add(card);
//                cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
             //  removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

               // card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);


              }
            }
          
            else if (playedCard.pointsVal < 2)
            {
              Card card=aiSet.Aggregate((i1,i2) => i1.pointsVal < i2.pointsVal ? i1 : i2);
              playedCards.Add(card);
//              cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
             //removeAI.Add(card);
              card.state=eCardState.playedbyAI;
              iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

              //card.transform.position=new Vector3(20,10,0);
              card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
              card.faceUp=true;
              SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
              SR.sortingOrder=playingSpace.Count;
              player1.tookLast=false;
              aiSet.Remove(card);



            }

          }

          break;
        
        case 2:
           playedCard = playedCards[1];
          if (player1.tookLast == true)
          {
         
            if (string.Equals(playedCard.Suit, briskula.Suit))
            {
              if (aiSet.Exists(a => string.Equals(a.Suit, briskula.Suit) & a.pointsVal > playedCard.pointsVal))
              {
                Card card=
                  aiSet.Find(a => string.Equals(a.Suit, briskula.Suit) & a.pointsVal > playedCard.pointsVal);
                playedCards.Add(card);
//                cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
               //removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

                //card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);


              }
              else
              {
                Card card = aiSet.Aggregate((i1,i2) => i1.pointsVal < i2.pointsVal ? i1 : i2);
                playedCards.Add(card);
//                cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
              //  removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

                //card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);


              }
            }
            else if(string.Equals(playedCard.Suit,playedCards[0].Suit) & playedCards[0].pointsVal<playedCard.pointsVal)
            {
              if (aiSet.Exists(a=>string.Equals(a.Suit,briskula.Suit)))
              {
                Card card=aiSet.Find(a => string.Equals(a.Suit, briskula.Suit));
                playedCards.Add(card);
//                cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
               // removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

                //card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);


              }
              else if (aiSet.Exists(a => string.Equals(a.Suit, playedCard.Suit) & a.pointsVal > playedCard.pointsVal))
              {
                Card card=aiSet.Find(a => string.Equals(a.Suit, playedCard.Suit) & a.pointsVal > playedCard.pointsVal);
                playedCards.Add(card);
//                cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
                //removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

                //card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);



              }
              else
              {
                
                Card card =aiSet.Aggregate((i1,i2) => i1.pointsVal < i2.pointsVal ? i1 : i2);
                playedCards.Add(card);
//                cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
                //removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

                //card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);


              }
              
            }
            else
            {
              Card card=aiSet.Aggregate((i1,i2) => i1.pointsVal < i2.pointsVal ? i1 : i2);
              playedCards.Add(card);
//              cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
             // removeAI.Add(card);
              card.state=eCardState.playedbyAI;
              iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

             // card.transform.position=new Vector3(20,10,0);
              card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
              card.faceUp=true;
              SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
              SR.sortingOrder=playingSpace.Count;
              player1.tookLast=false;
              aiSet.Remove(card);


            }
            
            



          }
          

          break;
        
        
        case 3:
         playedCard = playedCards[1];
         
        if (player1.tookLast == true )
          {
            if (string.Equals(playedCards[0].Suit, briskula.Suit) || string.Equals(playedCards[2].Suit, briskula.Suit))
            {
              if (string.Equals(playedCard.Suit, briskula.Suit) & (playedCard.pointsVal > playedCards[0].pointsVal) ||
                  (playedCards[2].pointsVal < playedCard.pointsVal))
              {
                Card card=aiSet.Aggregate((i1, i2) => i1.pointsVal > i2.pointsVal ? i1 : i2);
                playedCards.Add(card);
//                cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
               // removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

                //card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);


              }
              else
              {
                Card card=aiSet.Aggregate((i1, i2) => i1.pointsVal < i2.pointsVal ? i1 : i2);
                playedCards.Add(card);
//                cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
                //  removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

               // card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);


              }
              
            }
           else if (string.Equals(playedCards[0].Suit, playedCard.Suit) || string.Equals(playedCards[2].Suit, playedCard.Suit))
            {
              if ( playedCard.pointsVal > playedCards[0].pointsVal &
                  playedCards[2].pointsVal < playedCard.pointsVal)
              {
                Card card=aiSet.Aggregate((i1, i2) => i1.pointsVal > i2.pointsVal ? i1 : i2);
                playedCards.Add(card);
//                cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
               // removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

               // card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);


              }
              else if(aiSet.Exists(a=> string.Equals(a.Suit,briskula.Suit)))
              {
                Card card = aiSet.Find(a => string.Equals(a.Suit, briskula.Suit));
                playedCards.Add(card);
//              cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
                //removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

                //card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);
                
              }
              else
              {
                Card card=aiSet.Aggregate((i1, i2) => i1.pointsVal < i2.pointsVal ? i1 : i2);
                playedCards.Add(card);
//                cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
                //  removeAI.Add(card);
                card.state=eCardState.playedbyAI;
                iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

                //card.transform.position=new Vector3(20,10,0);
                card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
                card.faceUp=true;
                SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
                SR.sortingOrder=playingSpace.Count;
                player1.tookLast=false;
                aiSet.Remove(card);
                
              }
            }
            else 
            {
              Card card=aiSet.Aggregate((i1, i2) => i1.pointsVal < i2.pointsVal ? i1 : i2);
              playedCards.Add(card);
//              cardSlotAI.Add(aiSet.FindIndex(a =>a==card ));
              //removeAI.Add(card);
              card.state=eCardState.playedbyAI;
              iTween.MoveTo(card.gameObject,new Vector3(20,10,0) ,1);

              //card.transform.position=new Vector3(20,10,0);
              card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
              card.faceUp=true;
              SpriteRenderer SR=card.gameObject.GetComponent<SpriteRenderer>();
              SR.sortingOrder=playingSpace.Count;
              player1.tookLast=false;
              aiSet.Remove(card);


            }
            
          }
         

          break;
        
        case 4:
          if (playingSpace.Count == 4 & deck.cardsList.Count == 0 & player1.cardArr.Count==0 & ai.compSet.Count==0)
          {
            WonRound(playingSpace);
            bool winner=calcWinner(player1.pointDeck, ai.pointDeck1);
            GameOver(winner);

          }

          break;
        
      }

       

      
      
        
          
      
    }
    
   
    }
