using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using RushHourXamarin.Portable;
using Plugin.Geolocator;

namespace RushHourXamarin.Droid
{
    [Activity(Label = "Rush Hour Xamarin", MainLauncher = true, Icon = "@drawable/icon")]
    public class AllActivity : Activity
    {
        private readonly TrainStation[] _trainStations;
        public AllActivity()
        {
            _trainStations = TrainStationService.TrainStations.ToArray();
        }

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.All);

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            double lat;
            double lng;
            
            try
            {
                var result = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                lat = result.Latitude;
                lng = result.Longitude;
            }
            catch (Exception)
            {
                lat = -31.9522;
                lng = 115.8589;
            }

            TrainStationService.SetDistance(lat, lng);

            var listView = FindViewById<ListView>(Resource.Id.List);
            listView.Adapter = new TrainStationAdapter(this, _trainStations);
            listView.ItemClick += OnListItemClick;
        }

        private void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var t = _trainStations[e.Position];

            var activity = new Intent(this, typeof(StationDetailsActivity));
            activity.PutExtra("id", t.identifier);
            StartActivity(activity);
        }

    }
}

