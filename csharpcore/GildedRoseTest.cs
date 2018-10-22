using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace csharpcore
{
    public class ItemComparer : IEqualityComparer<Item>
    {
        public bool Equals(Item x, Item y)
        {
            return x.Name == y.Name &&
                   x.Quality == y.Quality &&
                   x.SellIn == y.SellIn;
        }

        public int GetHashCode(Item obj)
        {
            return obj.GetHashCode();
        }
    }

    public class GildedRoseTest
    {
        private const string AgedBrie = "Aged Brie";
        private const string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";
        private const string BackstagePassesToATafkal80EtcConcert = "Backstage passes to a TAFKAL80ETC concert";
        private GildedRose App { get; set; }
        private Item[] Items { get; set; }

        private void Initialize(params Item[] Items)
        {
            this.Items = Items;
            App = new GildedRose(Items);
        }

        [Fact]
        public void ctQualityCanBeNegativeIfSellInGreaterThanZero()
        {
            Initialize(new Item {Name = "foo", SellIn = 10, Quality = -1});

            App.UpdateQuality();

            Assert.Equal(-1, Items[0].Quality);
        }

        [Fact]
        public void ctQualityDecreasesByOneEachUpdate()
        {
            Initialize(new Item {Name = "foo", SellIn = 10, Quality = 5});

            App.UpdateQuality();

            Assert.Equal(4, Items[0].Quality);
        }

        [Fact]
        public void ctQualityDoesNotDecreaseBelowZero()
        {
            Initialize(new Item {Name = "foo", SellIn = 10, Quality = 0});

            App.UpdateQuality();

            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void ctSellInReducesByOneForEachUpdate()
        {
            Initialize(new Item {Name = "foo", SellIn = 10, Quality = 10});

            App.UpdateQuality();

            Assert.Equal(9, Items[0].SellIn);
        }

        [Fact]
        public void ctAllItemsGetProcessed()
        {
            Initialize(
                new Item {Name = "foo", SellIn = 10, Quality = 10},
                new Item {Name = "bar", SellIn = 9, Quality = 9}
            );
            var expected = new Item[]
            {
                new Item() {Name = "foo", SellIn = 9, Quality = 9},
                new Item {Name = "bar", SellIn = 8, Quality = 8}
            };

            App.UpdateQuality();

            Assert.Equal(expected, Items, new ItemComparer());
        }

        [Fact]
        public void ctQualityDegradesTwiceAsFastAfterSellInDate()
        {
            Initialize(new Item {Name = "foo", SellIn = 0, Quality = 10});

            App.UpdateQuality();

            Assert.Equal(8, Items[0].Quality);
        }

        [Fact]
        public void ctAgedBrieIncreasesInQualityWithAge()
        {
            Initialize(new Item {Name = AgedBrie, SellIn = 10, Quality = 10});

            App.UpdateQuality();

            Assert.Equal(11, Items[0].Quality);
        }

        [Fact]
        public void ctAgedBrieNeverGetsQualityMoreThan50()
        {
            Initialize(new Item {Name = AgedBrie, SellIn = 10, Quality = 50});

            App.UpdateQuality();

            Assert.Equal(50, Items[0].Quality);
        }

        [Fact]
        public void ctSulfurasNeverHasToBeSold()
        {
            Initialize(new Item {Name = SulfurasHandOfRagnaros, SellIn = 1, Quality = 80});
            var expected = new Item {Name = SulfurasHandOfRagnaros, SellIn = 1, Quality = 80};

            App.UpdateQuality();

            Assert.Equal(expected, Items[0], new ItemComparer());
        }

        [Fact]
        public void ctBackstagePassesIncreaseInQuality()
        {
            Initialize(new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 30, Quality = 40});
            var expected = new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 29, Quality = 41};

            App.UpdateQuality();

            Assert.Equal(expected, Items[0], new ItemComparer());
        }

        [Fact]
        public void ctBackstagePassesCannotHaveQualityOver50()
        {
            Initialize(new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 3, Quality = 50});
            var expected = new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 2, Quality = 50};

            App.UpdateQuality();

            Assert.Equal(expected, Items[0], new ItemComparer());
        }

        [Fact]
        public void ctBackstagePassesIncreaseInQualityByTwoWhen10DaysFromSellIn()
        {
            Initialize(new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 10, Quality = 40});
            var expected = new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 9, Quality = 42};

            App.UpdateQuality();

            Assert.Equal(expected, Items[0], new ItemComparer());
        }

        [Fact]
        public void ctBackstagePassesIncreaseInQualityByThreeWhen5DaysFromSellIn()
        {
            Initialize(new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 5, Quality = 40});
            var expected = new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 4, Quality = 43};

            App.UpdateQuality();

            Assert.Equal(expected, Items[0], new ItemComparer());
        }

        [Fact]
        public void ctBackstagePassesQualityDropsToZeroAfterSellIn()
        {
            Initialize(new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 0, Quality = 40});
            var expected = new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = -1, Quality = 0};

            App.UpdateQuality();

            Assert.Equal(expected, Items[0], new ItemComparer());
        }

        [Fact]
        public void ctBackstagePassesCannotExceedQualityOf50EvenWhen10DaysFromSellIn()
        {
            Initialize(new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 10, Quality = 49});
            var expected = new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 9, Quality = 50};

            App.UpdateQuality();

            Assert.Equal(expected, Items[0], new ItemComparer());
        }

        [Fact]
        public void ctBackstagePassesCannotExceedQualityOf50EvenWhen5DaysFromSellIn()
        {
            Initialize(new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 5, Quality = 49});
            var expected = new Item {Name = BackstagePassesToATafkal80EtcConcert, SellIn = 4, Quality = 50};

            App.UpdateQuality();

            Assert.Equal(expected, Items[0], new ItemComparer());
        }

        [Fact]
        public void ctSulfurasSellInDoesNotChange()
        {
            Initialize(new Item {Name = SulfurasHandOfRagnaros, SellIn = 1, Quality = 80});

            App.UpdateQuality();

            Assert.Equal(1, Items[0].SellIn);
        }

        [Fact]
        public void ctSulfurasQualityDoesNotChange()
        {
            Initialize(new Item {Name = SulfurasHandOfRagnaros, SellIn = 1, Quality = 200});

            App.UpdateQuality();

            Assert.Equal(200, Items[0].Quality);
        }
    }
}