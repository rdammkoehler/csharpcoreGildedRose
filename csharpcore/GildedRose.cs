using System.Collections.Generic;
using System.Linq;

namespace csharpcore
{
    public class GildedRose
    {
        private const string BackstagePassesToATafkal80EtcConcert = "Backstage passes to a TAFKAL80ETC concert";
        private const string AgedBrie = "Aged Brie";
        private const string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";
        IList<Item> Items;
        private ItemProcessor itemProcessor = new ItemProcessor();

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            Items
                .Where(item => item.Name != SulfurasHandOfRagnaros)
                .ToList()
                .ForEach(currentItem => itemProcessor.UpdateItem(currentItem));
        }

        class ItemProcessor
        {
            private Item currentItem = null;

            public void UpdateItem(Item item)
            {
                currentItem = item;

                PreprocessItemQuality();

                ReduceSellIn();

                PostprocessItemQuality();
            }

            private void PreprocessItemQuality()
            {
                if (IsAgedBrie() || IsConcertTicket())
                {
                    if (IsNotMaxQuality())
                    {
                        ChangeQuality(1);

                        if (IsConcertTicket())
                        {
                            if (currentItem.SellIn < 11)
                            {
                                IncreaseQuality();
                            }

                            if (currentItem.SellIn < 6)
                            {
                                IncreaseQuality();
                            }
                        }
                    }
                }
                else
                {
                    ReduceQuality();
                }
            }

            private void PostprocessItemQuality()
            {
                if (IsExpired())
                {
                    if (IsAgedBrie())
                    {
                        IncreaseQuality();
                    }
                    else
                    {
                        if (IsConcertTicket())
                        {
                            MakeWorthless();
                        }
                        else
                        {
                            ReduceQuality();
                        }
                    }
                }
            }

            private void ReduceSellIn()
            {
                currentItem.SellIn--;
            }

            private void MakeWorthless()
            {
                currentItem.Quality = 0;
            }

            private bool IsConcertTicket()
            {
                return currentItem.Name == BackstagePassesToATafkal80EtcConcert;
            }

            private bool IsAgedBrie()
            {
                return currentItem.Name == AgedBrie;
            }


            private bool IsExpired()
            {
                return currentItem.SellIn < 0;
            }

            private void IncreaseQuality()
            {
                if (IsNotMaxQuality())
                {
                    ChangeQuality(1);
                }
            }

            private void ReduceQuality()
            {
                if (currentItem.Quality > 0)
                {
                    ChangeQuality(-1);
                }
            }

            private void ChangeQuality(int delta)
            {
                currentItem.Quality += delta;
            }

            private bool IsNotMaxQuality()
            {
                return currentItem.Quality < 50;
            }
        }
    }
}