using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.Android.Widgets
{
    [Widget("Top Committers", Resource.Drawable.icon_topcommitters, DescriptionStatic = "Shows developers and number of commits", IsEnabled = true)]
    public class TopCommittersWidget : RelativeLayout, IWidget
    {
        private readonly IModelService<TopCommitters> service = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<TopCommitters>>();

        private TopCommitters model;

        public TopCommittersWidget(Context context) : base(context)
        {
            InflateView();
            //InitializeModelAndUpdateView();
        }

        private void InflateView()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (inflater != null)
            {
                inflater.Inflate(Resource.Layout.TopCommittersWidget, this);
            }
            else
            {
                throw new Exception("Unable to inflate view on Top committers widget");
            }
        }

        private void InitializeModelAndUpdateView()
        {
            //model = service.GetSingle();

            var list = FindViewById<ListView>(Resource.Id.TopCommittersList);

            var from = new string[] { "Name", "Commits" };
            var to = new int[] { Resource.Id.CommitterName, Resource.Id.NumberOfCommits };

            //var committer = model.Committers.First();

            var data = new Dictionary<string, object> {
                {"Name", "Lars"},
                {"Commits", "10"}
            };
            var fillData = new List<IDictionary<string, object>>() {data};

            var adapter = new SimpleAdapter(Context, fillData, Resource.Layout.TopCommittersWidget_ListItem, from, to);

            Handler.Post(() => list.Adapter = adapter);
        }
    }
}