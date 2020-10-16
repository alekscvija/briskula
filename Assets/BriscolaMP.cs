using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.Assertions.Must;

public class BriscolaMP : MonoBehaviour
{
 //   static public BriscolaMP BMp;
	private PlayerMP LocalPlayer;
    public DeckMP deck;
    private CardMP briskula;
    // public List<int> cardSlotP= new List<int>();
    // public List<int> cardSlotAI=new List<int>();
    public List<CardMP> playingSpace= new List<CardMP>();
    List<CardMP> removeP=new List<CardMP>();
    List<CardMP> removeAI=new List<CardMP>();

    private bool playsFirst;
    private GameObject exitButton;
    private GameObject newSetButton;
    public bool IsGameReady;
    public bool IsGameOver;

    public Quaternion[] rotOfCam=new Quaternion[4];

    public Text winnerText;
    public Text playerScore;
    public Text AIScore;

    public List<PlayerMP> players=new List<PlayerMP>();
    public int MinimumPlayersForGame = 2;
	
	private bool runOnce=false;
	
	private bool runByServerOnce=false;


    public Quaternion rotOfDeck=Quaternion.Euler(90.0f,0.0f,0.0f);


    // Start is called before the first frame update


 public void Start ()
  {	
   deck=GetComponent<DeckMP>();



  }





    // Update is called once per frame
    void Update()
    {
		
        if (NetworkManager.singleton.isNetworkActive){
          GameReadyCheck();
          GameOverCheck();
			if (LocalPlayer == null)
                {
                    FindLocalPlayer();
                }
			if (runOnce==false & IsGameReady==true)
		{
		//runOnce=true;
		NetworkIdentity identity=NetworkClient.connection.identity;
		PlayerMP player=identity.GetComponent<PlayerMP>();
		if(identity.isServer & runByServerOnce==false){
		runByServerOnce=true;
        deck.InstantiateCards();

        winnerText = GameObject.Find("winnerText").GetComponent<Text>();
        winnerText.gameObject.SetActive(false);
        playerScore = GameObject.Find("playerScore").GetComponent<Text>();
        playerScore.gameObject.SetActive(false);
        AIScore = GameObject.Find("aiScore").GetComponent<Text>();
        AIScore.gameObject.SetActive(false);


		DeckMP.Shuffle(ref deck.cardsList);
		deck.cardsList=deck.cardsList.Distinct().ToList();
		for(int i=0;i<40;i++){
		deck.RpcAddCardList(deck.cardsList[i].gameObject);
        }
        briskula=deck.cardsList[deck.cardsList.Count-1];
		briskula.RpcCardFaceUp();
		briskula.faceUp=true;
        briskula.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
		LocalPlayer.CardLayout();
		for(int i=2;i<3;i++){
		LocalPlayer.Camera=GameObject.Find("Camera"+i);
		LocalPlayer.Camera.SetActive(false);
		}

}

		if(runOnce==false & !identity.isServer ){

		if(LocalPlayer.playerNo>1){
		Debug.Log("Camera"+LocalPlayer.playerNo);

		LocalPlayer.Camera=GameObject.Find("Camera"+LocalPlayer.playerNo);
		LocalPlayer.Cam.SetActive(false);
		LocalPlayer.Camera.SetActive(true);
		}
		LocalPlayer.CardLayout();
		runOnce=true;
        }
		

		
		
 		}
        else
        {
                //Cleanup state once network goes offline
                IsGameReady = false;
                LocalPlayer = null;
                players.Clear();
        }
 		

    }
}




