using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Smeedee.Android.Widgets.Settings;
using Smeedee.Model;

namespace Smeedee.Android.Widgets
{
    [Widget("Top Committers", StaticDescription = "A list of most active committers", SettingsType = typeof(TopCommittersSettings))]
    public class TopCommittersWidget : RelativeLayout, IWidget
    {
        private readonly TopCommitters _model;

        public event EventHandler DescriptionChanged;

        public TopCommittersWidget(Context context) : base(context)
        {
            InflateView();
            _model = new TopCommitters();
            Refresh();
        }

        private void InflateView()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (inflater == null) throw new Exception("Unable to inflate view on Top committers widget");

            inflater.Inflate(Resource.Layout.TopCommittersWidget, this);
        }

        public void Refresh()
        {
            _model.Load(() => ContextSwitcher.Using(Context as Activity).InUI(UpdateListView).Run());
        }

        private void UpdateListView()
        {
            var from = new[] { "name", "commits" };
            var to = new[] { Resource.Id.TopCommittersWidget_committer_name, Resource.Id.TopCommittersWidget_number_of_commits };

            var data = _model.Committers
                            .Select(c => new Dictionary<string, object> { {"name", c.Name}, {"commits", c.Commits} })
                            .Cast<IDictionary<string, object>>().ToList();

            var listView = FindViewById<ListView>(Resource.Id.TopCommittersList);
            listView.Adapter = new TopCommittersAdapter(Context, data, Resource.Layout.TopCommittersWidget_ListItem, from, to);
        }

        public string GetDynamicDescription()
        {
            return _model.Description;
        }

        public void OnDescriptionChanged(EventArgs args)
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, args);
        }
    }

    internal class TopCommittersAdapter : SimpleAdapter
    {
        public TopCommittersAdapter(Context context, IList<IDictionary<string, object>> data, int resource, string[] @from, int[] to) : base(context, data, resource, from, to) { }
        public override bool IsEnabled(int position) { return false; }
    }
}