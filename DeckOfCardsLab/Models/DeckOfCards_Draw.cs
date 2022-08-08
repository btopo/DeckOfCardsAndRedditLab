namespace DeckOfCardsLab.Models
{
    public class DeckOfCards_Draw
    {
        public bool success { get; set; }
        public string deck_id { get; set; }

        public int remaining { get; set; }

        public DeckOfCards_Draw_Card[] cards { get; set; }
    }
}