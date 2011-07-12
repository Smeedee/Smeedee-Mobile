using Android.App;
using Android.OS;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "About Smeedee Mobile", Theme = "@android:style/Theme.NoTitleBar")]
    public class About : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.About);
        }
    }
}