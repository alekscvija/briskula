using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;


public class PlayerMP : NetworkBehaviour
{
    public List<CardMP> cardArr = new List<CardMP>();
     public GameObject Cam;
    public BriscolaMP briscola;

    int points;

	public GameObject PlayerArea;
	public GameObject EnemyArea;

    public List<CardMP> pointDeck = new List<CardMP>();

[SyncVar]
    public bool PWinLastRnd = false;
[SyncVar]
    public bool host;
[SyncVar]
    public int playerNo;

    private bool tooklast = false;
    [SyncVar]
    public bool isReady;

    public bool tookLast
    {
        get { return (tooklast); }
        set { tooklast = value; }
    }
    private bool played = false;

    public bool playedNow
    {
        get { return (played); }
        set { played = value; }
    }


    public Vector3[] playerSlot = new Vector3[4];

    public DeckMP deck1;
	public GameObject Camera;
	
	override public void OnStartClient(){

		base.OnStartClient();
		
		Cam = GameObject.Find("Main Camera");
		
        briscola=Cam.GetComponent<BriscolaMP>();		
		CmdAddToPlayerList();
		PlayerArea=GameObject.Find("PlayerArea");
        EnemyArea=GameObject.Find("EnemyArea");
		}	

	

	
	
    // Start is called before the first frame update
   /*public void Start()
    {	
		if(!isLocalPlayer) return;
		if(hasAuthority){
        CmdAddPlayer();
		}
        //RectTransform temp=Cam.GetComponent<RectTransform>();
        
        //temp.rotation= briscola.rotOfCam[playerNo - 1];
        
        //CmdCardLayout();
		
		
		




    }
*/

	[Command]
	public void CmdAddToPlayerList(){
	NetworkIdentity	networkIdentity= NetworkClient.connection.identity;
		PlayerMP player=networkIdentity.GetComponent<PlayerMP>();
	if (player!= null && !briscola.players.Contains(player))
        {

		 	briscola.players.Add(player);
        }

}
	
	[ClientRpc]
	public void RpcSetReady(){
isReady=true;
if(isServer){
		host=true;
		}
}

   [Command]
	public void CmdAddPlayer(){
		
		NetworkIdentity	networkIdentity= NetworkClient.connection.identity;
		PlayerMP player=networkIdentity.GetComponent<PlayerMP>();
		
		playerNo=briscola.players.Count;
		player.gameObject.name="Player"+playerNo;
		RpcSetReady();




}
	[ClientRpc]
	public void RpcSetFaceUp(int a){
	if (isLocalPlayer){
	cardArr[a].faceUp = true;
	}
}
[Command] 
void CmdAuthority(int i){

            NetworkIdentity identity=briscola.deck.cardsList[i].gameObject.GetComponent<NetworkIdentity>();
			identity.AssignClientAuthority(connectionToClient);
}

    // Update is called once per frame
[Client]
    public void CardLayout()
    {	            
           int j=0;
		NetworkIdentity	networkIdentity= NetworkClient.connection.identity;
		PlayerMP player1=networkIdentity.GetComponent<PlayerMP>();
			switch(playerNo){
			case 1:
			for(int i=0;i<4;i++){

		    cardArr.Add(briscola.deck.cardsList[i*2]);
		//	RpcSetFaceUp(i*2);
			briscola.deck.cardsList[i*2].faceUp=true;
			briscola.deck.cardsList[i*2].state = eCardStateMP.playerset;
			//CmdAuthority(i*2);
			
			briscola.deck.cardsList[i*2].gameObject.transform.SetParent(PlayerArea.transform);
			briscola.deck.cardsList[i*2+1].gameObject.transform.SetParent(EnemyArea.transform);
			playerSlot[i] = new Vector3(i*6-10, 10, -15);
			//RpcAddToPlayerCards(briscola.deck.cardsList[i*2].gameObject,i);
            //RpcMoveCard(briscola.deck.cardsList[i*2].gameObject,i);
			Vector3 EnemyPos= new Vector3(i*6-10, 10, 15);
			iTween.MoveTo(briscola.deck.cardsList[i*2].gameObject,
             playerSlot[i], 1);
		
			iTween.MoveTo(briscola.deck.cardsList[i*2+1].gameObject,
             EnemyPos, 1);
					
							   }
			briscola.deck.cardsList.RemoveRange(0,8);

			break;
			case 2:
			for(int i=0;i<4;i++){

			briscola.deck.cardsList[i*2+1].faceUp=true;
			briscola.deck.cardsList[i*2+1].state = eCardStateMP.player2set;
			//RpcSetFaceUp(i*2+1);
			cardArr.Add(briscola.deck.cardsList[i*2+1]);

			playerSlot[i] = new Vector3(i*6-10, 10, -15);

	//		CmdAuthority(i*2+1);
			
			briscola.deck.cardsList[i*2+1].gameObject.transform.SetParent(PlayerArea.transform);
			briscola.deck.cardsList[i*2].gameObject.transform.SetParent(EnemyArea.transform);
			Vector3 EnemyPos= new Vector3(i *6-10, 10, 15);
			//RpcAddToPlayerCards(briscola.deck.cardsList[i*2+1].gameObject,i);
          //  RpcMoveCard(briscola.deck.cardsList[i*2+1].gameObject,i);
		//	if(briscola.deck.cardsList[i*2+1].gameObject.transform.IsChildOf(PlayerArea.transform)){
			iTween.MoveTo(briscola.deck.cardsList[i*2+1].gameObject,
             playerSlot[i], 1);
		//	}
		//	else {
			iTween.MoveTo(briscola.deck.cardsList[i*2].gameObject,
            EnemyPos , 1);
		//	}
			
			
		
							    }
			briscola.deck.cardsList.RemoveRange(0,8);

			break;
			
}

         
}
        
    





