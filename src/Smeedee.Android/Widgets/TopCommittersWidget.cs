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
        private readonly IModelService<TopCommitters> service = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<TopCommitters>>();
        private readonly IBackgroundWorker bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        private ISharedPreferences preferences;
        private TopCommitters model;
        private ListView list;
        private string _dynamicDescription;

        public TopCommittersWidget(Context context) : base(context)
        {
            InflateView();

            list = FindViewById<ListView>(Resource.Id.TopCommittersList);
            preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            bgWorker.Invoke(LoadModelAndUpdateView);
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

        private void LoadModelAndUpdateView()
        {
            LoadModel();
            ((Activity) Context).RunOnUiThread(UpdateView);
        }

        private void LoadModel()
        {
            var args = new Dictionary<string, string>() {
                {"count", preferences.GetString("TopCommittersCountPref", "5")},
                {"time", preferences.GetString("TopCommittersTimePref", "1")},
            };
            model = service.Get(args);
        }

        private void UpdateView()
        {
            _dynamicDescription = model.DaysText;
            list.Adapter = CreateAdapter();
        }

        private SimpleAdapter CreateAdapter()
        {
            var from = new string[] { "name", "commits" };
            var to = new int[] { Resource.Id.TopCommittersWidget_committer_name, Resource.Id.TopCommittersWidget_number_of_commits };

            var data = GetModelAsListData(from[0], from[1]);

            return new TopCommittersAdapter(Context, data, Resource.Layout.TopCommittersWidget_ListItem, from, to);
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
            LoadModelAndUpdateView();
        }

        public string GetDynamicDescription()
        {
            return _dynamicDescription;
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