   public void WonRound(List<CardMP> playedCards){
       String firstSuit=playedCards[0].Suit;
       CardMP max=playedCards[0];
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

       if(max.state==eCardStateMP.playedbyPlayer1){
         players[0].pointDeck.AddRange(playedCards);
         players[0].PWinLastRnd=true;
         foreach(CardMP c in playedCards)
         {
           iTween.MoveTo(c.gameObject, new Vector3(2, 10, -5),1);
          // c.transform.position=new Vector3(2,10,-5);
           if(c.state==eCardStateMP.playedbyPlayer1){
           c.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
          }
           c.state = eCardStateMP.playerdeck;
           c.faceUp=false;
           playerScore.text = "Score:" + players[0].cardArr.Sum(a => a.pointsVal);
         }

       }
       else if (max.state==eCardStateMP.playedbyPlayer2){
         players[1].pointDeck.AddRange(playedCards);
         players[1].PWinLastRnd=false;
         foreach(CardMP c in playedCards){
           iTween.MoveTo(c.gameObject, new Vector3(2, 10, 5),1);
          // c.transform.position=new Vector3(2,10,5);
           if(c.state==eCardStateMP.playedbyPlayer2){
           c.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);}
           c.faceUp=false;
           c.state = eCardStateMP.player2deck;
           AIScore.text = "Score:" + players[1].cardArr.Sum(a => a.pointsVal);

         }

       }

       else if (max.state==eCardStateMP.playedbyPlayer3){
         players[2].pointDeck.AddRange(playedCards);
         players[2].PWinLastRnd=false;
         foreach(CardMP c in playedCards){
           iTween.MoveTo(c.gameObject, new Vector3(2, 10, 5),1);
          // c.transform.position=new Vector3(2,10,5);
           if(c.state==eCardStateMP.playedbyPlayer3){
           c.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);}
           c.faceUp=false;
           c.state = eCardStateMP.player3deck;
           AIScore.text = "Score:" + players[2].cardArr.Sum(a => a.pointsVal);

         }
       }

       else if (max.state==eCardStateMP.playedbyPlayer4){
         players[3].pointDeck.AddRange(playedCards);
         players[3].PWinLastRnd=false;
         foreach(CardMP c in playedCards){
           iTween.MoveTo(c.gameObject, new Vector3(2, 10, 5),1);
          // c.transform.position=new Vector3(2,10,5);
           if(c.state==eCardStateMP.playedbyPlayer4){
           c.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);}
           c.faceUp=false;
           c.state = eCardStateMP.player3deck;
           AIScore.text = "Score:" + players[3].cardArr.Sum(a => a.pointsVal);

         }
       }

       playedCards.Clear();

    }

   void  GameReadyCheck(){
 if (!IsGameReady)
            {
                //Look for connections that are not in the player list
               foreach (KeyValuePair<uint, NetworkIdentity> kvp in NetworkIdentity.spawned)
                {
                    PlayerMP comp = kvp.Value.GetComponent<PlayerMP>();

                    //Add if new
                    if (comp != null && !players.Contains(comp))
                    {
                        players.Add(comp);
						comp.playerNo=players.Count;
						comp.gameObject.name="Player"+players.Count;
						comp.isReady=true;
                    }
                }
    if (players.Count >= MinimumPlayersForGame)
    {
        bool AllReady = true;
        foreach (PlayerMP player in players)
        {
            if (!player.isReady)
            {
				
                AllReady = false;
            }
        }
        if (AllReady)
        {
            IsGameReady = true;

        }
    }
  }


  }

  void GameOverCheck()
  {
	
      //Cant win a game you play by yourself. But you can still use this example for testing network/movement
      if (players.Count == 1)
          return;

      if(deck!=null){
		if(deck.cardsList.Count==0){
        bool CheckEnd=false;
        foreach(PlayerMP player in players){
          if(player.cardArr.Count==0){
            CheckEnd=true;
          }
          else{
            CheckEnd=false;
            return;
          }
        }
        if (CheckEnd==true){
        IsGameOver = true;
}
      }
		}
      return;

  }

void FindLocalPlayer()
        {
            //Check to see if the player is loaded in yet
            if (ClientScene.localPlayer == null)
                return;

            LocalPlayer = ClientScene.localPlayer.GetComponent<PlayerMP>();
        }
[Server]
void CheckPlayingSpace(){


}
}
