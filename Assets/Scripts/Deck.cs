using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




 public class Deck : MonoBehaviour {
 public Deck d;
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
public GameObject prefabCard;
public GameObject prefabSprite;
[Header("Set Dynamically")]
public List<Card> cardsList=new List<Card>();
//public List<GameObject> GOList=new List<GameObject>();
void Start(){
  /*d=this;
  List<Card> cardsList=d.GetComponent<Deck>().cardsList;
  if(cardsList.Count==0){
  cardsList=InstantiateCards();*/
}






public List<Card> InstantiateCards(){
  // if(GameObject.GetComponent<Deck>()==null){

  // }


   string[] Suit={"Spade","Dinari","Coppe","Bastoni"};
   int[] Value={1,2,3,4,5,6,7,11,12,13};
   int[] pointval={11,0,10,0,0,0,0,2,3,4};

foreach (string s in Suit){
  for (int i=0;i<Value.Length;i++){
    GameObject cardObj =Instantiate(prefabCard);
    iTween.MoveTo(cardObj, new Vector3(-29, 1, 0), 1);
    iTween.MoveTo(cardObj, new Vector3(-30, 0, 0), 1);
    Card c =cardObj.GetComponent<Card>();
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

  cardObj.transform.SetParent(cardAnchor);
  AddBack(c);
  cardObj.name=c.Suit+" "+c.Value;
 // GOList.Add(cardObj);
  cardsList.Add(c);
}


}
return cardsList;
}



private void AddBack(Card card){
  _tGO=Instantiate(prefabSprite) as GameObject;
  _tSR=_tGO.GetComponent<SpriteRenderer>();
  _tSR.sprite=cardBack;
  _tGO.transform.SetParent(card.transform);
  _tGO.transform.localPosition=Vector3.zero;
  _tSR.sortingOrder=5;
  _tGO.name="back";
  card.back=_tGO;
  card.faceUp=startFaceUp;



}


static public void Shuffle(ref List<Card> oCards){
  List<Card> tCards=new List<Card>();//new temp list
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
}
