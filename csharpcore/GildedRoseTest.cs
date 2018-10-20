using Xunit;
using System.Collections.Generic;

namespace csharpcore
{
    public class GildedRoseTest
    {
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
    }
}