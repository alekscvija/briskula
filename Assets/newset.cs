using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newset : MonoBehaviour
{
    // Start is called before the first frame update
    public void newSet()
    {
        if (Briscola.B.playingSpace.Count == 4)
        {
            Briscola.B.WonRound(Briscola.B.playingSpace);
            if (Briscola.B.deck.cardsList.Count != 0)
            {
                Briscola.B.newSet(Briscola.B.deck.cardsList);
            }
            else if (Briscola.B.deck.cardsList.Count == 0 & Briscola.B.player1.cardArr.Count==0 & Briscola.B.ai.compSet.Count==0)
            {
                bool winner=Briscola.B.calcWinner(Briscola.B.player1.pointDeck, Briscola.B.ai.pointDeck1);
                Briscola.B.GameOver(winner);

            }
        }
        
    }
}
