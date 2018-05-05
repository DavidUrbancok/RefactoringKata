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

                var qualityChangeRate = GetQualityChangeRate(item.SellIn);

                switch (item.Name)
                {
                    case AGED_BRIE:
                    {
                        IncreaseQuality(item, qualityChangeRate);
                        break;
                    }
                    case BACKSTAGE_PASS:
                    {
                        ProcessBackstagePass(item, item.SellIn);
                        break;
                    }
                    case CONJURED_MANA:
                    {
                        DecreaseQuality(item, 2 * qualityChangeRate);
                        break;
                    }
                    default:
                    {
                        DecreaseQuality(item, qualityChangeRate);
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// Returns 2 if the sell in date is less than 0. Otherwise, returns 1.
        /// </summary>
        /// <param name="itemSellIn">Days remaining to sell.</param>
        private static int GetQualityChangeRate(int itemSellIn) => itemSellIn < 0 ? 2 : 1;


        /// <summary>
        /// Processes the backstage pass.
        /// </summary>
        /// <param name="backstagePass">Backstage pass to process.</param>
        /// <param name="sellIn">Number of days left to sell.</param>
        /// 
        private static void ProcessBackstagePass(Item backstagePass, int sellIn)
        {
            if (sellIn < 0)
            {
                backstagePass.Quality = 0;

                return;
            }

            var qualityIncreaseRate = GetQualityIncreaseRate(sellIn);

            IncreaseQuality(backstagePass, qualityIncreaseRate);
        }


        /// <summary>
        /// Returns the quality increase rate based on <paramref name="sellIn"/>.
        /// </summary>
        /// <param name="sellIn">Number of days left to sell.</param>
        private static int GetQualityIncreaseRate(int sellIn)
        {
            if (sellIn < 6)
            {
                return 3;
            }

            return sellIn < 11 ? 2 : 1;
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
        /// <param name="qualityChangeRate">Change rate of item's quality.</param>
        private static void IncreaseQuality(Item item, int qualityChangeRate)
        {
            if (item.Quality < MAX_QUALITY)
            {
                var qualitySum = item.Quality + qualityChangeRate;

                item.Quality = qualitySum > MAX_QUALITY ? MAX_QUALITY : qualitySum;
            }
        }


        /// <summary>
        /// Decreases the quality of <paramref name="item"/>.
        /// </summary>
        /// <param name="item">Item the quality of which to decrease.</param>
        /// <param name="qualityChangeRate">Change rate of item's quality.</param>
        private static void DecreaseQuality(Item item, int qualityChangeRate)
        {
            if (item.Quality > 0)
            {
                item.Quality -= qualityChangeRate;
            }
        }
    }
}
