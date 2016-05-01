using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RushHourXamarin.Portable;

namespace RushHourXamarin.Droid
{
    [Activity(Label = "Station")]
    public class StationDetailsActivity : Activity
    {
        private readonly TrainStation[] _trainStations;
        private string _id;
        private TrainStation _station;
        public StationDetailsActivity()
        {
            _trainStations = TrainStationService.TrainStations.ToArray();
        }

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _id = Intent.GetStringExtra("id");
            _station = _trainStations.Single(p => p.identifier == _id);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.All);

			try
			{
				var trains = await TrainStationService.GetTrainsForStation(_id);

				var listView = FindViewById<ListView>(Resource.Id.List);
				listView.Adapter = new TrainsAdapter(this, trains);
			}
			catch(Exception ex) {

			}
        }
    }
}