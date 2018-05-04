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
            Items = new List<Item> {new Item {Name = name, SellIn = sellIn, Quality = quality}};

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(quality - 1));
        }

        [TestCase(ELIXIR, 10, 0)]
        [TestCase(DEXTERITY_VEST, 100, 0)]
        public void OrdinaryItemWithZeroQuantity_DayPasses_QualityDoesNotDecrease(string name, int sellIn, int quality)
        {
            Items = new List<Item> {new Item {Name = name, SellIn = sellIn, Quality = quality}};

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(0));
        }

        [TestCase(ELIXIR, 0, 10)]
        [TestCase(DEXTERITY_VEST, -10, 20)]
        public void OrdinaryItemAfterSellDate_DayPasses_QualityDecreasesByDouble(string name, int sellIn, int quality)
        {
            Items = new List<Item> {new Item {Name = name, SellIn = sellIn, Quality = quality}};

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(quality - 2));
        }

        [Test]
        public void AgedBrie_DayPasses_QualityDecreases()
        {
            Items = new List<Item> {new Item {Name = AGED_BRIE, SellIn = 10, Quality = 10}};

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(11));
        }

        [Test]
        public void AgedBrieWithMaximumQuantity_DayPasses_QualityDoesNotIncrease()
        {
            Items = new List<Item> {new Item {Name = AGED_BRIE, SellIn = 100, Quality = 50}};

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(50));
        }

        [Test]
        public void Sulfuras_DayPasses_QualityDoesNotChange()
        {
            Items = new List<Item> {new Item {Name = SULFURAS, SellIn = 10, Quality = 10}};

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(10));
        }

        [Test]
        public void Sulfuras_DayPasses_SellInDoesNotChange()
        {
            Items = new List<Item> {new Item {Name = SULFURAS, SellIn = 10, Quality = 10}};

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().SellIn, Is.EqualTo(10));
        }

        [Test]
        public void BackstagePass_MoreThan10DaysRemaining_DayPasses_QualityIncreasesByOne()
        {
            Items = new List<Item> {new Item {Name = BACKSTAGE_PASS, SellIn = 20, Quality = 10}};

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(11));
        }

        [Test]
        public void BackstagePass_Between10And5DaysRemaining_DayPasses_QualityIncreasesByTwo()
        {
            Items = new List<Item> {new Item {Name = BACKSTAGE_PASS, SellIn = 7, Quality = 10}};

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(12));
        }

        [Test]
        public void BackstagePass_LessThan5DaysRemaining_DayPasses_QualityIncreasesByThree()
        {
            Items = new List<Item> {new Item {Name = BACKSTAGE_PASS, SellIn = 2, Quality = 10}};

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(13));
        }

        [Test]
        public void BackstagePass_AfterSellDate_DayPasses_QualityIsZero()
        {
            Items = new List<Item> { new Item { Name = BACKSTAGE_PASS, SellIn = 0, Quality = 10 } };

            var app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.That(Items.First().Quality, Is.EqualTo(0));
        }
    }
}
