using Android.App;
using Android.Views;
using Android.Widget;
using RushHourXamarin.Portable;

namespace RushHourXamarin.Droid
{
    public class TrainStationAdapter : BaseAdapter<TrainStation>
    {
        readonly TrainStation[] _trainStations;
        Activity context;
        public TrainStationAdapter(Activity context, TrainStation[] trainStations) : base()
        {
            this.context = context;
            _trainStations = trainStations;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override TrainStation this[int position]
        {
            get { return _trainStations[position]; }
        }
        public override int Count
        {
            get { return _trainStations.Length; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.StationItem, null);

            var station = _trainStations[position];

            view.FindViewById<TextView>(Resource.Id.Name).Text = station.Name;
            view.FindViewById<TextView>(Resource.Id.Distance).Text = station.Distance + "km";

            return view;
        }
    }
}