namespace RefactoringKata
{
    public class Item
    {
        /// <summary>
        /// Item's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of days left to sell the item.
        /// </summary>
        public int SellIn { get; set; }

        /// <summary>
        /// Remaining quality indicator of an item.
        /// </summary>
        public int Quality { get; set; }
    }
}
