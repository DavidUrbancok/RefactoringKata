using System.Collections.Generic;
using System.Linq;
using static RefactoringKata.SaleItems;

namespace RefactoringKata
{
    public class GildedRose
    {
        private readonly IList<Item> Items;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="items">Items of sale.</param>
        public GildedRose(IList<Item> items)
        {
            Items = items;
        }

        /// <summary>
        /// Updates the quality of all items of sale.
        /// </summary>
        public void UpdateQuality()
        {
            foreach (var item in Items.Where(item => item.Name != SULFURAS))
            {
                UpdateSellInDays(item);

                if (item.Name != AGED_BRIE && item.Name != BACKSTAGE_PASS)
                {
                    if (item.Quality > 0)
                    {
                        DecreaseQuality(item);
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        IncreaseQuality(item);

                        if (item.Name == BACKSTAGE_PASS)
                        {
                            UpdateBackstagePass(item);
                        }
                    }
                }

                if (item.SellIn < 0)
                {
                    if (item.Name != AGED_BRIE)
                    {
                        if (item.Name != BACKSTAGE_PASS)
                        {
                            if (item.Quality > 0)
                            {
                                DecreaseQuality(item);
                            }
                        }
                        else
                        {
                            item.Quality = 0;
                        }
                    }
                    else
                    {
                        if (item.Quality < 50)
                        {
                            IncreaseQuality(item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates the quality of the <paramref name="backstagePass"/>.
        /// </summary>
        /// <param name="backstagePass">Backstage pass.</param>
        private static void UpdateBackstagePass(Item backstagePass)
        {
            if (backstagePass.SellIn < 11)
            {
                if (backstagePass.Quality < 50)
                {
                    IncreaseQuality(backstagePass);
                }
            }

            if (backstagePass.SellIn < 6)
            {
                if (backstagePass.Quality < 50)
                {
                    IncreaseQuality(backstagePass);
                }
            }
        }

        /// <summary>
        /// Updates the sell in days of <paramref name="item"/>.
        /// </summary>
        /// <param name="item">Item of which to update sell in days.</param>
        private static void UpdateSellInDays(Item item)
        {
            if (item.Name != SULFURAS)
            {
                DecreseSellInDays(item);
            }
        }

        /// <summary>
        /// Decreases the number of days to sell in.
        /// </summary>
        /// <param name="item">Item of which to decrease the sell in days.</param>
        private static void DecreseSellInDays(Item item)
        {
            item.SellIn -= 1;
        }

        /// <summary>
        /// Increases the quality of <paramref name="item"/>.
        /// </summary>
        /// <param name="item">Item the quality of which to increase.</param>
        private static void IncreaseQuality(Item item)
        {
            item.Quality += 1;
        }

        /// <summary>
        /// Decreases the quality of <paramref name="item"/>.
        /// </summary>
        /// <param name="item">Item the quality of which to decrease.</param>
        private static void DecreaseQuality(Item item)
        {
            item.Quality -= 1;
        }
    }
}
