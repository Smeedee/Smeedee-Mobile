using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Utilities;

namespace Smeedee.Android.Widgets
{
    [Widget("Top Committers", Resource.Drawable.icon_topcommitters, DescriptionStatic = "Shows developers and number of commits")]
    public class TopCommittersWidget : RelativeLayout, IWidget
    {
        private readonly IModelService<TopCommitters> service = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<TopCommitters>>();
        private readonly IBackgroundWorker bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        private ISharedPreferences preferences;

        private TopCommitters model;
        private ListView list;

        public TopCommittersWidget(Context context) : base(context)
        {
            InflateView();

            preferences = PreferenceManager.GetDefaultSharedPreferences(Context);

            list = FindViewById<ListView>(Resource.Id.TopCommittersList);

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
            model = service.GetSingle(args);
        }

        private void UpdateView()
        {
            var text = FindViewById<TextView>(Resource.Id.TopCommittersTimeText);
            text.Text = TextFromNumberOfDays(model.Days);

            list.Adapter = CreateAdapter();
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

        private static string TextFromNumberOfDays(int days)
        {
            switch (days)
            {
                case 1:
                    return "Showing number of commits for the past 24 hours";
                case 7:
                    return "Showing number of commits for the past week";
                case 30:
                    return "Showing number of commit for the past month";
                default:
                    return "Showing number of commits for the past " + days + " days";
            }
        }

        public void Refresh()
        {
            LoadModelAndUpdateView();
        }
    }
}