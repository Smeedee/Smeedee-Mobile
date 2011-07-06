using System;
using System.Collections.Generic;

using Android.Content;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android.Widgets
{
    public class BuildStatus : RelativeLayout, IWidget
    {

        public BuildStatus(Context context)
            : base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (inflater != null)
            {
                inflater.Inflate(Resource.Layout.BuildStatusWidget, this);
            }
            else
            {
                throw new Exception("Ammagad inflater was null");
            }


            var lv = FindViewById<ListView>(Resource.Id.build_list);

            // create the grid item mapping
            var from = new[] { "rowid", "col_1", "col_2", "col_3" };
            var to = new[] { Resource.Id.item1, Resource.Id.item2, Resource.Id.item3, Resource.Id.item4 };

            // prepare the list of all records
            IList<IDictionary<String, object>> fillMaps = new List<IDictionary<String, object>>();
            for (var i = 0; i < 10; i++)
            {
                IDictionary<String, object> map = new Dictionary<String, object>
                                                      {
                                                          {"rowid", "" + i},
                                                          {"col_1", "col_1_item_" + i},
                                                          {"col_2", "col_2_item_" + i},
                                                          {"col_3", "col_3_item_" + i}
                                                      };
                fillMaps.Add(map);
            }
            // fill in the grid_item layout
            var adapter = new SimpleAdapter(Context, fillMaps, Resource.Layout.BuildStatusWidget_ListItem, from, to);
            lv.Adapter = adapter;
        }
    }
}