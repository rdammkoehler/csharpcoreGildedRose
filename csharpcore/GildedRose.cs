using System.Collections.Generic;
using System.Linq;

namespace csharpcore
{
    public class GildedRose
    {
        private const string AgedBrie = "Aged Brie";
        private const string BackstagePassesToATafkal80EtcConcert = "Backstage passes to a TAFKAL80ETC concert";
        private const string ConjuredManaCake = "Conjured Mana Cake";
        private const string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";
        private readonly IList<Item> _items;
        private readonly ItemProcessor _itemAdjuster = new ItemProcessor();

        public GildedRose(IList<Item> items)
        {
            this._items = items;
        }

        public void UpdateQuality()
        {
            _items
                .Where(item => item.Name != SulfurasHandOfRagnaros)
                .ToList()
                .ForEach(item => _itemAdjuster.UpdateItem(item));
        }

        private class ItemProcessor
        {
            private Item _currentItem = null;

            public void UpdateItem(Item item)
            {
                _currentItem = item;

                PreProcessItemQuality();

                ReduceSellIn();

                PostProcessItemQuality();
            }

            private void PreProcessItemQuality()
            {
                if (IsAgedBrie() || IsConcertTicket())
                {
                    IncreaseQuality();

                    if (IsConcertTicket())
                    {
                        ApplyPopularDemandQualityModifier();
                    }
                }
                else
                {
                    ReduceQuality();
                    if (IsConjuredManaCake())
                    {
                        ReduceQuality();
                    }
                }
            }

            private void PostProcessItemQuality()
            {
                if (IsExpired())
                {
                    if (IsAgedBrie())
                    {
                        IncreaseQuality();
                    }
                    else if (IsConcertTicket())
                    {
                        MakeWorthless();
                    }
                    else
                    {
                        ReduceQuality();
                    }
                }
            }

            private void ApplyPopularDemandQualityModifier()
            {
                if (IsExpiredInTenDays())
                {
                    IncreaseQuality();
                }

                if (IsExpiredInFiveDays())
                {
                    IncreaseQuality();
                }
            }

            private bool IsExpiredInFiveDays()
            {
                return _currentItem.SellIn < 6;
            }

            private bool IsExpiredInTenDays()
            {
                return _currentItem.SellIn < 11;
            }

            private void ReduceSellIn()
            {
                _currentItem.SellIn--;
            }

            private void MakeWorthless()
            {
                _currentItem.Quality = 0;
            }

            private bool IsConjuredManaCake()
            {
                return _currentItem.Name == ConjuredManaCake;
            }

            private bool IsConcertTicket()
            {
                return _currentItem.Name == BackstagePassesToATafkal80EtcConcert;
            }

            private bool IsAgedBrie()
            {
                return _currentItem.Name == AgedBrie;
            }


            private bool IsExpired()
            {
                return _currentItem.SellIn < 0;
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
                if (IsNotMinQuality())
                {
                    ChangeQuality(-1);
                }
            }

            private void ChangeQuality(int delta)
            {
                _currentItem.Quality += delta;
            }

            private bool IsNotMaxQuality()
            {
                return _currentItem.Quality < 50;
            }

            private bool IsNotMinQuality()
            {
                return _currentItem.Quality > 0;
            }
        }
    }
}