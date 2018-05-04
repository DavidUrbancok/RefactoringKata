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
                        ProcessBackstagePass(item, qualityChangeRate);
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
        /// <param name="backstagePass">Backstagepass to process.</param>
        /// <param name="qualityChangeRate">Change rate of item's quality.</param>
        /// 
        private static void ProcessBackstagePass(Item backstagePass, int qualityChangeRate)
        {
            if (qualityChangeRate == 2)
            {
                backstagePass.Quality = 0;
            }
            else
            {
                IncreaseQuality(backstagePass, 1);
                UpdateBackstagePass(backstagePass);
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
                    IncreaseQuality(backstagePass, 1);
                }

                if (backstagePass.SellIn < 6)
                {
                    IncreaseQuality(backstagePass, 1);
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
        /// <param name="qualityChangeRate">Change rate of item's quality.</param>
        private static void IncreaseQuality(Item item, int qualityChangeRate)
        {
            if (item.Quality < 50)
            {
                item.Quality += qualityChangeRate;
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
