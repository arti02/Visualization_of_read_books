using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	public class Sort
	{
		public static void Sortb(ChartValues<ScatterPoint> val)
		{
			List<ScatterPoint> scatterPoints = new List<ScatterPoint>();
			while (val.Count == 0)
			{
				foreach (ScatterPoint i in val)
				{
					if (val.Min().X == i.X)
					{
						
						scatterPoints.Add(i);
						val.Remove(i);
					}

				}
			}
			scatterPoints.OrderBy(book => book.X);
			foreach(ScatterPoint i in scatterPoints)
			{
				val.Add(i);
				
			}
		}
	}
}
