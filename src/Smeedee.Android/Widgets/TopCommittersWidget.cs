using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Utilities;

namespace Smeedee.Android.Widgets
{
    [Widget("Top Committers", Resource.Drawable.icon_topcommitters, IsEnabled = true)]
    public class TopCommittersWidget : RelativeLayout , IWidget
    {
        private readonly IModelService<TopCommitters> service = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<TopCommitters>>();
        private readonly IBackgroundWorker bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        private TopCommitters model;

        public TopCommittersWidget(Context context) : base(context)
        {
            InflateView();
            bgWorker.Invoke(InitializeModelAndUpdateView);
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
            model = service.GetSingle();

            var list = FindViewById<ListView>(Resource.Id.TopCommittersList);
            
            var from = new string[] { "name", "commits" };
            var to = new int[] { Resource.Id.committer_name, Resource.Id.number_of_commits };

            var committer = model.Committers.First();

            var data = new Dictionary<string, object> {
                {"name", committer.Name},
                {"commits", committer.Commits}
            };
            var fillData = new List<IDictionary<string, object>>() {data};

            var adapter = new SimpleAdapter(Context, fillData, Resource.Layout.TopCommittersWidget_ListItem, from, to);
            
            Handler.Post(() => list.Adapter = adapter);
        }
    }
}