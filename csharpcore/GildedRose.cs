﻿using System.Collections.Generic;
using System.Linq;

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
            var nonLegendaryItems = Items.Where(item => item.Name != SulfurasHandOfRagnaros);
            foreach (Item currentItem in nonLegendaryItems)
            {
                PreprocessItemQuality(currentItem);

                currentItem.SellIn--;

                PostprocessItemQuality(currentItem);
            }
        }

        private void PreprocessItemQuality(Item currentItem)
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
        }

        private void PostprocessItemQuality(Item currentItem)
        {
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