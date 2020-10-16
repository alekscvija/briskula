using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class DeckMP : NetworkBehaviour
{
    // Start is called before the first frame update
   public bool startFaceUp=false;
public Transform cardAnchor;

private GameObject _tGO=null;
private SpriteRenderer _tSR=null;
[Header("Set In Inspector")]

public Sprite OneBastoni;
public Sprite OneCoppa;
public Sprite OneDanari;
public Sprite OneSpade;
public Sprite TwoBastoni;
public Sprite TwoCoppe;
public Sprite TwoDanari;
public Sprite TwoSpade;
public Sprite TriBastoni;
public Sprite TriCoppa;
public Sprite TriDanari;
public Sprite TriSpade;
public Sprite FourBastoni;
public Sprite FourCoppa;
public Sprite FourDanari;
public Sprite FourSpade;
public Sprite FiveBastoni;
public Sprite FiveCoppa;
public Sprite FiveDanari;
public Sprite FiveSpade;
public Sprite SixBastoni;
public Sprite SixCoppa;
public Sprite SixDanari;
public Sprite SixSpade;
public Sprite SevenBastoni;
public Sprite SevenCoppa;
public Sprite SevenDanari;
public Sprite SevenSpade;
public Sprite ElevenBastoni;
public Sprite ElevenCoppa;
public Sprite ElevenDanari;
public Sprite ElevenSpade;
public Sprite TwelveBastoni;
public Sprite TwelveCoppa;
public Sprite TwelveDanari;
public Sprite TwelveSpade;
public Sprite ThirteenBastoni;
public Sprite ThirteenCoppa;
public Sprite ThirteenDanari;
public Sprite ThirteenSpade;
public Sprite cardBack;
public GameObject prefabCardMP;
public GameObject prefabSprite;
[Header("Set Dynamically")]



public List<CardMP> cardsList=new List<CardMP>();
public List<GameObject> CardObjects=new List<GameObject>();
//public List<GameObject> GOList=new List<GameObject>();
  void Start()
  {

  }



  [Server]
  public void InstantiateCards(){
  // if(GameObject.GetComponent<Deck>()==null){

  // }
   int j=0;

   string[] Suit={"Spade","Dinari","Coppe","Bastoni"};
   int[] Value={1,2,3,4,5,6,7,11,12,13};
   int[] pointval={11,0,10,0,0,0,0,2,3,4};
	foreach (string s in Suit){
  		for (int i=0;i<Value.Length;i++){
	GameObject cardObj=Instantiate(prefabCardMP);
	iTween.MoveTo(cardObj, new Vector3(-29, 1, 0), 1);
    iTween.MoveTo(cardObj, new Vector3(-30, 0, 0), 1);
	CardMP c =cardObj.GetComponent<CardMP>();
    SpriteRenderer sprite=cardObj.GetComponent<SpriteRenderer>();
	sprite.sortingOrder=1;
    if(string.Equals(s,"Spade")){
      switch(Value[i])
      {
        case(1):
        sprite.sprite=OneSpade;
        break;
        case(2):
        sprite.sprite=TwoSpade;
        break;
        case(3):
        sprite.sprite=TriSpade;
        break;
        case(4):
        sprite.sprite=FourSpade;
        break;
        case(5):
        sprite.sprite=FiveSpade;
        break;
        case(6):
        sprite.sprite=SixSpade;
        break;
        case(7):
        sprite.sprite=SevenSpade;
        break;
        case(11):
        sprite.sprite=ElevenSpade;
        break;
        case(12):
        sprite.sprite=TwelveSpade;
        break;
        case(13):
        sprite.sprite=ThirteenSpade;
        break;
      }
    }
      else if (string.Equals(s,"Dinari")){
        switch(Value[i]){
          case(1):
          sprite.sprite=OneDanari;
          break;
          case(2):
          sprite.sprite=TwoDanari;
          break;
          case(3):
          sprite.sprite=TriDanari;
          break;
          case(4):
          sprite.sprite=FourDanari;
          break;
          case(5):
          sprite.sprite=FiveDanari;
          break;
          case(6):
          sprite.sprite=SixDanari;
          break;
          case(7):
          sprite.sprite=SevenDanari;
          break;
          case(11):
          sprite.sprite=ElevenDanari;
          break;
          case(12):
          sprite.sprite=TwelveDanari;
          break;
          case(13):
          sprite.sprite=ThirteenDanari;
          break;
        }
      }
      else if(string.Equals(s,"Coppe")){
        switch(Value[i]){
          case(1):
          sprite.sprite=OneCoppa;
          break;
          case(2):
          sprite.sprite=TwoCoppe;
          break;
          case(3):
          sprite.sprite=TriCoppa;
          break;
          case(4):
          sprite.sprite=FourCoppa;
          break;
          case(5):
          sprite.sprite=FiveCoppa;
          break;
          case(6):
          sprite.sprite=SixCoppa;
          break;
          case(7):
          sprite.sprite=SevenCoppa;
          break;
          case(11):
          sprite.sprite=ElevenCoppa;
          break;
          case(12):
          sprite.sprite=TwelveCoppa;
          break;
          case(13):
          sprite.sprite=ThirteenCoppa;
          break;

        }
      }
      else{
        switch(Value[i]){
          case(1):
          sprite.sprite=OneBastoni;
          break;
          case(2):
          sprite.sprite=TwoBastoni;
          break;
          case(3):
          sprite.sprite=TriBastoni;
          break;
          case(4):
          sprite.sprite=FourBastoni;
          break;
          case(5):
          sprite.sprite=FiveBastoni;
          break;
          case(6):
          sprite.sprite=SixBastoni;
          break;
          case(7):
          sprite.sprite=SevenBastoni;
          break;
          case(11):
          sprite.sprite=ElevenBastoni;
          break;
          case(12):
          sprite.sprite=TwelveBastoni;
          break;
          case(13):
          sprite.sprite=ThirteenBastoni;
          break;

        }

      }
	c.Suit=s;
    c.Value=Value[i];
    c.pointsVal=pointval[i];
	AddBack(c);
	cardObj.name=c.Suit+" "+c.Value;
	NetworkServer.Spawn(cardObj,connectionToClient);
	cardsList.Add(c);

	RpcSetSprite(s,i,j,Value,pointval,cardObj);

}

	
}

	}
	
[ClientRpc]
	public void RpcAddCard(GameObject card){
	CardObjects.Add(card); //Rpc add these to clients lists 	
}
[ClientRpc]
public void RpcSetSprite(string S,int I,int j,int[] Value,int[] pointval,GameObject CardObj){
	if(!isServer){
 
  CardMP c =CardObj.GetComponent<CardMP>();
  SpriteRenderer sprite=CardObj.GetComponent<SpriteRenderer>();
  sprite.sortingOrder=1;
	if(string.Equals(S,"Spade")){
      switch(Value[I])
      {
        case(1):
        sprite.sprite=OneSpade;
        break;
        case(2):
        sprite.sprite=TwoSpade;
        break;
        case(3):
        sprite.sprite=TriSpade;
        break;
        case(4):
        sprite.sprite=FourSpade;
        break;
        case(5):
        sprite.sprite=FiveSpade;
        break;
        case(6):
        sprite.sprite=SixSpade;
        break;
        case(7):
        sprite.sprite=SevenSpade;
        break;
        case(11):
        sprite.sprite=ElevenSpade;
        break;
        case(12):
        sprite.sprite=TwelveSpade;
        break;
        case(13):
        sprite.sprite=ThirteenSpade;
        break;
      }
    }
      else if (string.Equals(S,"Dinari")){
        switch(Value[I]){
          case(1):
          sprite.sprite=OneDanari;
          break;
          case(2):
          sprite.sprite=TwoDanari;
          break;
          case(3):
          sprite.sprite=TriDanari;
          break;
          case(4):
          sprite.sprite=FourDanari;
          break;
          case(5):
          sprite.sprite=FiveDanari;
          break;
          case(6):
          sprite.sprite=SixDanari;
          break;
          case(7):
          sprite.sprite=SevenDanari;
          break;
          case(11):
          sprite.sprite=ElevenDanari;
          break;
          case(12):
          sprite.sprite=TwelveDanari;
          break;
          case(13):
          sprite.sprite=ThirteenDanari;
          break;
        }
      }
      else if(string.Equals(S,"Coppe")){
        switch(Value[I]){
          case(1):
          sprite.sprite=OneCoppa;
          break;
          case(2):
          sprite.sprite=TwoCoppe;
          break;
          case(3):
          sprite.sprite=TriCoppa;
          break;
          case(4):
          sprite.sprite=FourCoppa;
          break;
          case(5):
          sprite.sprite=FiveCoppa;
          break;
          case(6):
          sprite.sprite=SixCoppa;
          break;
          case(7):
          sprite.sprite=SevenCoppa;
          break;
          case(11):
          sprite.sprite=ElevenCoppa;
          break;
          case(12):
          sprite.sprite=TwelveCoppa;
          break;
          case(13):
          sprite.sprite=ThirteenCoppa;
          break;

        }
      }
      else{
        switch(Value[I]){
          case(1):
          sprite.sprite=OneBastoni;
          break;
          case(2):
          sprite.sprite=TwoBastoni;
          break;
          case(3):
          sprite.sprite=TriBastoni;
          break;
          case(4):
          sprite.sprite=FourBastoni;
          break;
          case(5):
          sprite.sprite=FiveBastoni;
          break;
          case(6):
          sprite.sprite=SixBastoni;
          break;
          case(7):
          sprite.sprite=SevenBastoni;
          break;
          case(11):
          sprite.sprite=ElevenBastoni;
          break;
          case(12):
          sprite.sprite=TwelveBastoni;
          break;
          case(13):
          sprite.sprite=ThirteenBastoni;
          break;

        }

      }
  c.Suit=S;
  c.Value=Value[I];
  c.pointsVal=pointval[I];


  CardObj.transform.SetParent(cardAnchor);
  CardObj.name=c.Suit+" "+c.Value;
 // GOList.Add(cardObj);
   AddBack(c);
   j++;
return;
}
}


[Command]
public void CmdAddToCardsList(GameObject card){
  CardMP c=card.GetComponent<CardMP>();
  cardsList.Add(c);
  
}

private void AddBack(CardMP card){
  _tGO=Instantiate(prefabSprite) as GameObject;
	//NetworkServer.Spawn(_tGO);
  _tSR=_tGO.GetComponent<SpriteRenderer>();
  _tSR.sprite=cardBack;
  _tGO.transform.SetParent(card.transform);
  _tGO.transform.localPosition=Vector3.zero;
  _tSR.sortingOrder=5;
  _tGO.name="back";
  card.back=_tGO;
  card.faceUp=startFaceUp;



}


static public void Shuffle(ref List<CardMP> oCards){
  List<CardMP> tCards=new List<CardMP>();//new temp list
  int ndx;//index of card to be moved
  //repeat while there are cards in original list
  while(oCards.Count > 0){
    //pick index of random card
    ndx=UnityEngine.Random.Range(0,oCards.Count);
    //add to temp list
    tCards.Add(oCards[ndx]);
    //remove from original list
    oCards.RemoveAt(ndx);
  }
  oCards=tCards;

  //because ref means reference paramater the original list is changed aswell
}

[ClientRpc] 
  public void RpcAddCardList(GameObject card){
	if(!isServer){
  CardMP c=card.GetComponent<CardMP>();
  cardsList.Add(c);
}
}
}
