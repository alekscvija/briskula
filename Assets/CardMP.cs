using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public enum eCardStateMP{
    deck,
    playerset,
    playerdeck,
    player2set,
    player2deck,
    player3set,
    player3deck,
    player4set,
    player4deck,
    playedbyPlayer1,
    playedbyPlayer2,
    playedbyPlayer3,
    playedbyPlayer4
}

[System.Serializable]
public class CardMP : NetworkBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        Cam = GameObject.Find("Main Camera");
        briscola=Cam.GetComponent<BriscolaMP>();

    }

    private GameObject Cam;
    private BriscolaMP briscola;
    public string Suit;
        public int Value;
        public int pointsVal;
      //  [SyncVar]
        public eCardStateMP state=eCardStateMP.deck;

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

        virtual public void OnMouseUpAsButton()
        {
          if(briscola.IsGameReady==true){
            switch (this.state)
            {
                case eCardStateMP.playerset:
                    briscola.players[0].CardClicked(this);
                    break;
                case eCardStateMP.player2set:
                    briscola.players[1].CardClicked(this);
                    break;
                case eCardStateMP.player3set:
                    briscola.players[2].CardClicked(this);
                    break;
                case eCardStateMP.player4set:
                    briscola.players[3].CardClicked(this);
                    break;

            }
}


        }
        
        [ClientRpc]
        public void RpcCardFaceUp(){
          
            this.faceUp=true;
        }
//virtual methods can be overwritten by subclass methods with the same name

}


