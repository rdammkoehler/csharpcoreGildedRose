using Xunit;
using System.Collections.Generic;

namespace csharpcore
{
    public class GildedRoseTest
    {
        [Fact]
        public void ctQualityCanBeNegativeIfSellInGreaterThanZero()
        {
            IList<Item> Items = new List<Item> {new Item {Name = "foo", SellIn = 10, Quality = -1}};
            GildedRose app = new GildedRose(Items);
            
            app.UpdateQuality();
            
            Assert.Equal(-1, Items[0].Quality);
        }
    }
}