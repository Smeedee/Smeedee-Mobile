using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "EnabledWidgetsScreen", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar")]
    public class EnabledWidgetsScreen : Activity
    {
        private ListView lView;
        private string[] lv_items = { "Android", "iPhone", "BlackBerry",
                                       "AndroidPeople", "J2ME", "Listview", "ArrayAdapter", "ListItem",
                                       "Us", "UK", "India"  };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.EnabledWidgetsScreen);
            
            lView = FindViewById<ListView>(Resource.Id.ListView01);
            //	 Set option as Multiple Choice. So that user can able to select more the one option from list
            
            var adapter = new ArrayAdapter<string>(this, Resource.Layout.EnabledWidgetsScreen, lv_items);

            lView.ChoiceMode = ChoiceMode.Multiple;

            lView.Adapter = adapter;
            
        }
    }
}