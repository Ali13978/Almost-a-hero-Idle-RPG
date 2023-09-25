using System;
using System.Collections.Generic;

namespace Simulation
{
	public class CalendarTreeOfferBundle
	{
		public static CalendarTreeOfferBundle CreateChristmasBundle()
		{
			CalendarTreeOfferBundle calendarTreeOfferBundle = new CalendarTreeOfferBundle();
			calendarTreeOfferBundle.tree = new List<List<CalendarTreeOfferNode>>();
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[0].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.GEM,
					purchasesLeft = 1
				},
				offerAmount = 25.0,
				offerCost = 500.0,
				dependencies = new KeyValuePair<int, int>[0]
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[1].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "BLIZZARD",
					purchasesLeft = 1
				},
				offerAmount = 3.0,
				offerCost = 500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(0, 0)
				}
			});
			calendarTreeOfferBundle.tree[1].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.TOKEN,
					purchasesLeft = 1
				},
				offerAmount = 500.0,
				offerCost = 500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(0, 0)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[2].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.TOKEN,
					purchasesLeft = 1
				},
				offerAmount = 1000.0,
				offerCost = 1500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(1, 0)
				}
			});
			calendarTreeOfferBundle.tree[2].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.SCRAP,
					purchasesLeft = 1
				},
				offerAmount = 250.0,
				offerCost = 1000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(1, 0),
					new KeyValuePair<int, int>(1, 1)
				}
			});
			calendarTreeOfferBundle.tree[2].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "HOT_COCOA",
					purchasesLeft = 1
				},
				offerAmount = 3.0,
				offerCost = 1000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(1, 1)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[3].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "HOT_COCOA",
					purchasesLeft = 1
				},
				offerAmount = 5.0,
				offerCost = 2000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(2, 0)
				}
			});
			calendarTreeOfferBundle.tree[3].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "ORNAMENT_DROP",
					purchasesLeft = 1
				},
				offerAmount = 3.0,
				offerCost = 1000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(2, 0),
					new KeyValuePair<int, int>(2, 1)
				}
			});
			calendarTreeOfferBundle.tree[3].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.GEM,
					purchasesLeft = 1
				},
				offerAmount = 50.0,
				offerCost = 1500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(2, 1),
					new KeyValuePair<int, int>(2, 2)
				}
			});
			calendarTreeOfferBundle.tree[3].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.TOKEN,
					purchasesLeft = 1
				},
				offerAmount = 1500.0,
				offerCost = 2500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(2, 2)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[4].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.GEM,
					purchasesLeft = 1
				},
				offerAmount = 75.0,
				offerCost = 2000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(3, 0),
					new KeyValuePair<int, int>(3, 1)
				}
			});
			calendarTreeOfferBundle.tree[4].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.TOKEN,
					purchasesLeft = 1
				},
				offerAmount = 2000.0,
				offerCost = 3500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(3, 1),
					new KeyValuePair<int, int>(3, 2)
				}
			});
			calendarTreeOfferBundle.tree[4].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.COSTUME,
					genericIntId = HeroSkins.VEXX_CHRISTMAS,
					purchasesLeft = 1
				},
				offerAmount = 1.0,
				offerCost = 6000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(3, 2),
					new KeyValuePair<int, int>(3, 3)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[5].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.TOKEN,
					purchasesLeft = 1
				},
				offerAmount = 2500.0,
				offerCost = 4000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(4, 0)
				}
			});
			calendarTreeOfferBundle.tree[5].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.SCRAP,
					purchasesLeft = 1
				},
				offerAmount = 500.0,
				offerCost = 2500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(4, 0),
					new KeyValuePair<int, int>(4, 1)
				}
			});
			calendarTreeOfferBundle.tree[5].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.GEM,
					purchasesLeft = 1
				},
				offerAmount = 100.0,
				offerCost = 3000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(4, 1),
					new KeyValuePair<int, int>(4, 2)
				}
			});
			calendarTreeOfferBundle.tree[5].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "BLIZZARD",
					purchasesLeft = 1
				},
				offerAmount = 5.0,
				offerCost = 1000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(4, 2)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[6].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "HOT_COCOA",
					purchasesLeft = 1
				},
				offerAmount = 7.0,
				offerCost = 2500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(5, 0),
					new KeyValuePair<int, int>(5, 1)
				}
			});
			calendarTreeOfferBundle.tree[6].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "BLIZZARD",
					purchasesLeft = 1
				},
				offerAmount = 7.0,
				offerCost = 1500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(5, 1),
					new KeyValuePair<int, int>(5, 2)
				}
			});
			calendarTreeOfferBundle.tree[6].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "ORNAMENT_DROP",
					purchasesLeft = 1
				},
				offerAmount = 5.0,
				offerCost = 1500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(5, 2),
					new KeyValuePair<int, int>(5, 3)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[7].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "BLIZZARD",
					purchasesLeft = 1
				},
				offerAmount = 9.0,
				offerCost = 2000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(6, 0)
				}
			});
			calendarTreeOfferBundle.tree[7].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "ORNAMENT_DROP",
					purchasesLeft = 1
				},
				offerAmount = 7.0,
				offerCost = 2000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(6, 0),
					new KeyValuePair<int, int>(6, 1)
				}
			});
			calendarTreeOfferBundle.tree[7].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.SCRAP,
					purchasesLeft = 1
				},
				offerAmount = 750.0,
				offerCost = 3500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(6, 1),
					new KeyValuePair<int, int>(6, 2)
				}
			});
			calendarTreeOfferBundle.tree[7].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.GEM,
					purchasesLeft = 1
				},
				offerAmount = 150.0,
				offerCost = 4000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(6, 2)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[8].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.COSTUME,
					genericIntId = HeroSkins.V_CHRISTMAS,
					purchasesLeft = 1
				},
				offerAmount = 1.0,
				offerCost = 7000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(7, 0),
					new KeyValuePair<int, int>(7, 1)
				}
			});
			calendarTreeOfferBundle.tree[8].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "HOT_COCOA",
					purchasesLeft = 1
				},
				offerAmount = 9.0,
				offerCost = 3500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(7, 1),
					new KeyValuePair<int, int>(7, 2)
				}
			});
			calendarTreeOfferBundle.tree[8].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.TOKEN,
					purchasesLeft = 1
				},
				offerAmount = 3500.0,
				offerCost = 5500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(7, 2),
					new KeyValuePair<int, int>(7, 3)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[9].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.GEM,
					purchasesLeft = 1
				},
				offerAmount = 200.0,
				offerCost = 5000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(8, 0)
				}
			});
			calendarTreeOfferBundle.tree[9].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.TOKEN,
					purchasesLeft = 1
				},
				offerAmount = 5000.0,
				offerCost = 7500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(8, 0),
					new KeyValuePair<int, int>(8, 1)
				}
			});
			calendarTreeOfferBundle.tree[9].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.SCRAP,
					purchasesLeft = 1
				},
				offerAmount = 1000.0,
				offerCost = 4500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(8, 1),
					new KeyValuePair<int, int>(8, 2)
				}
			});
			calendarTreeOfferBundle.tree[9].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "ORNAMENT_DROP",
					purchasesLeft = 1
				},
				offerAmount = 9.0,
				offerCost = 2500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(8, 2)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[10].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "BLIZZARD",
					purchasesLeft = 1
				},
				offerAmount = 12.0,
				offerCost = 3000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(9, 0),
					new KeyValuePair<int, int>(9, 1)
				}
			});
			calendarTreeOfferBundle.tree[10].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "ORNAMENT_DROP",
					purchasesLeft = 1
				},
				offerAmount = 12.0,
				offerCost = 3500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(9, 1),
					new KeyValuePair<int, int>(9, 2)
				}
			});
			calendarTreeOfferBundle.tree[10].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.GEM,
					purchasesLeft = 1
				},
				offerAmount = 250.0,
				offerCost = 6000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(9, 2),
					new KeyValuePair<int, int>(9, 3)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[11].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "HOT_COCOA",
					purchasesLeft = 1
				},
				offerAmount = 12.0,
				offerCost = 4500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(10, 0)
				}
			});
			calendarTreeOfferBundle.tree[11].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.GEM,
					purchasesLeft = 1
				},
				offerAmount = 300.0,
				offerCost = 7000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(10, 0),
					new KeyValuePair<int, int>(10, 1)
				}
			});
			calendarTreeOfferBundle.tree[11].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.COSTUME,
					genericIntId = HeroSkins.LENNY_CHRISTMAS,
					purchasesLeft = 1
				},
				offerAmount = 1.0,
				offerCost = 8000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(10, 1),
					new KeyValuePair<int, int>(10, 2)
				}
			});
			calendarTreeOfferBundle.tree[11].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.SCRAP,
					purchasesLeft = 1
				},
				offerAmount = 1500.0,
				offerCost = 6000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(10, 2)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[12].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "ORNAMENT_DROP",
					purchasesLeft = 1
				},
				offerAmount = 15.0,
				offerCost = 4500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(11, 0),
					new KeyValuePair<int, int>(11, 1)
				}
			});
			calendarTreeOfferBundle.tree[12].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.TOKEN,
					purchasesLeft = 1
				},
				offerAmount = 6000.0,
				offerCost = 8000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(11, 1),
					new KeyValuePair<int, int>(11, 2)
				}
			});
			calendarTreeOfferBundle.tree[12].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "BLIZZARD",
					purchasesLeft = 1
				},
				offerAmount = 15.0,
				offerCost = 3500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(11, 2),
					new KeyValuePair<int, int>(11, 3)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[13].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.COSTUME,
					genericIntId = HeroSkins.BABU_CHRISTMAS,
					purchasesLeft = 1
				},
				offerAmount = 1.0,
				offerCost = 9000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(12, 0)
				}
			});
			calendarTreeOfferBundle.tree[13].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.SCRAP,
					purchasesLeft = 1
				},
				offerAmount = 2000.0,
				offerCost = 7500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(12, 0),
					new KeyValuePair<int, int>(12, 1)
				}
			});
			calendarTreeOfferBundle.tree[13].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "HOT_COCOA",
					purchasesLeft = 1
				},
				offerAmount = 15.0,
				offerCost = 5500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(12, 1),
					new KeyValuePair<int, int>(12, 2)
				}
			});
			calendarTreeOfferBundle.tree[13].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.GEM,
					purchasesLeft = 1
				},
				offerAmount = 400.0,
				offerCost = 8500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(12, 2)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[14].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.GEM,
					purchasesLeft = 1
				},
				offerAmount = 450.0,
				offerCost = 9500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(13, 0),
					new KeyValuePair<int, int>(13, 1)
				}
			});
			calendarTreeOfferBundle.tree[14].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.TOKEN,
					purchasesLeft = 1
				},
				offerAmount = 8000.0,
				offerCost = 9500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(13, 1),
					new KeyValuePair<int, int>(13, 2)
				}
			});
			calendarTreeOfferBundle.tree[14].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.SCRAP,
					purchasesLeft = 1
				},
				offerAmount = 3000.0,
				offerCost = 10500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(13, 2),
					new KeyValuePair<int, int>(13, 3)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[15].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.SCRAP,
					purchasesLeft = 1
				},
				offerAmount = 4000.0,
				offerCost = 13500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(14, 0)
				}
			});
			calendarTreeOfferBundle.tree[15].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "BLIZZARD",
					purchasesLeft = 1
				},
				offerAmount = 20.0,
				offerCost = 4500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(14, 0),
					new KeyValuePair<int, int>(14, 1)
				}
			});
			calendarTreeOfferBundle.tree[15].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.GEM,
					purchasesLeft = 1
				},
				offerAmount = 500.0,
				offerCost = 10000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(14, 1),
					new KeyValuePair<int, int>(14, 2)
				}
			});
			calendarTreeOfferBundle.tree[15].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.COSTUME,
					genericIntId = HeroSkins.WARLOCK_CHRISTMAS,
					purchasesLeft = 1
				},
				offerAmount = 1.0,
				offerCost = 10500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(14, 2)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[16].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.COSTUME,
					genericIntId = HeroSkins.DRUID_CHRISTMAS,
					purchasesLeft = 1
				},
				offerAmount = 1.0,
				offerCost = 11000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(15, 0),
					new KeyValuePair<int, int>(15, 1)
				}
			});
			calendarTreeOfferBundle.tree[16].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.TOKEN,
					purchasesLeft = 1
				},
				offerAmount = 10000.0,
				offerCost = 10500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(15, 1),
					new KeyValuePair<int, int>(15, 2)
				}
			});
			calendarTreeOfferBundle.tree[16].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "ORNAMENT_DROP",
					purchasesLeft = 1
				},
				offerAmount = 20.0,
				offerCost = 5500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(15, 2),
					new KeyValuePair<int, int>(15, 3)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[17].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.MERCHANT_ITEM,
					genericStringId = "HOT_COCOA",
					purchasesLeft = 1
				},
				offerAmount = 20.0,
				offerCost = 6500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(16, 0),
					new KeyValuePair<int, int>(16, 1)
				}
			});
			calendarTreeOfferBundle.tree[17].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.SCRAP,
					purchasesLeft = 1
				},
				offerAmount = 5000.0,
				offerCost = 15000.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(16, 1),
					new KeyValuePair<int, int>(16, 2)
				}
			});
			calendarTreeOfferBundle.tree.Add(new List<CalendarTreeOfferNode>());
			calendarTreeOfferBundle.tree[18].Add(new CalendarTreeOfferNode
			{
				offer = new FlashOffer
				{
					isCrhistmas = true,
					costType = FlashOffer.CostType.CANDY,
					type = FlashOffer.Type.COSTUME,
					genericIntId = HeroSkins.HILT_CHRISTMAS,
					purchasesLeft = 1
				},
				offerAmount = 1.0,
				offerCost = 12500.0,
				dependencies = new KeyValuePair<int, int>[]
				{
					new KeyValuePair<int, int>(17, 0),
					new KeyValuePair<int, int>(17, 1)
				}
			});
			return calendarTreeOfferBundle;
		}

		public void InitTreeState(List<List<int>> purchasesLeftByOffer)
		{
			if (purchasesLeftByOffer != null)
			{
				int i = 0;
				int count = this.tree.Count;
				while (i < count)
				{
					int j = 0;
					int count2 = this.tree[i].Count;
					while (j < count2)
					{
						this.tree[i][j].offer.purchasesLeft = purchasesLeftByOffer[i][j];
						j++;
					}
					i++;
				}
			}
		}

		public bool IsOfferUnlockedInTree(int row, int offerIndex)
		{
			if (this.tree[row][offerIndex].dependencies == null || this.tree[row][offerIndex].dependencies.Length == 0)
			{
				return true;
			}
			for (int i = 0; i < this.tree[row][offerIndex].dependencies.Length; i++)
			{
				KeyValuePair<int, int> keyValuePair = this.tree[row][offerIndex].dependencies[i];
				if (this.tree[keyValuePair.Key][keyValuePair.Value].offer.purchasesLeft == 0)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsTreeFullyPurchased()
		{
			for (int i = this.tree.Count - 1; i >= 0; i--)
			{
				for (int j = this.tree[i].Count - 1; j >= 0; j--)
				{
					if (this.tree[i][j].offer.purchasesLeft > 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		public List<List<CalendarTreeOfferNode>> tree;
	}
}
