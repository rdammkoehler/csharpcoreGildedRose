using System.Collections.Generic;

namespace csharpcore
{
    public class GildedRose
    {
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
                if (currentItem.Name == "Sulfuras, Hand of Ragnaros")
                {
                }
                else
                {
                    if (currentItem.Name == "Aged Brie" || currentItem.Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (currentItem.Quality < 50)
                        {
                            currentItem.Quality++;

                            if (currentItem.Name == "Backstage passes to a TAFKAL80ETC concert")
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
                        if (currentItem.Name == "Aged Brie")
                        {
                            if (currentItem.Quality < 50)
                            {
                                currentItem.Quality++;
                            }
                        }
                        else
                        {
                            if (currentItem.Name == "Backstage passes to a TAFKAL80ETC concert")
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