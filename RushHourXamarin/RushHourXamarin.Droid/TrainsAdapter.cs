using System;
using Android.Widget;
using RushHourXamarin.Portable;
using Android.App;
using Android.Views;

namespace RushHourXamarin.Droid
{
	public class TrainsAdapter : BaseAdapter<Train>
	{
		readonly Train[] _trains;
		Activity context;
		public TrainsAdapter(Activity context, Train[] trains) : base()
		{
			this.context = context;
			_trains = trains;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Train this[int position]
		{
			get { return _trains[position]; }
		}
		public override int Count
		{
			get { return _trains.Length; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Resource.Layout.TrainItem, null);

			var train = _trains[position];

			view.FindViewById<TextView>(Resource.Id.Destination).Text = train.Line;
			view.FindViewById<TextView>(Resource.Id.TimeToSchedule).Text = train.Time + " mins";

			return view;
		}
	}
}

