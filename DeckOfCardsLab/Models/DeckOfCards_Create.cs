namespace DeckOfCardsLab.Models
{
    public class DeckOfCards_Create
    {
        public bool sucess { get; set; }
        public string deck_id { get; set; }
        public bool shuffled { get; set; }
        public int remaining { get; set; }
    }
}