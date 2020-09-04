using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

namespace Tests
{
    public class Test
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestSimplePasses()
        {
            bool isActive = false;

            Assert.AreEqual(false, isActive);
            // Use the Assert class to test conditions
        }
        private Player player2;
        private List<Card> playedcards;
        [SetUp] 
        public void SetUp()
        {
             player2=new Player();
             playedcards=new List<Card>();
        }
        
        
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestWithEnumeratorPasses()
        {
            
            string[] Suit={"Spade","Dinari","Coppe","Bastoni"};
            int[] Value={1,2,3,4,5,6,7,11,12,13};
            int[] pointval={11,0,10,0,0,0,0,2,3,4};
           
          
            

            
            for(int i=0;i<4;i++)
            {
                Card card1=new Card();
                card1.Suit = Suit[0];
                card1.Value = Value[i];
                card1.pointsVal = pointval[i];
                if (i % 2 == 0)
                {
                    card1.state = eCardState.playedbyPlayer;
                }
                else
                {
                    card1.state = eCardState.playedbyAI;
                }
                playedcards.Add(card1);


            }
            WonRound(playedcards);
            Assert.IsTrue(player2.PWinLastRnd);

            
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
        public void WonRound(List<Card> playedCards){
            String firstSuit=playedCards[0].Suit;
            Card max=playedCards[0];
            Card briskula=new Card();
            briskula.Suit = "Coppe";
            briskula.Value = 2;
            briskula.pointsVal = 0;
            for(int j=0;j<4;j++){
                if (string.Equals(playedCards[j].Suit, briskula.Suit))
                {
                    if (max.pointsVal < playedCards[j].pointsVal)
                    {
                        max = playedCards[j];
                    }
                }
                else if (string.Equals(playedCards[j].Suit,firstSuit)){
                    if(max.pointsVal<playedCards[j].pointsVal){
                        max=playedCards[j];
                    }
                }
         
            }
            if(max.state==eCardState.playedbyPlayer){
                Debug.Log(max.pointsVal);

                player2.PWinLastRnd=true;
               

         

            }
            else if (max.state==eCardState.playedbyAI){
            
                Debug.Log(max.pointsVal);

                player2.PWinLastRnd=false;
                
         
            }

            playedCards.Clear();
            

        }

    }
    


   
}



