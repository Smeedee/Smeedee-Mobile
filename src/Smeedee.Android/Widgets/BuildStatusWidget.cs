using System;
using System.Collections.Generic;

using Android.Content;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android.Widgets
{
    public class BuildStatusWidget : RelativeLayout, IWidget
    {

        public BuildStatusWidget(Context context)
            : base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            inflater.Inflate(Resource.Layout.BuildStatusWidget, this);
            


            var lv = FindViewById<ListView>(Resource.Id.build_list);

            // create the grid item mapping
            var from = new[] { "project_name", "username", "datetime" };
            var to = new[] {  Resource.Id.item2, Resource.Id.item3, Resource.Id.item4 };

            // prepare the list of all records
            IList<IDictionary<String, object>> fillMaps = new List<IDictionary<String, object>>();
            for (var i = 0; i < 10; i++)
            {
                IDictionary<String, object> map = new Dictionary<String, object>
                                                      {
                                                          {"project_name", "Project " + i},
                                                          {"username", "Dag Olav Prestegarden"},
                                                          {"datetime", DateTime.Now.ToString()}
                                                      };
                fillMaps.Add(map);
            }
            // fill in the grid_item layout
            var adapter = new SimpleAdapter(Context, fillMaps, Resource.Layout.BuildStatusWidget_ListItem, from, to);
            lv.Adapter = adapter;
        }
    }
}