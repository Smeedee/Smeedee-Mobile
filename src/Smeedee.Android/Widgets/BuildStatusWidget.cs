using System;
using System.Collections.Generic;

using Android.Content;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android.Widgets
{
    public class BuildStatusWidget : RelativeLayout, IWidget
    {
        private readonly string[] listItemMappingFrom = new[] { "project_name", "username", "datetime" };
        private readonly int[] listItemMappingTo = new[] {  Resource.Id.item2, Resource.Id.item3, Resource.Id.item4 };

        public BuildStatusWidget(Context context)
            : base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            CreateGui();
            FillBuildList();
        }
        
        private void CreateGui()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            inflater.Inflate(Resource.Layout.BuildStatusWidget, this);
        }

        private void FillBuildList()
        {
            var buildList = FindViewById<ListView>(Resource.Id.build_list);

            var adapter = new SimpleAdapter(Context, CreateFakeData(), Resource.Layout.BuildStatusWidget_ListItem, listItemMappingFrom, listItemMappingTo);
            buildList.Adapter = adapter;
        }

        private IList<IDictionary<string, object>> CreateFakeData()
        {
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
            return fillMaps;
        }
    }
}