using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RushHourXamarin.Portable
{
    public class TrainStation
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Name { get; set; }
        public string identifier { get; set; }
        public double Distance { get; set; }
    }

    public class Train
    {
		public int? cars{get;set;}
		public string line{get;set;}
		public string time{ get; set; }
    }

	public class GetTrainStationResponse
	{
		public TrainStationFull response{ get; set; }
	}

	public class TrainStationFull
	{
		public IEnumerable<Train> times{ get; set; }
	}


}
