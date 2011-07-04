using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Android.OS;
using Smeedee.Android.Widgets;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.Android
{
    [Activity(Label = "Smeedee.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class WidgetContainer : Activity
    {
        private static Animation slideLeftIn;
        private static Animation slideLeftOut;
        private static Animation slideRightIn;
        private static Animation slideRightOut;

        //swipe gesture constants
        private static int SWIPE_MIN_DISTANCE = 120;
        private static int SWIPE_MAX_OFF_PATH = 250;
        private static int SWIPE_THRESHOLD_VELOCITY = 200;

        private GestureDetector gestureDetector;

        private static ViewFlipper flipper;
        private static Context context;

        protected override void OnCreate(Bundle bundle)
        {
            context = this;
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //var scroller0 = FindViewById<ScrollView>(Resource.Id.scripScroll0);
            //var scroller1 = FindViewById<ScrollView>(Resource.Id.scripScroll1);
            //var scroller2 = FindViewById<ScrollView>(Resource.Id.scripScroll2);

            //flipper = FindViewById<ViewFlipper>(Resource.Id.flipper);//you will need to use the flipper object later in your SimpleOnGestureListener class to flip the views
            
            //slideLeftIn = AnimationUtils.LoadAnimation(this, Resource.Animation.InFromLeft);
            //slideLeftOut = AnimationUtils.LoadAnimation(this, Resource.Animation.InFromRight);
            //slideRightIn = AnimationUtils.LoadAnimation(this, Resource.Animation.OutToLeft);
            //slideRightOut = AnimationUtils.LoadAnimation(this, Resource.Animation.OutToRight);

            //gestureDetector = new GestureDetector(new MyGestureDetector());

            /*
            var layout = FindViewById<LinearLayout>(Resource.Id.ContainerLayout);
            var widgets = GetWidgets();
            foreach (var widget in widgets)
            {
                layout.AddView(widget as View);
            }
            ConfigureDependencies();
             */
        }

        //private class MyGestureDetector : GestureDetector.SimpleOnGestureListener 
        //{

        //    public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY) 
        //    {
        //        // int delta = 0;
        //        if (Math.Abs(e1.GetY() - e2.GetY()) > SWIPE_MAX_OFF_PATH) return false;
            
        //        try { 
        //            // right to left swipe
        //            if (e1.GetX() - e2.GetX() > SWIPE_MIN_DISTANCE && Math.Abs(velocityX) > SWIPE_THRESHOLD_VELOCITY) 
        //            {
        //                if (CanFlipRight()) 
        //                {
        //                    flipper.SetInAnimation(context, Resource.Animation.InFromRight);
        //                    flipper.SetOutAnimation(context, Resource.Animation.OutToLeft); 
        //                    flipper.ShowNext();
        //                }
        //                else
        //                { 
        //                    return false;
        //                }
        //                //left to right swipe
        //            } 
        //            else if (e2.GetX() - e1.GetX() > SWIPE_MIN_DISTANCE && Math.Abs(velocityX) > SWIPE_THRESHOLD_VELOCITY) 
        //            {
        //                if(CanFlipLeft())
        //                {
        //                    flipper.SetInAnimation(context, Resource.Animation.InFromLeft);
        //                    flipper.SetOutAnimation(context, Resource.Animation.OutToRight); 
        //                    flipper.ShowPrevious();
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            } 
        //        } 
        //        catch (Exception e) {
        //            // nothing
        //        }
        //        return true;
        //    }

        //    private bool CanFlipLeft()
        //    {
        //        return true;
        //    }

        //    private bool CanFlipRight()
        //    {
        //        return true;
        //    }
        //}

        private IEnumerable<IWidget> GetWidgets()
        {
            return new IWidget[] {new TestWidget(null)};
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }

        private void ConfigureDependencies()
        {
            SmeedeeApp.SmeedeeService = new SmeedeeFakeService();
            RegisterAllSupportedWidgets();
        }

        private void RegisterAllSupportedWidgets()
        {
            
        }
    }
}

