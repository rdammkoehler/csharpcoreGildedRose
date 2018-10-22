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
                if (isNotMaxQuality(currentItem))
                {
                    currentItem.Quality++;

                    if (currentItem.Name == BackstagePassesToATafkal80EtcConcert)
                    {
                        if (currentItem.SellIn < 11)
                        {
                            IncreaseQuality(currentItem);
                        }

                        if (currentItem.SellIn < 6)
                        {
                            IncreaseQuality(currentItem);
                        }
                    }
                }
            }
            else
            {
                ReduceQuality(currentItem);
            }
        }

        private void PostprocessItemQuality(Item currentItem)
        {
            if (isExpired(currentItem))
            {
                if (currentItem.Name == AgedBrie)
                {
                    IncreaseQuality(currentItem);
                }
                else
                {
                    if (currentItem.Name == BackstagePassesToATafkal80EtcConcert)
                    {
                        currentItem.Quality = 0;
                    }
                    else
                    {
                        ReduceQuality(currentItem);
                    }
                }
            }
        }

        private bool isExpired(Item currentItem)
        {
            return currentItem.SellIn < 0;
        }

        private void IncreaseQuality(Item currentItem)
        {
            if (isNotMaxQuality(currentItem))
            {
                currentItem.Quality++;
            }
        }

        private void ReduceQuality(Item currentItem)
        {
            if (currentItem.Quality > 0)
            {
                currentItem.Quality--;
            }
        }

        private bool isNotMaxQuality(Item currentItem)
        {
            return currentItem.Quality < 50;
        }
    }
}