 	[ClientRpc]
	void RpcAddToPlayerCards(GameObject card,int i){
		if(isLocalPlayer){
	    cardArr.Add(card.GetComponent<CardMP>());
		CmdShowCard(card);
		briscola.deck.cardsList.RemoveAt(i);
}
	}

[ClientRpc]
	void RpcSetPlayerSlot(GameObject card,int i){
     playerSlot[i] = card.transform.position;

	}

    public void CardClicked(CardMP card)
    {
        if (isLocalPlayer)
        {
            if (playedNow == false & briscola.playingSpace.Count<4)
            {
                CmdPlayerMove(card.gameObject);

            }


        }
    }


	[Command]
	public void CmdDeleteOnServer(int i){
	briscola.deck.cardsList.RemoveAt(i);


}
	[Command]
    public void CmdPlayerMove(GameObject card)
    {
		CardMP tempCard=card.GetComponent<CardMP>();
        // removeP.Add(card);
//              cardSlotP.Add(player1.cardArr.FindIndex(a => a == card));

        if (briscola.players[0] == this)
        {
            tempCard.state = eCardStateMP.playedbyPlayer1;
			if(briscola.playingSpace.Count>=2 & this.playedNow==false){
			RpcSetPlayed(1);
			}
        }

        else if (briscola.players[1]==this)
        {
            tempCard.state = eCardStateMP.playedbyPlayer2;
			if (briscola.players.Count==2){
			RpcSetPlayed(0);
			}
        }

        else if (briscola.players[2] == this)
        {
            tempCard.state = eCardStateMP.playedbyPlayer3;
        }

        else
        {
            tempCard.state = eCardStateMP.playedbyPlayer4;
        }


        briscola.playingSpace.Add(tempCard);
		
        //player1.cardArr.Remove(card);
     //   iTween.MoveTo(card, new Vector3(20, 10, 0), 1);
		Vector3 deck=new Vector3 (20,10,0);
		if(briscola.playingSpace.Count%2==1){
		card.transform.Rotate(0.0f,0.0f,90.0f,Space.Self);
		}
		
		RpcMoveCard(card,deck);
        // card.transform.position = new Vector3(20, 10, 0);
        SpriteRenderer SR = card.GetComponent<SpriteRenderer>();
        SR.sortingOrder = briscola.playingSpace.Count;
        RpcRemoveCard(card);
        


    }

[ClientRpc]
void RpcSetPlayed(int i){
briscola.players[i].playedNow=false;

}

	[ClientRpc]
void RpcMoveCard(GameObject card,Vector3 position){
iTween.MoveTo(card,
                    position, 1);	
CardMP c=card.GetComponent<CardMP>();
c.faceUp=true;
}
[ClientRpc]
void RpcRemoveCard(GameObject card)
{
if(isLocalPlayer){
CardMP tempCard=card.GetComponent<CardMP>();
cardArr.Remove(tempCard);
playedNow = true;
}

}

[Command]
public void CmdShowCard(GameObject card){
	//NetworkServer.Spawn(card,connectionToClient);
	card.transform.SetParent(briscola.deck.cardAnchor,false);
}
}
