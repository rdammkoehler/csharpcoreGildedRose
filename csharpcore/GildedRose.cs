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

                ReduceSellIn(currentItem);

                PostprocessItemQuality(currentItem);
            }
        }

        private void PreprocessItemQuality(Item currentItem)
        {
            if (IsAgedBrie(currentItem) || IsConcertTicket(currentItem))
            {
                if (IsNotMaxQuality(currentItem))
                {
                    ChangeQuality(currentItem, 1);

                    if (IsConcertTicket(currentItem))
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
            if (IsExpired(currentItem))
            {
                if (IsAgedBrie(currentItem))
                {
                    IncreaseQuality(currentItem);
                }
                else
                {
                    if (IsConcertTicket(currentItem))
                    {
                        MakeWorthless(currentItem);
                    }
                    else
                    {
                        ReduceQuality(currentItem);
                    }
                }
            }
        }

        private void ReduceSellIn(Item currentItem)
        {
            currentItem.SellIn--;
        }

        private void MakeWorthless(Item currentItem)
        {
            currentItem.Quality = 0;
        }

        private bool IsConcertTicket(Item currentItem)
        {
            return currentItem.Name == BackstagePassesToATafkal80EtcConcert;
        }

        private bool IsAgedBrie(Item currentItem)
        {
            return currentItem.Name == AgedBrie;
        }


        private bool IsExpired(Item currentItem)
        {
            return currentItem.SellIn < 0;
        }

        private void IncreaseQuality(Item currentItem)
        {
            if (IsNotMaxQuality(currentItem))
            {
                ChangeQuality(currentItem, 1);
            }
        }

        private void ReduceQuality(Item currentItem)
        {
            if (currentItem.Quality > 0)
            {
                ChangeQuality(currentItem, -1);
            }
        }

        private void ChangeQuality(Item currentItem, int delta)
        {
            currentItem.Quality += delta;
        }

        private bool IsNotMaxQuality(Item currentItem)
        {
            return currentItem.Quality < 50;
        }
    }
}