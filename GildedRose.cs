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
                DecreseSellInDays(item);

                var isAfterSellInDate = item.SellIn < 0;

                switch (item.Name)
                {
                    case AGED_BRIE:
                    {
                        IncreaseQuality(item, isAfterSellInDate);
                        break;
                    }
                    case BACKSTAGE_PASS:
                    {
                        ProcessBackstagePass(item, isAfterSellInDate);
                        break;
                    }
                    default:
                    {
                        DecreaseQuality(item, isAfterSellInDate);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isAfterSellInDate"></param>
        private static void ProcessBackstagePass(Item item, bool isAfterSellInDate)
        {
            if (isAfterSellInDate)
            {
                item.Quality = 0;
            }
            else
            {
                IncreaseQuality(item);
                UpdateBackstagePass(item);
            }
        }

        /// <summary>
        /// Updates the quality of the <paramref name="backstagePass"/>.
        /// </summary>
        /// <param name="backstagePass">Backstage pass.</param>
        private static void UpdateBackstagePass(Item backstagePass)
        {
            if (backstagePass.Quality < 50)
            {
                if (backstagePass.SellIn < 11)
                {
                    IncreaseQuality(backstagePass);
                }

                if (backstagePass.SellIn < 6)
                {
                    IncreaseQuality(backstagePass);
                }
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
        /// <param name="isAfterSellInDate"></param>
        private static void IncreaseQuality(Item item, bool isAfterSellInDate = false)
        {
            if (item.Quality < 50)
            {
                if (isAfterSellInDate)
                {
                    item.Quality += 2;
                }
                else
                {
                    item.Quality += 1;
                }
            }
        }

        /// <summary>
        /// Decreases the quality of <paramref name="item"/>.
        /// </summary>
        /// <param name="item">Item the quality of which to decrease.</param>
        /// <param name="isAfterSellInDate"></param>
        private static void DecreaseQuality(Item item, bool isAfterSellInDate = false)
        {
            if (item.Quality > 0)
            {
                if (isAfterSellInDate)
                {
                    item.Quality -= 2;
                }
                else
                {
                    item.Quality -= 1;
                }
            }
        }
    }
}
