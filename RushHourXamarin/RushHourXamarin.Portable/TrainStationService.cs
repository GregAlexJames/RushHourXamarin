﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RushHourXamarin.Portable
{
    public static class TrainStationService
    {
        static TrainStationService()
        {
            TrainStations = JsonConvert.DeserializeObject<IEnumerable<TrainStation>>(trainStationJson);
        }

        public static void SetDistance(double lat, double lng)
        {
            foreach (var trainStation in TrainStations)
            {
                trainStation.Distance = GetDistanceFromLatLonInKm(lat, lng, trainStation.Lat, trainStation.Lng);
            }
        }

        public static async Task<IEnumerable<Train>> GetTrainsForStation(string stationId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync("http://api.perthtransit.com/1/train_stations/" + stationId);
				var getTrainStationDetailsResponse = JsonConvert.DeserializeObject<GetTrainStationResponse>(response);

				var currentTime = DateTime.Now;
				var currentHour = currentTime.Hour;
				var currentMinute = currentTime.Minute; 
				foreach(var trainDetails in getTrainStationDetailsResponse.response.times)
				{
					var trainTimeSplit = trainDetails.Time.Split(':');
					var trainHour = int.Parse(trainTimeSplit[0]);
					var trainMinute = int.Parse(trainTimeSplit[1]);

					if(trainHour < currentHour)
					{
						trainHour = trainHour + 24;
					}
					trainDetails.MinutesToTrain = (trainHour - currentHour) * 60 + (trainMinute - currentMinute); 
				}
						
				return getTrainStationDetailsResponse.response.times;
            }
        } 

        private static double GetDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            var rlat1 = Math.PI * lat1 / 180;
            var rlat2 = Math.PI * lat2 / 180;
            var theta = lon1 - lon2;
            var rtheta = Math.PI * theta / 180;
            var dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist * 1.609344;
        }

        public static IEnumerable<TrainStation> TrainStations { get; private set; }

        private static readonly string trainStationJson = 
            @"[
                {""lat"":-32.1539083145,""lng"":116.013053433,""name"":""Armadale"",""compact"":true,""identifier"":""armadale"",""url"":""http://api.perthtransit.com/1/train_stations/armadale""},
                {""lat"":-31.9129488889,""lng"":115.935994444,""name"":""Ashfield"",""compact"":true,""identifier"":""ashfield"",""url"":""http://api.perthtransit.com/1/train_stations/ashfield""},
                {""lat"":-31.9032819823,""lng"":115.947151742,""name"":""Bassendean"",""compact"":true,""identifier"":""bassendean"",""url"":""http://api.perthtransit.com/1/train_stations/bassendean""},
                {""lat"":-31.9181622222,""lng"":115.912566667,""name"":""Bayswater"",""compact"":true,""identifier"":""bayswater"",""url"":""http://api.perthtransit.com/1/train_stations/bayswater""},
                {""lat"":-32.02253,""lng"":115.954358889,""name"":""Beckenham"",""compact"":true,""identifier"":""beckenham"",""url"":""http://api.perthtransit.com/1/train_stations/beckenham""},
                {""lat"":-32.0467898375,""lng"":115.854048873,""name"":""Bull Creek"",""compact"":true,""identifier"":""bull-creek"",""url"":""http://api.perthtransit.com/1/train_stations/bull-creek""},
                {""lat"":-31.9597838889,""lng"":115.900050556,""name"":""Burswood"",""compact"":true,""identifier"":""burswood"",""url"":""http://api.perthtransit.com/1/train_stations/burswood""},
                {""lat"":-31.6365,""lng"":115.7005,""name"":""Butler"",""compact"":true,""identifier"":""butler"",""url"":""http://api.perthtransit.com/1/train_stations/butler""},
                {""lat"":-32.0097210728,""lng"":115.856149789,""name"":""Canning Bridge"",""compact"":true,""identifier"":""canning-bridge"",""url"":""http://api.perthtransit.com/1/train_stations/canning-bridge""},
                {""lat"":-32.0143464173,""lng"":115.945539306,""name"":""Cannington"",""compact"":true,""identifier"":""cannington"",""url"":""http://api.perthtransit.com/1/train_stations/cannington""},
                {""lat"":-31.9808994444,""lng"":115.911233889,""name"":""Carlisle"",""compact"":true,""identifier"":""carlisle"",""url"":""http://api.perthtransit.com/1/train_stations/carlisle""},
                {""lat"":-32.1263405556,""lng"":116.012937222,""name"":""Challis"",""compact"":true,""identifier"":""challis"",""url"":""http://api.perthtransit.com/1/train_stations/challis""},
                {""lat"":-31.9448494444,""lng"":115.845601667,""name"":""City West"",""compact"":true,""identifier"":""city-west"",""url"":""http://api.perthtransit.com/1/train_stations/city-west""},
                {""lat"":-31.9495433333,""lng"":115.872188889,""name"":""Claisebrook"",""compact"":true,""identifier"":""claisebrook"",""url"":""http://api.perthtransit.com/1/train_stations/claisebrook""},
                {""lat"":-31.9804059573,""lng"":115.781871264,""name"":""Claremont"",""compact"":true,""identifier"":""claremont"",""url"":""http://api.perthtransit.com/1/train_stations/claremont""},
                {""lat"":-31.6908105556,""lng"":115.737568889,""name"":""Clarkson"",""compact"":true,""identifier"":""clarkson"",""url"":""http://api.perthtransit.com/1/train_stations/clarkson""},
                {""lat"":-32.1251972222,""lng"":115.858328333,""name"":""Cockburn Central"",""compact"":true,""identifier"":""cockburn-central"",""url"":""http://api.perthtransit.com/1/train_stations/cockburn-central""},
                {""lat"":-31.9972394444,""lng"":115.76075,""name"":""Cottesloe"",""compact"":true,""identifier"":""cottesloe"",""url"":""http://api.perthtransit.com/1/train_stations/cottesloe""},
                {""lat"":-31.72387,""lng"":115.749814444,""name"":""Currambine"",""compact"":true,""identifier"":""currambine"",""url"":""http://api.perthtransit.com/1/train_stations/currambine""},
                {""lat"":-31.9519716667,""lng"":115.813003889,""name"":""Daglish"",""compact"":true,""identifier"":""daglish"",""url"":""http://api.perthtransit.com/1/train_stations/daglish""},
                {""lat"":-31.8963955556,""lng"":115.980217778,""name"":""East Guildford"",""compact"":true,""identifier"":""east-guildford"",""url"":""http://api.perthtransit.com/1/train_stations/east-guildford""},
                {""lat"":-31.9424016667,""lng"":115.877896667,""name"":""East Perth"",""compact"":true,""identifier"":""east-perth"",""url"":""http://api.perthtransit.com/1/train_stations/east-perth""},
                {""lat"":-31.7721055556,""lng"":115.778591111,""name"":""Edgewater"",""compact"":true,""identifier"":""edgewater"",""url"":""http://api.perthtransit.com/1/train_stations/edgewater""},
                {""lat"":-31.956745,""lng"":115.855422778,""name"":""Esplanade"",""compact"":true,""identifier"":""esplanade"",""url"":""http://api.perthtransit.com/1/train_stations/esplanade""},
                {""lat"":-32.0520361111,""lng"":115.745046111,""name"":""Fremantle"",""compact"":true,""identifier"":""fremantle"",""url"":""http://api.perthtransit.com/1/train_stations/fremantle""},
                {""lat"":-31.9146972222,""lng"":115.823331111,""name"":""Glendalough"",""compact"":true,""identifier"":""glendalough"",""url"":""http://api.perthtransit.com/1/train_stations/glendalough""},
                {""lat"":-32.0714511111,""lng"":116.000011667,""name"":""Gosnells"",""compact"":true,""identifier"":""gosnells"",""url"":""http://api.perthtransit.com/1/train_stations/gosnells""},
                {""lat"":-31.9865844444,""lng"":115.764891111,""name"":""Grant Street"",""compact"":true,""identifier"":""grant-street"",""url"":""http://api.perthtransit.com/1/train_stations/grant-street""},
                {""lat"":-31.8175022222,""lng"":115.783130556,""name"":""Greenwood"",""compact"":true,""identifier"":""greenwood"",""url"":""http://api.perthtransit.com/1/train_stations/greenwood""},
                {""lat"":-31.8990383333,""lng"":115.965900556,""name"":""Guildford"",""compact"":true,""identifier"":""guildford"",""url"":""http://api.perthtransit.com/1/train_stations/guildford""},
                {""lat"":-31.745485277,""lng"":115.76742763,""name"":""Joondalup"",""compact"":true,""identifier"":""joondalup"",""url"":""http://api.perthtransit.com/1/train_stations/joondalup""},
                {""lat"":-31.9682866667,""lng"":115.79627,""name"":""Karrakatta"",""compact"":true,""identifier"":""karrakatta"",""url"":""http://api.perthtransit.com/1/train_stations/karrakatta""},
                {""lat"":-32.1130493645,""lng"":116.013135811,""name"":""Kelmscott"",""compact"":true,""identifier"":""kelmscott"",""url"":""http://api.perthtransit.com/1/train_stations/kelmscott""},
                {""lat"":-32.036298293,""lng"":115.968930201,""name"":""Kenwick"",""compact"":true,""identifier"":""kenwick"",""url"":""http://api.perthtransit.com/1/train_stations/kenwick""},
                {""lat"":-32.2351509589,""lng"":115.842540189,""name"":""Kwinana"",""compact"":true,""identifier"":""kwinana"",""url"":""http://api.perthtransit.com/1/train_stations/kwinana""},
                {""lat"":-31.9388016667,""lng"":115.8404,""name"":""Leederville"",""compact"":true,""identifier"":""leederville"",""url"":""http://api.perthtransit.com/1/train_stations/leederville""},
                {""lat"":-31.9717994444,""lng"":115.791360556,""name"":""Loch Street"",""compact"":true,""identifier"":""loch-street"",""url"":""http://api.perthtransit.com/1/train_stations/loch-street""},
                {""lat"":-32.0490811111,""lng"":115.982361111,""name"":""Maddington"",""compact"":true,""identifier"":""maddington"",""url"":""http://api.perthtransit.com/1/train_stations/maddington""},
                {""lat"":-32.5263937276,""lng"":115.746566322,""name"":""Mandurah"",""compact"":true,""identifier"":""mandurah"",""url"":""http://api.perthtransit.com/1/train_stations/mandurah""},
                {""lat"":-31.9282811111,""lng"":115.891876111,""name"":""Maylands"",""compact"":true,""identifier"":""maylands"",""url"":""http://api.perthtransit.com/1/train_stations/maylands""},
                {""lat"":-31.9517538889,""lng"":115.866416667,""name"":""McIver"",""compact"":true,""identifier"":""mciver"",""url"":""http://api.perthtransit.com/1/train_stations/mciver""},
                {""lat"":-31.9225005556,""lng"":115.900346111,""name"":""Meltham"",""compact"":true,""identifier"":""meltham"",""url"":""http://api.perthtransit.com/1/train_stations/meltham""},
                {""lat"":-31.8917617481,""lng"":116.000524876,""name"":""Midland"",""compact"":true,""identifier"":""midland"",""url"":""http://api.perthtransit.com/1/train_stations/midland""},
                {""lat"":-32.0070861111,""lng"":115.757213889,""name"":""Mosman Park"",""compact"":true,""identifier"":""mosman-park"",""url"":""http://api.perthtransit.com/1/train_stations/mosman-park""},
                {""lat"":-31.9348561111,""lng"":115.880862222,""name"":""Mt Lawley"",""compact"":true,""identifier"":""mt-lawley"",""url"":""http://api.perthtransit.com/1/train_stations/mt-lawley""},
                {""lat"":-32.0661222275,""lng"":115.850487825,""name"":""Murdoch"",""compact"":true,""identifier"":""murdoch"",""url"":""http://api.perthtransit.com/1/train_stations/murdoch""},
                {""lat"":-32.0297955556,""lng"":115.751708889,""name"":""North Fremantle"",""compact"":true,""identifier"":""north-fremantle"",""url"":""http://api.perthtransit.com/1/train_stations/north-fremantle""},
                {""lat"":-31.9870511111,""lng"":115.916055,""name"":""Oats Street"",""compact"":true,""identifier"":""oats-street"",""url"":""http://api.perthtransit.com/1/train_stations/oats-street""},
                {""lat"":-31.9510907808,""lng"":115.859925408,""name"":""Perth"",""compact"":true,""identifier"":""perth"",""url"":""http://api.perthtransit.com/1/train_stations/perth""},
                {""lat"":-31.951565,""lng"":115.858122222,""name"":""Perth Underground"",""compact"":true,""identifier"":""perth-underground"",""url"":""http://api.perthtransit.com/1/train_stations/perth-underground""},
                {""lat"":-32.0070044444,""lng"":115.938391111,""name"":""Queens Park"",""compact"":true,""identifier"":""queens-park"",""url"":""http://api.perthtransit.com/1/train_stations/queens-park""},
                {""lat"":-32.2896136264,""lng"":115.760981726,""name"":""Rockingham"",""compact"":true,""identifier"":""rockingham"",""url"":""http://api.perthtransit.com/1/train_stations/rockingham""},
                {""lat"":-32.0846438889,""lng"":116.011204444,""name"":""Seaforth"",""compact"":true,""identifier"":""seaforth"",""url"":""http://api.perthtransit.com/1/train_stations/seaforth""},
                {""lat"":-31.9594388889,""lng"":115.805468889,""name"":""Shenton Park"",""compact"":true,""identifier"":""shenton-park"",""url"":""http://api.perthtransit.com/1/train_stations/shenton-park""},
                {""lat"":-32.1376083333,""lng"":116.010460556,""name"":""Sherwood"",""compact"":true,""identifier"":""sherwood"",""url"":""http://api.perthtransit.com/1/train_stations/sherwood""},
                {""lat"":-31.97678,""lng"":115.786839444444,""name"":""Showgrounds"",""compact"":true,""identifier"":""showgrounds"",""url"":""http://api.perthtransit.com/1/train_stations/showgrounds""},
                {""lat"":-31.8944166667,""lng"":115.804743889,""name"":""Stirling"",""compact"":true,""identifier"":""stirling"",""url"":""http://api.perthtransit.com/1/train_stations/stirling""},
                {""lat"":-31.9446588898,""lng"":115.824229369,""name"":""Subiaco"",""compact"":true,""identifier"":""subiaco"",""url"":""http://api.perthtransit.com/1/train_stations/subiaco""},
                {""lat"":-31.9002972222,""lng"":115.955741667,""name"":""Success Hill"",""compact"":true,""identifier"":""success-hill"",""url"":""http://api.perthtransit.com/1/train_stations/success-hill""},
                {""lat"":-31.9825133333,""lng"":115.77113,""name"":""Swanbourne"",""compact"":true,""identifier"":""swanbourne"",""url"":""http://api.perthtransit.com/1/train_stations/swanbourne""},
                {""lat"":-32.0474468801,""lng"":115.952538188,""name"":""Thornlie"",""compact"":true,""identifier"":""thornlie"",""url"":""http://api.perthtransit.com/1/train_stations/thornlie""},
                {""lat"":-31.9715605555556,""lng"":115.902151666667,""name"":""Victoria Park"",""compact"":true,""identifier"":""victoria-park"",""url"":""http://api.perthtransit.com/1/train_stations/victoria-park""},
                {""lat"":-32.0114994444,""lng"":115.755036111,""name"":""Victoria Street"",""compact"":true,""identifier"":""victoria-street"",""url"":""http://api.perthtransit.com/1/train_stations/victoria-street""},
                {""lat"":-32.3265432493,""lng"":115.767816157,""name"":""Warnbro"",""compact"":true,""identifier"":""warnbro"",""url"":""http://api.perthtransit.com/1/train_stations/warnbro""},
                {""lat"":-31.8444894444,""lng"":115.796193333,""name"":""Warwick"",""compact"":true,""identifier"":""warwick"",""url"":""http://api.perthtransit.com/1/train_stations/warwick""},
                {""lat"":-32.2639682397,""lng"":115.817154814,""name"":""Wellard"",""compact"":true,""identifier"":""wellard"",""url"":""http://api.perthtransit.com/1/train_stations/wellard""},
                {""lat"":-31.9950055556,""lng"":115.923252222,""name"":""Welshpool"",""compact"":true,""identifier"":""welshpool"",""url"":""http://api.perthtransit.com/1/train_stations/welshpool""},
                {""lat"":-31.9426166667,""lng"":115.833192778,""name"":""West Leederville"",""compact"":true,""identifier"":""west-leederville"",""url"":""http://api.perthtransit.com/1/train_stations/west-leederville""},
                {""lat"":-31.799406952,""lng"":115.782332213,""name"":""Whitfords"",""compact"":true,""identifier"":""whitfords"",""url"":""http://api.perthtransit.com/1/train_stations/whitfords""},
                {""lat"":-31.8916761111,""lng"":115.99243,""name"":""Woodbridge"",""compact"":true,""identifier"":""woodbridge"",""url"":""http://api.perthtransit.com/1/train_stations/woodbridge""}
            ]";
    }
}
