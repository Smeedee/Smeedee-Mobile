using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Widgets
{
    [Widget("Top Committers", StaticDescription = "Shows developers and number of commits")]
    public class TopCommittersWidget : RelativeLayout, IWidget
    {
        private readonly IBackgroundWorker bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        private ISharedPreferences preferences;
        private TopCommitters model;
        private ListView list;

        public TopCommittersWidget(Context context) : base(context)
        {
            InflateView();

            list = FindViewById<ListView>(Resource.Id.TopCommittersList);
            preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            model = new TopCommitters();
            model.Load(() => ((Activity) Context).RunOnUiThread(UpdateView));
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

        private void UpdateView()
        {
            var from = new string[] { "name", "commits" };
            var to = new int[] { Resource.Id.TopCommittersWidget_committer_name, Resource.Id.TopCommittersWidget_number_of_commits };

            var data = GetModelAsListData(from[0], from[1]);

            list.Adapter = new TopCommittersAdapter(Context, data, Resource.Layout.TopCommittersWidget_ListItem, from, to);
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

        public void Refresh()
        {
        }

        public string GetDynamicDescription()
        {
            return model.Description;
        }
    }

    internal class TopCommittersAdapter : SimpleAdapter
    {
        public TopCommittersAdapter(Context context, List<IDictionary<string, object>> data, int resource, string[] @from, int[] to) : base(context, data, resource, from, to)
        {
                        
        }

        public override bool IsEnabled(int position)
        {
            return false;
        }
    }
}