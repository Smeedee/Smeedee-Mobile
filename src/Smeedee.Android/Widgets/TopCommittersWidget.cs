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
        private DateTime _lastRefreshTime;

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
            _model.Load(() => ((Activity)Context).RunOnUiThread(UpdateListView));
            _lastRefreshTime = DateTime.Now;
        }

        public DateTime LastRefreshTime()
        {
            return _lastRefreshTime;
        }

        private void UpdateListView()
        {
            var from = new[] { "name", "commits" };
            var to = new[] { Resource.Id.TopCommittersWidget_committer_name, Resource.Id.TopCommittersWidget_number_of_commits };
            
            var data = _model.Committers
                            .Select(c =>
                                        {
                                            return new Dictionary<string, object>
                                                       {{"name", c.Name}, {"commits", c.Commits}, {"image", c.ImageUri}};
                                        })
                            .Cast<IDictionary<string, object>>().ToList();

            var listView = FindViewById<ListView>(Resource.Id.TopCommittersList);
            var topCommittersAdapter = new TopCommittersAdapter(Context, data, Resource.Layout.TopCommittersWidget_ListItem, from, to);
            topCommittersAdapter.SetModel(_model);
            listView.Adapter = topCommittersAdapter;
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
        private TopCommitters _model;
        private int _commitBarFullWidth;
        private IList<IDictionary<string, object>> items;

        public TopCommittersAdapter(IntPtr doNotUse) 
            : base(doNotUse)
        {
        }

        public TopCommittersAdapter(Context context, IList<IDictionary<string, object>> items, int resource, string[] from, int[] to)
            : base(context, items, resource, from, to)
        {
            this.items = items;
        }
        public void SetModel(TopCommitters model)
        {
            _model = model;
        }
        public override bool IsEnabled(int position) // To disable list item clicks
        { return false; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);
            var commits = Convert.ToInt32(((TextView)view.FindViewById(Resource.Id.TopCommittersWidget_number_of_commits)).Text);
            var committerbar = (TextView) view.FindViewById(Resource.Id.TopCommittersWidget_commit_bar);
            var committerbarBackground = (TextView)view.FindViewById(Resource.Id.TopCommittersWidget_commit_bar_background);
            if (_commitBarFullWidth <= 0)
                _commitBarFullWidth = (view.FindViewById<TextView>(Resource.Id.TopCommittersWidget_committer_name)).MeasuredWidth;
            if (_model.Committers != null && _model.Committers.Count() >= 1)
            {
                var percent = commits/(float) _model.Committers.First().Commits;
                committerbar.SetWidth(Convert.ToInt32(percent*(_commitBarFullWidth - 70f)));
                committerbarBackground.SetWidth(_commitBarFullWidth - 70);
            }
            else 
                committerbar.SetWidth(1);


            LoadImage(position, view);

            return view;
        }
        private void LoadImage(int position, View view)
        {
            var image = (view as RelativeLayout).GetChildAt(0) as ImageView;
            var uri = items[position]["image"] as Uri;
            image.LoadUriOrDefault(uri, Resource.Drawable.DefaultPerson);
        }
    }
}