/*
 * This file is based the work by Marc Reichfelt (Copyright 2010) found at:
 * http://marcreichelt.blogspot.com/2010/09/android-use-realviewswitcher-to-switch.html with
 * the original attributions:
 * "
 *    Work derived from Workspace.java of the Launcher application
 *    see http://android.git.kernel.org/?p=platform/packages/apps/Launcher.git
 * "
 * 
 * Changes have been made to port the file to Mono for Android, adding touch interception
 * for horizontal scroll movements and using a .NET event to communicate changed slides.
 * 
 * Original work is licensed using the
 * Apache 2 license found at http://www.apache.org/licenses/LICENSE-2.0. 
 * This modification is licensed under the same terms as the rest of the Smeedee-Mobile project 
 * found at http://github.com/Smeedee/Smeedee-Mobile
 */
using System;

using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android
{
    class RealViewSwitcher : ViewGroup
    {
        private const int SNAP_VELOCITY = 1000;
	    private const int INVALID_SCREEN = -1;

	    private Scroller _scroller;
	    private VelocityTracker _velocityTracker;

	    private const int TOUCH_STATE_REST = 0;
        private const int TOUCH_STATE_SCROLLING = 1;

	    private int _touchState = TOUCH_STATE_REST;

	    private float _lastMotionX;
	    private int _touchSlop;
	    private int _maximumVelocity;
	    private int _currentScreen;
	    private int _nextScreen = INVALID_SCREEN;

	    private bool _firstLayout = true;

        public event EventHandler ScreenChanged;


        public RealViewSwitcher(IntPtr doNotUse) : base(doNotUse)
        {
            Initialize();
        }

        public RealViewSwitcher(Context context) : base(context)
        {
            Initialize();
        }

        public RealViewSwitcher(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public RealViewSwitcher(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize();
        }

        private void Initialize()
        {
            _scroller = new Scroller(Context);

		    var configuration = ViewConfiguration.Get(Context);
		    _touchSlop = configuration.ScaledTouchSlop;
		    _maximumVelocity = configuration.ScaledMaximumFlingVelocity;
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

            var width = MeasureSpec.GetSize(widthMeasureSpec);
		    var widthMode = MeasureSpec.GetMode(widthMeasureSpec);
		    if (widthMode != MeasureSpecMode.Exactly) {
			    throw new NotSupportedException("ViewSwitcher can only be used in EXACTLY mode.");
		    }

		    var heightMode = MeasureSpec.GetMode(heightMeasureSpec);
		    if (heightMode != MeasureSpecMode.Exactly) {
			    throw new NotSupportedException("ViewSwitcher can only be used in EXACTLY mode.");
		    }

		    // The children are given the same width and height as the workspace
		    var count = ChildCount;
		    for (int i = 0; i < count; i++) {
			    GetChildAt(i).Measure(widthMeasureSpec, heightMeasureSpec);
		    }

		    if (_firstLayout) {
			    ScrollTo(_currentScreen * width, 0);
			    _firstLayout = false;
		    }
        }


        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            int childLeft = 0;

		    var count = ChildCount;
		    for (int i = 0; i < count; i++) {
			    var child = GetChildAt(i);
			    if (child.Visibility != ViewStates.Gone) {
				    var childWidth = child.MeasuredWidth;
				    child.Layout(childLeft, 0, childLeft + childWidth, child.MeasuredHeight);
				    childLeft += childWidth;
			    }
		    }
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            if (_velocityTracker == null) {
			    _velocityTracker = VelocityTracker.Obtain();
		    }
		    _velocityTracker.AddMovement(ev);

		    var action = ev.Action;
		    var x = ev.GetX();

		    switch (action) {
		    case MotionEventActions.Down:
			    /*
			     * If being flinged and user touches, stop the fling. isFinished will be false if being flinged.
			     */
			    if (!_scroller.IsFinished) {
				    _scroller.AbortAnimation();
			    }

			    // Remember where the motion event started
			    _lastMotionX = x;

			    _touchState = _scroller.IsFinished ? TOUCH_STATE_REST : TOUCH_STATE_SCROLLING;

			    break;

		    case MotionEventActions.Move:
			    var xDiff = (int) Math.Abs(x - _lastMotionX);

			    var xMoved = xDiff > _touchSlop;

			    if (xMoved) {
				    // Scroll if the user moved far enough along the X axis
				    _touchState = TOUCH_STATE_SCROLLING;
			    }

			    if (_touchState == TOUCH_STATE_SCROLLING) {
				    // Scroll to follow the motion event
				    var deltaX = (int) (_lastMotionX - x);
				    _lastMotionX = x;

				    var scrollX = ScrollX;
				    if (deltaX < 0) {
					    if (scrollX > 0) {
						    ScrollBy(Math.Max(-scrollX, deltaX), 0);
					    }
				    } else if (deltaX > 0) {
					    var availableToScroll = GetChildAt(ChildCount - 1).Right - scrollX - Width;
					    if (availableToScroll > 0) {
						    ScrollBy(Math.Min(availableToScroll, deltaX), 0);
					    }
				    }
			    }
			    break;

		    case MotionEventActions.Up:
			    if (_touchState == TOUCH_STATE_SCROLLING) {
				    var velocityTracker = _velocityTracker;
				    velocityTracker.ComputeCurrentVelocity(1000, _maximumVelocity);
				    var velocityX = (int) velocityTracker.XVelocity;

				    if (velocityX > SNAP_VELOCITY && _currentScreen > 0) {
					    // Fling hard enough to move left
					    SnapToScreen(_currentScreen - 1);
				    } else if (velocityX < -SNAP_VELOCITY && _currentScreen < ChildCount - 1) {
					    // Fling hard enough to move right
					    SnapToScreen(_currentScreen + 1);
				    } else {
					    SnapToDestination();
				    }

				    if (_velocityTracker != null) {
					    _velocityTracker.Recycle();
					    _velocityTracker = null;
				    }
			    }

			    _touchState = TOUCH_STATE_REST;

			    break;
		    case MotionEventActions.Cancel:
			    _touchState = TOUCH_STATE_REST;
		        break;
		    }

		    return true;
	    }

        private void SnapToDestination()
        {
            var screenWidth = Width;
		    var whichScreen = (ScrollX + (screenWidth / 2)) / screenWidth;

		    SnapToScreen(whichScreen);
        }

        private void SnapToScreen(int whichScreen)
        {
            if (!_scroller.IsFinished)
			    return;

		    whichScreen = Math.Max(0, Math.Min(whichScreen, ChildCount - 1));

		    _nextScreen = whichScreen;

		    var newX = whichScreen * Width;
		    var delta = newX - ScrollX;
		    _scroller.StartScroll(ScrollX, 0, delta, 0, Math.Abs(delta) * 2);
		    Invalidate(); 
        }

        public override void ComputeScroll()
        {
            if (_scroller.ComputeScrollOffset())
            {
                ScrollTo(_scroller.CurrX, _scroller.CurrY);
                PostInvalidate();
            }
            else if (_nextScreen != INVALID_SCREEN)
            {
                _currentScreen = Math.Max(0, Math.Min(_nextScreen, ChildCount - 1));

                OnScreenChanged(new EventArgs());
                _nextScreen = INVALID_SCREEN;
            }
        }

        public int CurrentScreen
        {
            get
            {
                return _currentScreen;
            } 
            set
            {
                _currentScreen = Math.Max(0, Math.Min(value, ChildCount - 1));
                ScrollTo(_currentScreen * Width, 0);
                Invalidate();
            }
        }


        # region Custom Smeedee Functionality
        public View CurrentView
        {
            get { return GetChildAt(_currentScreen); }
        }

        public void ShowPrevious()
        {
            CurrentScreen--;
        }

        public void ShowNext()
        {
            CurrentScreen++;
        }

        /**
         * Used to retrieve control over MotionEvents when
         * the drag movement can be interpreted as horizontal.
         * 
         * This is used to "steal" control from internal listviews and
         * similar views that normally handles control of MotionEvents.
         */
        public override bool OnInterceptTouchEvent(MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _lastMotionX = e.GetX();
                    break;
                case MotionEventActions.Move:
                    var deltaX = e.GetX() - _lastMotionX;
                    if (Math.Abs(deltaX) > ViewConfiguration.TouchSlop * 2)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        protected virtual void OnScreenChanged(EventArgs e)
        {
            if (ScreenChanged != null)
            {
                ScreenChanged(this, e);
            }
        }
        # endregion
    }
}