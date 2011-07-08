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
    public class TopCommittersWidget : RelativeLayout, IWidget
    {
        private readonly IModelService<TopCommitters> service = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<TopCommitters>>();
        private readonly IBackgroundWorker bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        private TopCommitters model;
        private ListView list;

        public TopCommittersWidget(Context context) : base(context)
        {
            InflateView();

            list = FindViewById<ListView>(Resource.Id.TopCommittersList);

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
            LoadModel();
            ((Activity)Context).RunOnUiThread(() => list.Adapter = CreateAdapter());
        }

        private void LoadModel()
        {
            model = service.GetSingle();
        }

        private SimpleAdapter CreateAdapter()
        {
            var from = new string[] { "name", "commits" };
            var to = new int[] { Resource.Id.committer_name, Resource.Id.number_of_commits };

            var data = GetModelAsListData(from[0], from[1]);

            return new SimpleAdapter(Context, data, Resource.Layout.TopCommittersWidget_ListItem, from, to);
        }

        private List<IDictionary<string, object>> GetModelAsListData(string nameField, string commitsField)
        {
            var data = new List<IDictionary<string, object>>();

            foreach (var committer in model.Committers)
            {
                data.Add(new Dictionary<string, object> {
                    {nameField, committer.Name},
                    {commitsField, committer.Commits}
                });
            }

            return data;
        } 
    }
}