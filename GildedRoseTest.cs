using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static RefactoringKata.Program;

namespace RefactoringKata
{
    [TestFixture]
    public class GildedRoseTest
    {
        private IList<Item> Items;
        
        [TestCase(ELIXIR, 10, 10)]
        [TestCase(DEXTERITY_VEST, 100, 100)]
        public void OrdinaryItem_DayPasses_QualityDecreases(string name, int sellIn, int quality)
        {
            Items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(quality - 1));
        }

        [TestCase(ELIXIR, 10, 0)]
        [TestCase(DEXTERITY_VEST, 100, 0)]
        public void OrdinaryItemWithZeroQuantity_DayPasses_QualityDoesNotDecrease(string name, int sellIn, int quality)
        {
            Items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(0));
        }

        [TestCase(ELIXIR, 0, 10)]
        [TestCase(DEXTERITY_VEST, -10, 20)]
        public void OrdinaryItemAfterSellDate_DayPasses_QualityDecreasesByDouble(string name, int sellIn, int quality)
        {
            Items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(quality - 2));
        }
    }
}
