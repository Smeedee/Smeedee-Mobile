using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace Smeedee.Android
{
    /**
     * Custom ViewFlipper that suppresses known issue that will crash the application when rotating screen.
     * 
     * See 
     *   http://code.google.com/p/android/issues/detail?id=6191
     * and
     *   http://stackoverflow.com/questions/3019606/why-does-keyboard-slide-crash-my-app/3026985#3026985
     * for issue report and information.
     */
    public class NonCrashingViewFlipper : ViewFlipper
    {

        private const int SCROLL_NEXT_VIEW_THRESHOLD = 100; // TODO: Make dynamic based on screen size?
        private MotionEvent downStart;
        private IBackgroundWorker bgWorker;
        private ViewVisibilityMessageHandler visibilityMessageHandler;

        public NonCrashingViewFlipper(Context context, IAttributeSet attrs) 
            : base(context, attrs)
        {
            visibilityMessageHandler = new ViewVisibilityMessageHandler();
            bgWorker = ((SmeedeeApplication)((Activity) Context).Application).App.ServiceLocator.Get<IBackgroundWorker>();
        }


        protected override void OnDetachedFromWindow()
        {
            try
            {
                base.OnDetachedFromWindow();
            }
            catch (Exception)
            {
                Log.Debug("TT","NonCrashingViewFlipper Stopped a viewflipper crash");
                base.StopFlipping();
            }
        }
        public int GetPreviousChildIndex()
        {
            if (DisplayedChild == ChildCount-1) 
                return 0;
            return DisplayedChild + 1;
        }

        public int GetNextChildIndex()
        {
            if (DisplayedChild == 0)
                return ChildCount - 1;
            return DisplayedChild - 1;
        }

        public override bool OnTouchEvent(MotionEvent touchEvent)
        {
            var currentView = CurrentView;
            var nextView = GetChildAt(GetNextChildIndex());
            var previousView = GetChildAt(GetPreviousChildIndex());
            var xCoordinateDifference = (int)(touchEvent.GetX() - downStart.GetX());

            switch (touchEvent.Action)
            {
                case MotionEventActions.Down:
                    downStart = MotionEvent.Obtain(touchEvent);

                    nextView.Visibility = ViewStates.Invisible;
                    previousView.Visibility = ViewStates.Invisible;
                    break;
                case MotionEventActions.Up:
                    var currentX = touchEvent.GetX();
                    if (downStart.GetX() < currentX - SCROLL_NEXT_VIEW_THRESHOLD)
                    {
                        InAnimation = AnimationHelper.GetInFromLeftAnimation(this, xCoordinateDifference);
                        OutAnimation = AnimationHelper.GetOutToRightAnimation(this, xCoordinateDifference);

                        ShowNext();
                    }
                    else if (downStart.GetX() > currentX + SCROLL_NEXT_VIEW_THRESHOLD)
                    {
                        InAnimation = AnimationHelper.GetInFromRightAnimation(this, xCoordinateDifference);
                        OutAnimation = AnimationHelper.GetOutToLeftAnimation(this, xCoordinateDifference);

                        ShowPrevious();
                    }
                    else
                    {
                        currentView.Layout(Left, currentView.Top, Width, currentView.Bottom);
                        nextView.Visibility = ViewStates.Gone;
                        previousView.Visibility = ViewStates.Gone;
                    }
                    break;

                case MotionEventActions.Move:
                    currentView.Layout(
                        xCoordinateDifference,
                        currentView.Top,
                        Width+xCoordinateDifference,
                        currentView.Bottom);

                    nextView.Layout(
                        Width + xCoordinateDifference,
                        currentView.Top,
                        Width * 2 + xCoordinateDifference,
                        currentView.Bottom);

                    previousView.Layout(
                        -Width + xCoordinateDifference,
                        currentView.Top,
                        xCoordinateDifference,
                        currentView.Bottom);

                    if (nextView.Visibility != ViewStates.Visible || 
                        previousView.Visibility != ViewStates.Visible)
                    {
                        visibilityMessageHandler.SendMessage(nextView, previousView, Message.Obtain(visibilityMessageHandler, 0));
                    }

                    break;
            }
            return true;
        }

        public override bool OnInterceptTouchEvent(MotionEvent e)
        {
            var nextView = GetChildAt(GetNextChildIndex());
            var previousView = GetChildAt(GetPreviousChildIndex());
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    downStart = MotionEvent.Obtain(e);
                    
                    nextView.Visibility = ViewStates.Invisible;
                    previousView.Visibility = ViewStates.Invisible;
                    break;
                case MotionEventActions.Move:
                    var deltaX = e.GetX() - downStart.GetX();
                    if (Math.Abs(deltaX) > ViewConfiguration.TouchSlop * 2)
                    {
                        return true;
                    }
                    break;
                case MotionEventActions.Up:
                    nextView.Visibility = ViewStates.Gone;
                    previousView.Visibility = ViewStates.Gone;
                    break;
            }
            return false;
        }
    }

    static class AnimationHelper
    {
        public static Animation GetOutToLeftAnimation(View flipper, int xCoordinateDifference)
        {
            if (flipper == null) throw new ArgumentNullException("flipper");
            return new TranslateAnimation(
                (int)Dimension.RelativeToSelf, 0,
                (int)Dimension.Absolute, -flipper.Width - xCoordinateDifference,
                (int)Dimension.Absolute, 0,
                (int)Dimension.Absolute, 0)
            {
                Duration = 350,
                Interpolator = new LinearInterpolator()
            };
        }

        public static Animation GetInFromRightAnimation(View flipper, int xCoordinateDifference)
        {
            if (flipper == null) throw new ArgumentNullException("flipper");
            return new TranslateAnimation(
                (int)Dimension.Absolute, flipper.Width + xCoordinateDifference,
                (int)Dimension.Absolute, 0,
                (int)Dimension.Absolute, 0,
                (int)Dimension.Absolute, 0)
            {
                Duration = 350,
                Interpolator = new LinearInterpolator()
            };
        }

        public static Animation GetOutToRightAnimation(View flipper, int xCoordinateDifference)
        {
            if (flipper == null) throw new ArgumentNullException("flipper");
            return new TranslateAnimation(
                (int)Dimension.RelativeToSelf, 0,
                (int)Dimension.Absolute, flipper.Width - xCoordinateDifference,
                (int)Dimension.Absolute, 0,
                (int)Dimension.Absolute, 0)
            {
                Duration = 350,
                Interpolator = new LinearInterpolator()
            };
        }

        public static Animation GetInFromLeftAnimation(View flipper, int xCoordinateDifference)
        {
            if (flipper == null) throw new ArgumentNullException("flipper");
            return new TranslateAnimation(
                (int)Dimension.Absolute, -flipper.Width + xCoordinateDifference,
                (int)Dimension.Absolute, 0,
                (int)Dimension.Absolute, 0,
                (int)Dimension.Absolute, 0)
            {
                Duration = 350,
                Interpolator = new LinearInterpolator()
            };
        }
    }

    class ViewVisibilityMessageHandler : Handler
    {
        public View NextView { get; set; }
        public View PreviousView { get; set; }

        public void SendMessage(View next, View previous, Message msg)
        {
            Guard.NotNull(next, previous);
            next.Visibility = ViewStates.Visible;
            next.Visibility = ViewStates.Visible;
        }
    }
}

