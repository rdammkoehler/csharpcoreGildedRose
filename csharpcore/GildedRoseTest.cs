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
    }
}