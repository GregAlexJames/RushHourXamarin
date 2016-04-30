using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RushHourXamarin.Portable;

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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.All);

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

