using System.Collections.Generic;

namespace csharpcore
{
    public class GildedRose
    {
        private const string BackstagePassesToATafkal80EtcConcert = "Backstage passes to a TAFKAL80ETC concert";
        private const string AgedBrie = "Aged Brie";
        private const string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";
        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var idx = 0; idx < Items.Count; idx++)
            {
                Item currentItem = Items[idx];
                if (currentItem.Name == SulfurasHandOfRagnaros)
                {
                }
                else
                {
                    if (currentItem.Name == AgedBrie || currentItem.Name == BackstagePassesToATafkal80EtcConcert)
                    {
                        if (currentItem.Quality < 50)
                        {
                            currentItem.Quality++;

                            if (currentItem.Name == BackstagePassesToATafkal80EtcConcert)
                            {
                                if (currentItem.SellIn < 11)
                                {
                                    if (currentItem.Quality < 50)
                                    {
                                        currentItem.Quality++;
                                    }
                                }

                                if (currentItem.SellIn < 6)
                                {
                                    if (currentItem.Quality < 50)
                                    {
                                        currentItem.Quality++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (currentItem.Quality > 0)
                        {
                            currentItem.Quality--;
                        }
                    }


                    currentItem.SellIn--;

                    if (currentItem.SellIn < 0)
                    {
                        if (currentItem.Name == AgedBrie)
                        {
                            if (currentItem.Quality < 50)
                            {
                                currentItem.Quality++;
                            }
                        }
                        else
                        {
                            if (currentItem.Name == BackstagePassesToATafkal80EtcConcert)
                            {
                                currentItem.Quality = 0;
                            }
                            else
                            {
                                if (currentItem.Quality > 0)
                                {
                                    currentItem.Quality--;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}